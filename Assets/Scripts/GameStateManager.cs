using UnityEngine;
using System.Collections;
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
    /// Active scene command hints.
    /// </summary>
    public string[] ActiveSceneCommandHints { get; private set; }

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
        // todo: remove after creating main menu (for test purposes)
        Player = new PlayerModel("Rumata Estorsky", 40, 185, 90);
        ActiveSceneName = SceneName.SkeletonInnSuburbs;
        ActiveSceneCommandHints = new[] { "Menu (M)", "Turn (T) <direction>", "Go (G) <direction>", "Halt (H)", "Action (A)" };
    }

    /// <summary>
    /// Prepare player state for the new game.
    /// </summary>
    public void OnNewGame(string playerName, int playerAge, int playerHeight, int playerWeight)
    {
        Player = new PlayerModel(playerName, playerAge, playerHeight, playerWeight);
        ActiveSceneName = SceneName.SkeletonInnSuburbs;

        // todo: initialize NPCs, enemies, loot and facilities for new game
    }

    /// <summary>
    /// Load state of the game from some storage.
    /// </summary>
    /// <param name="savedGameName"></param>
    public void OnLoadGame(string savedGameName)
    {
        // todo: load real data from some storage
        Player = new PlayerModel("juryger", 34, 175, 75);
        ActiveSceneName = SceneName.SkeletonInn;

        // todo: load NPCs, enemies, loot and facilitie for saved game
    }

    public void OnLoadScene(string sceneName)
    {
        // todo: load real data from some storage
        Player = new PlayerModel("juryger", 34, 175, 75);
        ActiveSceneName = sceneName;

        // todo: load NPCs, enemies, loot and facilitie for saved game
    }

    // Use this for initialization
    void Start () {
    
    }
    
    // Update is called once per frame
    void Update () {
    
    }
}
