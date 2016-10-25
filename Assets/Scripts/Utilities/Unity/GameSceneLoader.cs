using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages to load general game scene.
/// </summary>
public class GameSceneLoader : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        // Load Scene state
        PrepareSceneState();

        // Create main player for the scene
        PrepareMainPlayer();
    }

    private void PrepareSceneState()
    {
        //if (string.IsNullOrEmpty(SceneId))
        //    throw new ApplicationException("Scene identifier for SceneLoader should be initialized.");        
        var sceneName = SceneManager.GetActiveScene().name;
        Debug.Log("Loading state for scene: " + sceneName);

        // activate scene
        GameStateManager.Instance.SetActiveScene(sceneName);

        // load scene state from storage
        GameStateManager.Instance.LoadSceneState(sceneName);

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
