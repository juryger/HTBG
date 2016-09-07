public class PlayerController : BaseController
{
    public PlayerModel Model { get; private set; }

    public PlayerController(IView view, PlayerModel model) :
        base(view, model)
    {
        Model = model;
        Model.SetController(this);
    }

    public override void Notify(string eventPath, object source, params object[] data)
    {
        switch (eventPath)
        {
            case ControllerNotification.PlayerPositionChanged:
                var pos = data[0] as UnityPosition;
                View.Notify(eventPath, source, pos.X, pos.Y, pos.Z);
                break;
            case ControllerNotification.PlayerMovementVectorChanged:
                var vector = data[0] as UnityVector2;
                View.Notify(eventPath, source, vector.X, vector.Y);
                break;
            case ControllerNotification.PlayerMovementHaulted:
                View.Notify(eventPath, source);
                break;
            default:
                break;
        }
    }
}