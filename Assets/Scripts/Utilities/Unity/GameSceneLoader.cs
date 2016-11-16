using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages to load general game scene.
/// </summary>
public class GameSceneLoader : MonoBehaviour
{
    private bool isShownMenu;

    /// <summary>
    /// Current scene identifier.
    /// </summary>
    public string SceneId;

    // Use this for initialization
    void Start()
    {
        // Load Scene state
        InitializeScene();

        // Create main player for the scene
        InitializeMainPlayer();

        // Create NPC for the scene
        InitializeNpcs();

        // Create Loot for the scene
        InitializeLoot();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isShownMenu)
            {
                isShownMenu = true;
                Time.timeScale = 0;

                // load main menu in additive mode
                SceneManager.LoadScene(SceneName.MainMenu, LoadSceneMode.Additive);
            }
            else
            {
                isShownMenu = false;
                Time.timeScale = 1.0f;

                SceneManager.UnloadScene(SceneName.MainMenu);
            }
        }
    }

    private void InitializeScene()
    {
        Debug.Log("Loading state for scene: " + SceneManager.GetActiveScene().name);

        // set activate scene
        if (string.IsNullOrEmpty(SceneId))
        {
            Debug.Log("SceneLoader game object should define scene identifier.");
            throw new ApplicationException("There is not defined a scene identifier for current Scene.");
        }

        var gameManager = GameStateManager.Instance;
        var activeScene = gameManager.ActiveScene;

        gameManager.SetActiveScene(SceneId);

        // unload game player for previously actve scene
        if (activeScene != null)
            gameManager.UnloadSceneMutiplayers(activeScene.Id);

        // upadte scene state from storage
        gameManager.LoadSceneState(SceneId);

        var hud = GameObject.FindGameObjectWithTag("HUD");
        if (hud != null)
        {
            var view = hud.GetComponent<SceneView>();
            var model = gameManager.ActiveScene;
            var viewModel = new SceneViewModel(view, model);
        }
    }

    private void InitializeMainPlayer()
    {
        var player = GameStateManager.Instance.Player;
        var playerPrefabName =
            player.Gender == CharacterGender.Male ?
                "Prefabs/MainPlayerMale" :
                "Prefabs/MainPlayerFemale";
        var prefab = Resources.Load<GameObject>(playerPrefabName);
        var instance = Instantiate(prefab);
        instance.name = player.Name;

        var view = instance.GetComponent<IView>();
        player.ResetViewModel();
        var viewModel = new CharacterViewModel(view, player);

        var playerPosition = new Vector3();

        // get main player spawn point
        var spawnPointName = GameStateManager.Instance.ActiveSpawnPoint;
        if (string.IsNullOrEmpty(spawnPointName))
        {
            playerPosition.x = player.Position.X;
            playerPosition.y = player.Position.Y;
            playerPosition.z = player.Position.Z;
        }
        else
        {
            var spawnPoint = GameObject.Find(spawnPointName);
            if (spawnPoint == null)
                throw new ApplicationException(
                    String.Format("Spawn point game object not found for name: {0}", spawnPointName));

            playerPosition = spawnPoint.transform.position;
        }

        // update main player position and scene
        player.SceneId = SceneId;
        player.SetPosition(
            new UnityPosition(playerPosition.x, playerPosition.y, playerPosition.z));

        // ceneter camera for WorldMap scene
        if (GameStateManager.Instance.ActiveScene.Name == SceneName.WorldMap)
        {
            var centerPoint = GameObject.Find(GeneralName.WorldMapCenterPointName);
            if (centerPoint != null)
            {
                var cameraObj = GameObject.Find(GeneralName.MainCameraName);
                if (cameraObj != null)
                {
                    var camera = cameraObj.GetComponent<Camera>();
                    var cameraScript = camera.GetComponent(typeof(CameraFollow)) as CameraFollow;
                    cameraScript.followTartget = false;

                    camera.orthographicSize = 530f;
                    camera.transform.position =
                        Vector3.Lerp(centerPoint.transform.position, centerPoint.transform.position, 0.1f) +
                        new Vector3(0, 0, -0.9f);
                }
            }
        }
    }

    private void InitializeNpcs()
    {
        // load npcs state

    }

    private void InitializeLoot()
    {
        // load loot state

    }
}
