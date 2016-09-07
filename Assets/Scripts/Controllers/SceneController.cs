public class SceneController : BaseController
{
    public SceneModel Model { get; private set; }

    public SceneController(IView view, SceneModel model) :
        base(view, model)
    {
        Model = model;
        Model.SetController(this);

        Notify(ControllerNotification.SyncViewState, this, Model.Player.Health, Model.Player.Stamina);
    }

    public override void Notify(string eventPath, object source, params object[] data)
    {
        switch (eventPath)
        {
            case ControllerNotification.SyncViewState:
                View.Notify(ControllerNotification.SyncViewState, this, data);
                break;
        }
    }
}
