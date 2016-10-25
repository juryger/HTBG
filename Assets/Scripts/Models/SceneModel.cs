using System;

/// <summary>
/// Represent scene of the game.
/// </summary>
public class SceneModel : BaseModel
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="id">scene identifier</param>
    /// <param name="name">name of the scene</param>
    /// <param name="commandHints">list of command hints</param>
    /// <param name="spawnPoints">list of spawn points of the scene</param>
    public SceneModel(string id, string name, string commandHints)
    {
        if (string.IsNullOrEmpty(id))
            throw new ArgumentNullException("id");

        Id = id;
        Name = name;
        CommandHints = commandHints;
    }

    /// <summary>
    /// Scene identifier.
    /// </summary>
    public string Id { get; private set; }

    /// <summary>
    /// Scene name.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Command hints of the scene.
    /// </summary>
    public string CommandHints { get; private set; }

    public override T ConvertToDTO<T>(params object[] data)
    {
        return new SceneDTO()
        {
            SceneId = Id,
            Title = Name,
            CommandHints = this.CommandHints,
        } as T;
    }
}