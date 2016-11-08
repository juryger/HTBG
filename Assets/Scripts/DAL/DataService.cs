using SQLite4Unity3d;
using System;
using System.IO;
using UnityEngine;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif

/// <summary>
/// SQLite database access layer.
/// </summary>
public class DataService
{
    private SQLiteConnection _connection;

    public DataService(string DatabaseName)
    {

#if UNITY_EDITOR
        var dbPath = string.Format(@"Assets/StreamingAssets/{0}", DatabaseName);
#else
        // check if file exists in Application.persistentDataPath
        var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);

        if (!File.Exists(filepath))
        {
            Debug.Log("Database not in Persistent path");
            // if it doesn't ->
            // open StreamingAssets directory and load the db ->

#if UNITY_ANDROID
            var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName);  // this is the path to your StreamingAssets in android
            while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
            // then save to Application.persistentDataPath
            File.WriteAllBytes(filepath, loadDb.bytes);
#elif UNITY_IOS
                 var loadDb = Application.dataPath + "/Raw/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);
#elif UNITY_WP8
                var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);

#elif UNITY_WINRT
		var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
#else
	var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
	// then save to Application.persistentDataPath
	File.Copy(loadDb, filepath);

#endif

            Debug.Log("Database written");
        }

        var dbPath = filepath;
#endif
        var fi = new FileInfo(dbPath);
        if (!fi.Directory.Exists)
        {
            Directory.CreateDirectory(fi.Directory.FullName);
        }

        var dbExist = File.Exists(dbPath);

        _connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);

        if (!dbExist)
        {
            CreateInitalDB();
        }

        Debug.Log("Final PATH: " + dbPath);

    }

    private void CreateTable<T>() where T : BaseDTO
    {
        //_connection.DropTable<T>();
        _connection.CreateTable<T>();
    }

    /// <summary>
    /// Prepare initial DB for game
    /// </summary>
    private void CreateInitalDB()
    {
        CreateTable<UserDTO>();
        CreateTable<GameStateDTO>();
        CreateTable<CharacterDTO>();
        CreateTable<CharacterStateDTO>();
        CreateTable<SceneDTO>();
        CreateTable<InventoryDTO>();

        _connection.InsertAll(new[] {
            new SceneDTO {
                SceneId = "55327246-932E-4CCE-BBA1-8DB9A905F686",
                Title = "SkeletonInnSuburbs",
                CommandHints = "menu(m);go(g) <direction>;run(r) <direction>;turn(t) <direction>;hault(h);action(a)",
            },
            new SceneDTO {
                SceneId = "C2D30875-49D5-4DF0-9EBB-F1B1C7A76C93",
                Title = "SkeletonInn",
                CommandHints = "menu(m);go(g) <direction>;run(r) <direction>;turn(t) <direction>;hault(h);action(a)",
            },
            new SceneDTO {
                SceneId = "4D505A1D-90FB-4685-ACFC-E176C5D77EAE",
                Title = "WorldMap",
                CommandHints = "menu(m);visit(v) <place number>",
            },
        });
    }

    /// <summary>
    /// Checks if user with required login already exist.
    /// </summary>
    /// <param name="login">user login</param>
    /// <returns>true if user exist in db, else false</returns>
    public bool CheckLogin(string login)
    {
        return null != _connection.Table<UserDTO>()
            .Where(x => x.Login == login)
            .FirstOrDefault();
    }

    /// <summary>
    /// Authenticate an existing user in local DB.
    /// </summary>
    /// <param name="login">user login</param>
    /// <param name="password">user password</param>
    /// <returns>true if user authenticated successful, flase in other case</returns>
    public UserModel AuthenticateUser(string login, string password)
    {
        var userDto = _connection.Table<UserDTO>()
            .Where(x =>
                x.Login == login &&
                x.Password == password)
            .FirstOrDefault();

        if (userDto == null)
            return null;

        return userDto.ConvertToModel<UserModel>();
    }

    /// <summary>
    /// Register a new user in local DB.
    /// </summary>
    /// <param name="login">user login</param>
    /// <param name="password">user password</param>
    /// <param name="age">age of user's character</param>
    /// <param name="gender">gender of user's character</param>
    /// <returns>true if user authenticated successful, flase in other case</returns>
    public UserModel RegisterNewUser(string login, string password, int age, CharacterGender gender)
    {
        if (CheckLogin(login))
            return null;

        // Create new character 
        var character = GameEnvironmentManager.CreatePlayer(login, "Talk (t)", GeneralName.InitialGameSceneId, age, gender);
        var characterDto = character.ConvertToDTO<CharacterDTO>();

        // Get inventory for character
        var inventoryDto = character.Inventory.ConvertToDTO<InventoryDTO>();

        // Create new user
        var user = new UserModel(login, character.Id);
        var userDto = user.ConvertToDTO<UserDTO>(password);

        // todo: save all objects to Database in transaction
        _connection.Insert(inventoryDto);
        _connection.Insert(characterDto);
        _connection.Insert(userDto);

        InitializeNewGame(userDto, characterDto);

        return user;
    }

    /// <summary>
    /// Initialize new game state for authenticated user.
    /// </summary>
    /// <param name="userDto">authenticated user</param>
    /// <param name="characterDto">associated character</param>
    private void InitializeNewGame(UserDTO userDto, CharacterDTO characterDto)
    {
        if (userDto == null)
            throw new ArgumentNullException("userDto");

        if (characterDto == null)
            throw new ArgumentNullException("characterDto");

        // Get character for user
        //////var characterDto = _connection.Table<CharacterDTO>()
        //////    .Where(c => c.CharacterId == user.CharacterId)
        //////    .First();

        // Create game safe point
        var created = DateTime.UtcNow;
        var gameState = new GameStateModel(
            Guid.NewGuid().ToString(),
            userDto.Login,
            GeneralName.InitialGameSceneId,
            string.Format("Autosave for {0} at {1}", userDto.Login, created.ToLocalTime()),
            created);
        var gameStateDto = gameState.ConvertToDTO<GameStateDTO>();

        // Create main character safe point state
        var gameStateCharacterDto = new CharacterStateDTO(gameStateDto.GameStateId, characterDto);

        // todo: save all objects to Database in transaction
        _connection.Insert(gameStateDto);
        _connection.Insert(gameStateCharacterDto);
    }

    /// <summary>
    /// Load scene definition.
    /// </summary>
    /// <param name="sceneId">scene identifier</param>
    public SceneModel LoadSceneDefenition(string sceneId)
    {
        var sceneDto = _connection.Table<SceneDTO>()
            .Where(c => c.SceneId == sceneId)
            .FirstOrDefault();

        return sceneDto.ConvertToModel<SceneModel>();
    }

    /// <summary>
    /// Load most recent game save definition.
    /// </summary>
    /// <param name="login">user login for which game save definition required</param>
    /// <returns>most recent game save definition</returns>
    public GameStateModel GetLastGameSave(string login)
    {
        var gameStateDto = _connection.Table<GameStateDTO>()
            .Where(g => g.Login == login)
            .OrderByDescending(g => g.SaveDate).
            FirstOrDefault();

        return gameStateDto == null ? null :
            gameStateDto.ConvertToModel<GameStateModel>();
    }

    /// <summary>
    /// Load player state.
    /// </summary>
    /// <param name="gameStateId">saved game identifier</param>
    /// <param name="playerId">player identifier</param>
    /// <returns>character model</returns>
    public CharacterModel LoadPlayerState(string gameStateId, string playerId)
    {
        var playerDto = _connection.Table<CharacterStateDTO>()
            .Where(c => c.GameStateId == gameStateId && c.CharacterId == playerId)
            .FirstOrDefault();

        if (playerDto == null)
            return null;

        var inventoryDto = _connection.Table<InventoryDTO>()
            .Where(i => i.InventoryId == playerDto.InventoryId)
            .FirstOrDefault();

        if (inventoryDto == null)
            return null;

        return playerDto.ConvertToModel<CharacterModel>(inventoryDto.ConvertToModel<InventoryModel>());
    }

    /// <summary>
    /// Load game state.
    /// </summary>
    /// <param name="gameState">current state of the game</param>
    /// <param name="gameStateId">saved game identifier</param>
    /// <param name="sceneId">current scene identifier</param>
    /// <param name="playerId">player identifier</param>
    /// <returns></returns>
    public CharacterModel LoadGameState(ExtendedGameState gameState, string gameStateId, string sceneId, string playerId)
    {
        var charactersDto = _connection.Table<CharacterStateDTO>()
            .Where(c => c.GameStateId == gameStateId && c.SceneId == sceneId);

        CharacterModel player = null;

        foreach (var itemDto in charactersDto)
        {
            var inventoryDto = _connection.Table<InventoryDTO>()
                .Where(i => i.InventoryId == itemDto.InventoryId)
                .FirstOrDefault();

            // todo: load loot itmes for current inventory

            var character = itemDto.ConvertToModel<CharacterModel>(
                    inventoryDto.ConvertToModel<InventoryModel>());

            gameState.AddCharacter(character);

            if (character.Id == playerId)
                player = character;
        }

        return player;
    }

    /// <summary>
    /// Save game state.
    /// </summary>
    /// <param name="login">authenticated user login</param>
    /// <param name="sceneId">active scene identifier</param>
    /// <param name="player">player state</param>
    /// <param name="extGameState">game state</param>
    /// <returns></returns>
    public void SaveGame(string login, string sceneId, CharacterModel player, ExtendedGameState extGameState)
    {
        // Create game safe point
        var created = DateTime.UtcNow;
        var gameState = new GameStateModel(
            Guid.NewGuid().ToString(),
            login,
            sceneId,
            string.Format("Save for {0} at {1}", login, created.ToLocalTime()),
            created);
        var gameStateDto = gameState.ConvertToDTO<GameStateDTO>();

        // Create main character safe point state
        var gameStateCharacterDto =
            player.ConvertToDTO<CharacterStateDTO>(
                gameStateDto.GameStateId);

        // todo: save all objects to Database in transaction
        _connection.Insert(gameStateDto);
        _connection.Insert(gameStateCharacterDto);

        // todo: process extGameState saving
    }

    ////////public IEnumerable<Person> GetPersons(){
    ////////	return _connection.Table<Person>();
    ////////}

    ////////public IEnumerable<Person> GetPersonsNamedRoberto(){
    ////////	return _connection.Table<Person>().Where(x => x.Name == "Roberto");
    ////////}

    //////public Person GetJohnny(){
    //////	return _connection.Table<Person>().Where(x => x.Name == "Johnny").FirstOrDefault();
    //////}

    //////public Person CreatePerson(){
    //////	var p = new Person{
    //////			Name = "Johnny",
    //////			Surname = "Mnemonic",
    //////			Age = 21
    //////	};
    //////	_connection.Insert (p);
    //////	return p;
    //////}
}
