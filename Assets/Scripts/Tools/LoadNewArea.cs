using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadNewArea : MonoBehaviour {

    public string LevelToLoad;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "MainPlayer")
        {
            GameStateManager.Instance.OnLoadScene(LevelToLoad);

            SceneManager.LoadScene(LevelToLoad);
        }
    }
}
