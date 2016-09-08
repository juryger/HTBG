public class SceneViewModel : BaseViewModel
{
    public SceneModel Model { get; private set; }

    public SceneViewModel(IView view, SceneModel model) :
        base(view, model)
    {
        Model = model;
        Model.SetViewModel(this);

        Notify(ViewModelNotification.SyncViewState, this, Model.Player.Health, Model.Player.Stamina);
    }

    public override void Notify(string eventPath, object source, params object[] data)
    {
        switch (eventPath)
        {
            case ViewModelNotification.SyncViewState:
                View.Notify(ViewModelNotification.SyncViewState, this, data);
                break;
        }
    }
}
