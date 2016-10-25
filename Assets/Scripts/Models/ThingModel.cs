using System;

/// <summary>
/// Base class for all objects in the game.
/// </summary>
[Serializable]
public abstract class ThingModel : BaseModel
{
    public ThingModel(string id, string name, string commandHint)
    {
        Id = id;
        Name = name;
        CommandHint = commandHint;
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
