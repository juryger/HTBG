public class PlayerViewModel : BaseViewModel
{
    public CharacterModel Model { get; private set; }

    public PlayerViewModel(IView view, CharacterModel model) :
        base(view, model)
    {
        Model = model;
        Model.SetViewModel(this);
    }

    public override void Notify(string eventPath, object source, params object[] data)
    {
        switch (eventPath)
        {
            case NotificationName.PlayerPositionChanged:
                var pos = data[0] as UnityPosition;
                View.Notify(eventPath, source, pos.X, pos.Y, pos.Z);
                break;
            case NotificationName.PlayerMovementVectorChanged:
                var vector = data[0] as UnityVector2;
                var isRun = (bool)data[1];
                View.Notify(eventPath, source, vector.X, vector.Y, isRun);
                break;
            case NotificationName.PlayerMovementHaulted:
                View.Notify(eventPath, source);
                break;
            case NotificationName.PlayerStatisticsChanged:
                View.Notify(eventPath, source, Model.Statistics);
                break;
            case NotificationName.RequestPlayerPosition:
                View.Notify(eventPath, source);
                break;
            case NotificationName.ResponsePlayerPosition:
                Model.SetPosition(
                    new UnityPosition(
                        float.Parse(data[0].ToString()),
                        float.Parse(data[1].ToString()),
                        0));
                break;
            default:
                break;
        }
    }
}