using System;
using UnityEngine;

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

        // todo: prepare NPC, enemies, ...
    }

    private void PrepareSceneState()
    {
        var hud = GameObject.FindGameObjectWithTag("HUD");
        if (hud != null)
        {
            var view = hud.GetComponent<SceneView>();
            var model = new SceneModel(view.SceneName, view.SceneCommandHints);
            var viewModel = new SceneViewModel(view, model);
        }
    }

    private void PrepareMainPlayer()
    {
        var prefab = Resources.Load<GameObject>("Prefabs/MainPlayer");
        var instance = UnityEngine.Object.Instantiate(prefab);
        instance.name = "MainPlayer";

        var view = instance.GetComponent<IView>();
        var model = GameStateManager.Instance.Player;
        var viewModel = new PlayerViewModel(view, model);

        // get main player spawn point
        var spawnPointName = GameStateManager.Instance.ActiveSpawnPoint;
        if (String.IsNullOrEmpty(spawnPointName))
            throw new ApplicationException(
                String.Format("Spawn point name is not defined for scene: {0}", GameStateManager.Instance.ActiveSceneName));

        var spawnPoint = GameObject.Find(spawnPointName);
        if (spawnPoint == null)
            throw new ApplicationException(
                String.Format("Spawn point game object not found for name: {0}", spawnPointName));

        // set main player position
        var pos = spawnPoint.transform.position;
        model.SetPosition(
            new UnityPosition(pos.x, pos.y, pos.z));
    }
}
