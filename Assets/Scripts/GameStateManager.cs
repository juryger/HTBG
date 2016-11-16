using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Manager of the current scene (singleton).
/// </summary>
public class GameStateManager : Singleton<GameStateManager>
{
    // Active game state
    private ExtendedGameState extendedGameState;

    // Local database manager
    private DataService dbManager;

    /// <summary>
    /// Protected consructor for Singleton behaviour.
    /// </summary>
    protected GameStateManager()
    {
        extendedGameState = new ExtendedGameState();
    }

    // Use this for initialization
    void Start()
    {
        // create connection to local database
        dbManager = new DataService(GeneralName.LocalDatabaseName);

        if (dbManager == null)
            throw new ApplicationException("Unable to connect to database: " + GeneralName.LocalDatabaseName);

        // connect to Game server and register client application
        GameServer = new GameServerHelper();
        StartCoroutine(GameServer.RegisterClientApp());
    }

    void Update()
    {
        SyncActiveSceneMultiplayers();
    }

    /// <summary>
    /// Active scene name.
    /// </summary>
    public SceneModel ActiveScene { get; private set; }

    /// <summary>
    /// Name of spawn point for showing player during scene load
    /// </summary>
    public string ActiveSpawnPoint { get; private set; }

    /// <summary>
    /// Main player.
    /// </summary>
    public CharacterModel Player { get; private set; }

    /// <summary>
    /// Game server.
    /// </summary>
    public GameServerHelper GameServer { get; private set; }

    /// <summary>
    /// Current authenticated user of the game.
    /// </summary>
    public UserModel AuthenticatedUser { get; private set; }

    /// <summary>
    /// Login existing user.
    /// </summary>
    /// <param name="login">user login</param>
    /// <param name="password">user password</param>
    /// <returns>true if authentication succeeded, false if not</returns>
    public bool LoginExistingUser(string login, string password)
    {
        AuthenticatedUser = dbManager.AuthenticateUser(login, password);

        return AuthenticatedUser != null;
    }

    /// <summary>
    /// Register new user.
    /// </summary>
    /// <param name="login">user's login</param>
    /// <param name="password">user's password</param>
    /// <param name="age">age of player associated with user</param>
    /// <param name="gender">gender of player associated with user</param>
    /// <returns>true if user registered successfully, false if user with the login already exists</returns>
    public bool RegisterNewUser(string login, string password, int age, CharacterGender gender)
    {
        if (dbManager.CheckLogin(login))
        {
            return false;
        }

        AuthenticatedUser = dbManager.RegisterNewUser(login, password, age, gender);

        LoadLastSavedGame();

        // Set default spawn point in order to set initial player position at scene.
        ActiveSpawnPoint = GeneralName.DefaultSpawnPointName;

        return null != AuthenticatedUser;
    }

    /// <summary>
    /// Logout user.
    /// </summary>
    public void LogoutUser()
    {
        // Remove player info from Game server
        StartCoroutine(GameServer.RemovePlayerState(Player.Id));

        print("Remove player state from Game server...");

        AuthenticatedUser = null;
    }

    /// <summary>
    /// Load last saved state of the game from storage.
    /// </summary>
    public void LoadSceneState(string sceneId)
    {
        if (AuthenticatedUser == null)
            throw new ApplicationException("Could not load last saved game because user is not authenitcated.");

        var gameState = dbManager.GetLastGameSave(AuthenticatedUser.Login);
        if (gameState == null)
            throw new ApplicationException("Could not load last save game because it is not exist at storage.");

        // Load Characters, Loots, and Facilities for saved game
        dbManager.LoadGameState(extendedGameState, gameState.Id, sceneId, AuthenticatedUser.CharacterId);
    }

    /// <summary>
    /// Load last saved state of the game from storage.
    /// </summary>
    public void LoadLastSavedGame()
    {
        if (AuthenticatedUser == null)
            throw new ApplicationException("Could not load last saved game because user is not authenitcated.");

        var gameState = dbManager.GetLastGameSave(AuthenticatedUser.Login);
        if (gameState == null)
            throw new ApplicationException("Could not load last save game because it is not exist at storage.");

        // Load Player, Characters, Loots, and Facilities for saved game
        Player = dbManager.LoadGameState(extendedGameState, gameState.Id, gameState.CurrentSceneId, AuthenticatedUser.CharacterId);

        if (Player == null)
            throw new ApplicationException("Could not load last save game because player is not exist at storage.");

        SetActiveScene(gameState.CurrentSceneId);

        // Reset spawn point because saved player position should be used.
        ActiveSpawnPoint = (Player.Position.X == 0 && Player.Position.Y == 0 ?
            GeneralName.DefaultSpawnPointName :
            string.Empty);
    }

    /// <summary>
    /// Set active spawn point on scene.
    /// </summary>
    /// <param name="spawnPoint">spwan point inside scene</param>
    public void SetActiveSpawnPoint(string spawnPoint)
    {
        if (string.IsNullOrEmpty(spawnPoint))
            throw new ArgumentNullException("spawnPoint");

        ActiveSpawnPoint = spawnPoint;
    }

    /// <summary>
    /// Reset active spawn point on scene (player current position would be used instead).
    /// </summary>
    public void ResetActiveSpawnPoint()
    {
        ActiveSpawnPoint = string.Empty;
    }

    /// <summary>
    /// Sync multiplayers state for active scene.
    /// </summary>
    public void SyncActiveSceneMultiplayers()
    {
        // if game not started leave processing
        if (ActiveScene == null)
            return;

        var playersList = GameServer.GamePlayersSync;

        // Prepare dictionary for actual multiplayers for easier access
        var dicPlayers = new Dictionary<string, CharacterDTO>();
        foreach (var item in playersList)
        {
            // skip current player
            if (string.Equals(Player.Id, item.CharacterId, StringComparison.InvariantCultureIgnoreCase))
                continue;

            dicPlayers.Add(item.CharacterId, item);
        }

        // Get multplayers which are not exist any more
        var outdatedPlayers = extendedGameState.GetOutdatedMultiplayers(ActiveScene.Id, dicPlayers.Keys);

        // Get multplayers which are still exist
        var activePlayers = extendedGameState.GetActiveMultiplayers(ActiveScene.Id, dicPlayers.Keys);

        // Get new multplayers
        var newPlayerIds = extendedGameState.GetNewMultiplayers(ActiveScene.Id, dicPlayers.Values);

        #region remove outdated multiplayers

        foreach (var item in outdatedPlayers)
        {
            // scene
            item.ViewModel.Notify(NotificationName.RemoveCharacterFromScene, this);

            // state
            extendedGameState.ResetMultiplayerCharacter(item.Id);
        }

        #endregion

        #region Update existing players

        foreach (var item in activePlayers)
        {
            // state
            // note: inventory should be also syncronized via Game Server
            var player = dicPlayers[item.Id].ConvertToModel<CharacterModel>(
                new InventoryModel(
                    Guid.NewGuid().ToString(),
                    "MultiplayerInventory",
                    "inventory (i)",
                    InventoryKind.Character));
            extendedGameState.AddOrUpdateMultiplayerCharacter(player);

            // scene
            var mv = GetMulitplayerMovementVector(item, player);
            if (mv.X != 0f || mv.Y != 0f)
            {
                item.ViewModel.Notify(NotificationName.CharacterMovementVectorChanged, this, mv, false);
                item.ViewModel.Notify(NotificationName.RequestCharacterPosition, this);
            }
            else
                item.ViewModel.Notify(NotificationName.CharacterMovementHaulted, this);

            item.ViewModel.Notify(NotificationName.CharacterStatisticsChanged, this, player.Statistics);
        }

        #endregion

        #region Add new players

        foreach (var id in newPlayerIds)
        {
            // note: inventory should be also syncronized via Game Server
            var player = dicPlayers[id].ConvertToModel<CharacterModel>(
                new InventoryModel(
                    Guid.NewGuid().ToString(),
                    "MultiplayerInventory",
                    "inventory (i)",
                    InventoryKind.Character));

            // scene 
            AddNewMultiplayer(player);

            // state
            extendedGameState.AddOrUpdateMultiplayerCharacter(player);
        }

        #endregion
    }

    /// <summary>
    /// Unload all mutlitplayers of active scene (on scene unload).
    /// </summary>
    public void UnloadSceneMutiplayers(string sceneId)
    {
        if (ActiveScene == null)
            return;

        // Get multplayers which are still exist
        foreach (var item in extendedGameState.GetMultiplayers(sceneId))
        {
            item.ViewModel.Notify(NotificationName.RemoveCharacterFromScene, this);

            extendedGameState.ResetMultiplayerCharacter(item.Id);
        }
    }

    /// <summary>
    /// Save state of the game to storage.
    /// </summary>
    public void SaveGame()
    {
        if (AuthenticatedUser == null)
            throw new ApplicationException("Could not save game because user is not authenticated.");

        // save game state
        dbManager.SaveGame(AuthenticatedUser.Login, ActiveScene.Id, Player, extendedGameState);

        ResetGameStateExceptCurrentScene();
    }

    /// <summary>
    /// Set active scene.
    /// </summary>
    /// <param name="sceneId">scene identifier</param>
    public void SetActiveScene(string sceneId)
    {
        ActiveScene = dbManager.LoadSceneDefenition(sceneId);

        if (ActiveScene == null)
            throw new ApplicationException(string.Format("Could not find a scene with id: {0}", sceneId));
    }

    #region Internal methods

    /// <summary>
    /// Prepares MVC objects for new Multiplayer.
    /// </summary>
    /// <param name="player">player info</param>
    private void AddNewMultiplayer(CharacterModel player)
    {
        var playerPrefabName =
                    player.Gender == CharacterGender.Male ?
                        "Prefabs/MainPlayerMale" :
                        "Prefabs/MainPlayerFemale";
        var prefab = Resources.Load<GameObject>(playerPrefabName);

        var instance = Instantiate(prefab);
        instance.name = player.Name;

        // set multiplayer flag
        var playerView = instance.GetComponent(typeof(PlayerView)) as PlayerView;
        if (playerView != null)
        {
            playerView.IsMultiplayer = true;
        }

        var viewModel = new CharacterViewModel(playerView, player);
    }

    /// <summary>
    /// Return multiplayer character movement vector.
    /// </summary>
    /// <param name="oldState">old state of multiplayer</param>
    /// <param name="newState">new state of multiplayer</param>
    /// <returns>movement vector</returns>
    private UnityVector2 GetMulitplayerMovementVector(CharacterModel oldState, CharacterModel newState)
    {
        var dx = newState.Position.X - oldState.Position.X;
        var dy = newState.Position.Y - oldState.Position.Y;

        // delta value '3' considers minimal and close to "zero"
        float vx = (Mathf.Floor(Mathf.Abs(dx)) <= 3) ? 0f : 1f;
        float vy = (Mathf.Floor(Mathf.Abs(dy)) <= 3) ? 0f : 1f;

        if (Mathf.Sign(dx) < 0f) vx *= -1;
        if (Mathf.Sign(dy) < 0f) vy *= -1;

        Debug.Log(
            String.Format("Calculating multiplayer movement vector, Dx:{0}, Dy:{1}, Vx:{2}, Vy:{3}",
            dx, dy, vx, vy));

        return new UnityVector2(vx, vy);
    }

    /// <summary>
    /// Reset game state for all earlier unloaded scenes excerpt current scene.
    /// </summary>
    /// <param name="sceneId">scene identifier</param>
    private void ResetGameStateExceptCurrentScene()
    {
        // todo: update extended state for scene objects
    }

    #endregion

    #region PersistenceManager based on local file and binary serialization

    //////// todo: move Load and Save to PersistenceManager
    //////public void Load()
    //////{
    //////    if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
    //////    {
    //////        BinaryFormatter bf = new BinaryFormatter();
    //////        FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
    //////        var restoredData = (PlayerModel)bf.Deserialize(file);
    //////        file.Close();

    //////        Player.SetHealth(restoredData.Health);
    //////        Player.SetStamina(restoredData.Health);
    //////    }
    //////}

    //////public void Save()
    //////{
    //////    BinaryFormatter bf = new BinaryFormatter();
    //////    FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

    //////    bf.Serialize(file, Player);
    //////    file.Close();
    //////}

    #endregion
}
