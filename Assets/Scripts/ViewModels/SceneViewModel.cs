public class SceneViewModel : BaseViewModel
{
    public SceneModel Model { get; private set; }

    public SceneViewModel(IView view, SceneModel model) :
        base(view, model)
    {
        Model = model;
        Model.SetViewModel(this);

        Notify(NotificationName.NotifyViewSyncState,
            this,
            GameStateManager.Instance.ActiveScene.Name,
            GameStateManager.Instance.ActiveScene.CommandHints,
            GameStateManager.Instance.Player.Statistics.CurrentHealth,
            GameStateManager.Instance.Player.Statistics.CurrentStamina);
    }

    public override void Notify(string eventPath, object source, params object[] data)
    {
        switch (eventPath)
        {
            case NotificationName.NotifyViewSyncState:
                View.Notify(NotificationName.NotifyViewSyncState, this, data);
                break;
        }
    }
}
