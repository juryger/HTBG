using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Base class for all objects in the game.
/// </summary>
public abstract class ThingModel: IModel
{
    protected string internalKey;
    public BaseController Controller { get; private set; }

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

    public void SetController(BaseController controller)
    {
        if (Controller != null)
            throw new ApplicationException("Controller has been already initialized.");

        Controller = controller;
    }

    public void SetPosition(UnityPosition pos)
    {
        Position = pos;

        if (Controller != null)
        {
            Controller.Notify(ControllerNotification.GameObjectPositionChanged, this, pos);
        }
    }

    public void SetMovementVector(UnityVector2 vector)
    {
        MovementVector = vector;

        if (Controller != null)
        {
            if (vector.X == 0 && vector.Y == 0)
                Controller.Notify(ControllerNotification.GameObjectMovementHalted, this);
            else
                Controller.Notify(ControllerNotification.GameObjectMovementVectorChanged, this, vector);
        }
    }
}
