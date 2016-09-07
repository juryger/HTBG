/// <summary>
/// Represent scene of the game.
/// </summary>
public class SceneModel : BaseModel
{
    public SceneModel(string name, string[] commandHints)
    {
        Name = name ?? string.Empty;
        CommandHints = commandHints ?? new string[] { };
    }

    /// <summary>
    /// Name of the scene.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// List of supported command hints.
    /// </summary>
    public string[] CommandHints { get; private set; }

    public PlayerModel Player
    {
        get
        {
            return GameStateManager.Instance.Player;
        }
    }
}