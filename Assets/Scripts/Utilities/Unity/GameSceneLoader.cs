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
        PrepareSceneState();

        // Create main player for the scene
        PrepareMainPlayer();
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

    private void PrepareSceneState()
    {
        Debug.Log("Loading state for scene: " + SceneManager.GetActiveScene().name);

        // set activate scene
        if (string.IsNullOrEmpty(SceneId))
        {
            Debug.Log("SceneLoader game object should define scene identifier.");
            throw new ApplicationException("There is not defined a scene identifier for current Scene.");
        }

        GameStateManager.Instance.SetActiveScene(SceneId);

        // load scene state from storage
        GameStateManager.Instance.LoadSceneState(SceneId);

        var hud = GameObject.FindGameObjectWithTag("HUD");
        if (hud != null)
        {
            var view = hud.GetComponent<SceneView>();
            var model = GameStateManager.Instance.ActiveScene;
            var viewModel = new SceneViewModel(view, model);
        }
    }

    private void PrepareMainPlayer()
    {
        var prefab = Resources.Load<GameObject>("Prefabs/MainPlayer");
        var instance = Instantiate(prefab);
        instance.name = "MainPlayer";

        var view = instance.GetComponent<IView>();
        var model = GameStateManager.Instance.Player;
        model.ResetModel();
        var viewModel = new PlayerViewModel(view, model);

        var playerPosition = new Vector3();

        // get main player spawn point
        var spawnPointName = GameStateManager.Instance.ActiveSpawnPoint;
        if (string.IsNullOrEmpty(spawnPointName))
        {
            playerPosition.x = model.Position.X;
            playerPosition.y = model.Position.Y;
            playerPosition.z = model.Position.Z;
        }
        else
        {
            var spawnPoint = GameObject.Find(spawnPointName);
            if (spawnPoint == null)
                throw new ApplicationException(
                    String.Format("Spawn point game object not found for name: {0}", spawnPointName));

            playerPosition = spawnPoint.transform.position;
        }

        // set main player position
        model.SetPosition(
            new UnityPosition(playerPosition.x, playerPosition.y, playerPosition.z));
    }
}
