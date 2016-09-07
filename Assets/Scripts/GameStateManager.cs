using System.Collections.Generic;

/// <summary>
/// Manager of the current scene (singleton).
/// </summary>
public class GameStateManager : Singleton<GameStateManager>
{
    /// <summary>
    /// Indicate if there is a game being loaded or started.
    /// </summary>
    public bool HasActiveGame { get; private set; }

    /// <summary>
    /// Active scene name.
    /// </summary>
    public string ActiveSceneName { get; private set; }

    /// <summary>
    /// Name of spawn point for showing player during scene load
    /// </summary>
    public string ActiveSpawnPoint { get; private set; }

    /// <summary>
    /// Position of main player during load saved game.
    /// </summary>
    public UnityPosition PlayerPositionOnGameLoad { get; private set; }

    /// <summary>
    /// Main player state
    /// </summary>
    public PlayerModel Player { get; private set; }

    /// <summary>
    /// State of NPCs for active scene.
    /// </summary>
    public Dictionary<string, NpcModel> ActiveSceneNPCs { get; private set; }

    /// <summary>
    /// State of Enemies for active scene.
    /// </summary>
    public Dictionary<string, EnemyModel> ActiveSceneEnemies { get; private set; }

    /// <summary>
    /// State of Loot for active scene.
    /// </summary>
    public Dictionary<string, NpcModel> ActiveLoot { get; private set; }


    /// <summary>
    /// State of Facilities for active scene.
    /// </summary>
    public Dictionary<string, NpcModel> ActiveFacilities { get; private set; }

    /// <summary>
    /// Protected consructor for Singleton behaviour.
    /// </summary>
    protected GameStateManager()
    {
        // note: remove after creating main menu (for test purposes)
        Player = new PlayerModel("Rumata Estorsky", 40, 185, 90);
        ActiveSceneName = SceneName.SkeletonInnSuburbs;
        ActiveSpawnPoint = GeneralName.DefaultSpawnPointName;

        // note: for testing purpose
        //////Load();
    }

    /// <summary>
    /// Prepare player state for the new game.
    /// </summary>
    public void NewGame(string playerName, int playerAge, int playerHeight, int playerWeight)
    {
        Player = new PlayerModel(playerName, playerAge, playerHeight, playerWeight);
        ActiveSceneName = SceneName.SkeletonInnSuburbs;

        // todo: initialize key NPCs, enemies, loot and facilities for new game
    }

    /// <summary>
    /// Load state of the game from some storage.
    /// </summary>
    /// <param name="savedGameName"></param>
    public void LoadGame(string savedGameName)
    {
        // todo: load real data from some storage
        Player = new PlayerModel("juryger", 34, 175, 75);
        ActiveSceneName = SceneName.SkeletonInn;
        ActiveSpawnPoint = GeneralName.DefaultSpawnPointName;
        PlayerPositionOnGameLoad = new UnityPosition(49f, -785f, 0f);

        // todo: load key NPCs, enemies, loot and facilities for saved game
    }
    /// <summary>
    /// Save state of the game to some storage.
    /// </summary>
    /// <param name="gameName"></param>
    public void SaveGame(string gameName)
    {
        // todo: Save Player state
        //Player


        // todo: save active scene name
        //ActiveSceneName

        // todo: save key NPC, enemis, loot and facilities state
    }

    /// <summary>
    /// Initialize scene before loading at Unity.
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="spawnPoint"></param>
    public void InitializeScene(string sceneName, string spawnPoint)
    {
        // todo: load base scene data from some storage
        ActiveSceneName = sceneName;
        ActiveSpawnPoint = spawnPoint;

        // note: for testing purpose
        if (Player.Controller != null)
        {
            Player.Controller.Dispose();

            Player = new PlayerModel("Rumata Estorsky", 40, 185, 90);
        }

        Player.SetHealth(50);
        Player.SetStamina(50);
        //////Save();
    }

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
}
