using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameSceneLoader : MonoBehaviour
{
    void Start()
    {
        // load main menu in additive mode
        SceneManager.LoadScene(SceneName.MainMenu, LoadSceneMode.Additive);
    }
}
