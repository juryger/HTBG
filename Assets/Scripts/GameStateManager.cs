using System;
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

        // create connection to local database
        dbManager = new DataService(GeneralName.LocalDatabaseName);
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
        AuthenticatedUser = null;
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

        Player = dbManager.LoadPlayerState(gameState.Id);
        if (Player == null)
            throw new ApplicationException("Could not load last save game because player is not exist at storage.");

        // todo: load Characters, Loots, and Facilities for saved game
        // extendedGameState = DbManager.LoadPlayerState(gameState.Id);

        // Prepare current scene
        SetActiveScene(gameState.CurrentSceneId);
        LoadSceneState(gameState.CurrentSceneId);

        // Reset spawn point because saved player position should be used.
        ActiveSpawnPoint = (Player.Position.X == 0 && Player.Position.Y == 0 ?
            GeneralName.DefaultSpawnPointName :
            string.Empty);
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

        ResetGameExceptCurrentScene();
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

    /// <summary>
    /// Load scene state from storage.
    /// </summary>
    /// <param name="sceneId">scene identifier</param>
    public void LoadSceneState(string sceneId)
    {
        // todo: update game extended state for specified scene
    }

    /// <summary>
    /// Reset game state for unloaded scene.
    /// </summary>
    /// <param name="sceneId">scene identifier</param>
    private void ResetGameExceptCurrentScene()
    {
        // todo: update extended state for scene objects
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
