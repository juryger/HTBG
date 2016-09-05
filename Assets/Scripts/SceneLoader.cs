using UnityEngine;
using System.Collections;

public class SceneLoader : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {

        if (GameStateManager.Instance.ActiveSceneName == SceneName.GlobalMap)
        {
            // todo: show small picture of main character at his current location
            return;
        }

        // Load main player to the scene        
        LoadMainPlayer();

        // todo: load NPC, enemies, ...
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private void LoadMainPlayer()
    {
        var prefab = Resources.Load<GameObject>("Prefabs/MainPlayer");
        var instance = UnityEngine.Object.Instantiate(prefab);
        instance.name = "MainPlayer";

        var view = instance.GetComponent<IView>();
        var model = GameStateManager.Instance.Player;
        var controller = new PlayerController(view, model);

        model.SetPosition(new UnityPosition(49f, -785f, 0f));
    }
}
