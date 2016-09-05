using UnityEngine;
using System.Collections;
using System;

public class PlayerController : BaseController
{
    public PlayerModel Model { get; private set; }

    public PlayerController(IView view, PlayerModel model) : base(view)
    {
        Model = model;
        Model.SetController(this);
    }

    public override void Notify(string eventPath, object source, params object[] data)
    {
        switch (eventPath)
        {
            case ControllerNotification.GameObjectPositionChanged:
                var pos = data[0] as UnityPosition;
                View.Notify(eventPath, source, pos.X, pos.Y, pos.Z);
                break;
            case ControllerNotification.GameObjectMovementVectorChanged:
                var vector = data[0] as UnityVector2;
                View.Notify(eventPath, source, vector.X, vector.Y);
                break;
            case ControllerNotification.GameObjectMovementHalted:
                View.Notify(eventPath, source);
                break;
            default:
                break;
        }
    }
}