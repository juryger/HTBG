using System;

/// <summary>
/// Base class for all objects in the game.
/// </summary>
[Serializable]
public abstract class ThingModel : BaseModel
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="id">id of object</param>
    /// <param name="name">name of object</param>
    /// <param name="commandHint">command hint for interacting with object</param>
    public ThingModel(string id, string name, string commandHint)
    {
        Id = id;
        Name = name;
        CommandHint = commandHint;
        Position = new UnityPosition(0f, 0f, 0f);
    }


    // Model identifier
    public string Id { get; private set; }

    // Model name
    public string Name { get; private set; }

    // Modle command hint
    public string CommandHint { get; private set; }

    /// <summary>
    /// Location on the scene
    /// </summary>
    public UnityPosition Position { get; private set; }

    public void SetPosition(UnityPosition pos)
    {
        Position = pos;

        if (ViewModel != null)
        {
            ViewModel.Notify(NotificationName.PlayerPositionChanged, this, pos);
        }
    }

    public override T ConvertToDTO<T>(params object[] data)
    {
        throw new NotImplementedException();
    }
}
