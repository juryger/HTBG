public class CharacterViewModel : BaseViewModel
{
    public CharacterModel Model { get; private set; }

    public CharacterViewModel(IView view, CharacterModel model) :
        base(view, model)
    {
        Model = model;
        Model.SetViewModel(this);

        // notify view about intial state upate
        Notify(NotificationName.CharacterPositionChanged, this, model.Position);
        Notify(NotificationName.CharacterStatisticsChanged, this, model.Statistics);
    }

    public override void Notify(string eventPath, object source, params object[] data)
    {
        switch (eventPath)
        {
            case NotificationName.RemoveCharacterFromScene:
                View.Notify(eventPath, source);
                break;
            case NotificationName.CharacterPositionChanged:
                var pos = data[0] as UnityPosition;
                View.Notify(eventPath, source, pos.X, pos.Y, pos.Z);
                break;
            case NotificationName.CharacterMovementVectorChanged:
                var vector = data[0] as UnityVector2;
                var isRun = (bool)data[1];
                View.Notify(eventPath, source, vector.X, vector.Y, isRun);
                break;
            case NotificationName.CharacterMovementHaulted:
                View.Notify(eventPath, source);
                break;
            case NotificationName.CharacterStatisticsChanged:
                var statistics = data[0] as CharacterStatisticsModel;
                View.Notify(eventPath, source, statistics);
                break;
            case NotificationName.RequestCharacterPosition:
                View.Notify(eventPath, source);
                break;
            case NotificationName.ResponseCharacterPosition:
                Model.SetPosition(
                    new UnityPosition(
                        float.Parse(data[0].ToString()),
                        float.Parse(data[1].ToString()),
                        0),
                    false);
                break;
            default:
                break;
        }
    }
}