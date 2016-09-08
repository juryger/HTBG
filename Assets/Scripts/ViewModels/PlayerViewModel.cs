public class PlayerViewModel : BaseViewModel
{
    public PlayerModel Model { get; private set; }

    public PlayerViewModel(IView view, PlayerModel model) :
        base(view, model)
    {
        Model = model;
        Model.SetViewModel(this);
    }

    public override void Notify(string eventPath, object source, params object[] data)
    {
        switch (eventPath)
        {
            case ViewModelNotification.PlayerPositionChanged:
                var pos = data[0] as UnityPosition;
                View.Notify(eventPath, source, pos.X, pos.Y, pos.Z);
                break;
            case ViewModelNotification.PlayerMovementVectorChanged:
                var vector = data[0] as UnityVector2;
                View.Notify(eventPath, source, vector.X, vector.Y);
                break;
            case ViewModelNotification.PlayerMovementHaulted:
                View.Notify(eventPath, source);
                break;
            default:
                break;
        }
    }
}