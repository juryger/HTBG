using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Used to handle collision of player with level board points.
/// </summary>
public class LoadNewArea : MonoBehaviour
{
    /// <summary>
    /// Name of scene to load.
    /// </summary>
    public string DestinationSceneName;

    /// <summary>
    /// Name of spawn point at loaded scene.
    /// </summary>
    public string DestinationSpawnPoint;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.gameObject.GetComponent(typeof(PlayerView));
        if (player != null && false == ((PlayerView)player).IsMultiplayer)
        {
            // set active spawn point at new scene
            GameStateManager.Instance.SetActiveSpawnPoint(DestinationSpawnPoint);

            SceneManager.LoadScene(DestinationSceneName);
        }
    }
}
