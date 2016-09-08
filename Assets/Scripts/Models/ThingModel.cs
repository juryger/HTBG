using System;

/// <summary>
/// Base class for all objects in the game.
/// </summary>
[Serializable]
public abstract class ThingModel : BaseModel
{
    protected string internalKey;

    public ThingModel(string key, string name, string commandHint)
    {
        internalKey = String.Format("{0}.{1}", key, DateTime.Now.Ticks);
        Name = name;
        CommandHint = commandHint;
    }

    public string Name { get; private set; }

    public string CommandHint { get; private set; }

    /// <summary>
    /// Location on the scene
    /// </summary>
    public UnityPosition Position { get; private set; }

    /// <summary>
    /// Location on the scene
    /// </summary>
    public UnityVector2 MovementVector { get; private set; }

    public string Key
    {
        get { return internalKey; }
    }

    public void SetPosition(UnityPosition pos)
    {
        Position = pos;

        if (ViewModel != null)
        {
            ViewModel.Notify(ViewModelNotification.PlayerPositionChanged, this, pos);
        }
    }

    public void SetMovementVector(UnityVector2 vector)
    {
        MovementVector = vector;

        if (ViewModel != null)
        {
            if (vector.X == 0 && vector.Y == 0)
                ViewModel.Notify(ViewModelNotification.PlayerMovementHaulted, this);
            else
                ViewModel.Notify(ViewModelNotification.PlayerMovementVectorChanged, this, vector);
        }
    }
}
