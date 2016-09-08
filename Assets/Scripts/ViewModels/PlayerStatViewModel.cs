using System;

public class PlayerStatViewModel : BaseViewModel
{
    public PlayerStatisticsModel Model { get; private set; }

    public PlayerStatViewModel(IView view, PlayerStatisticsModel model) :
        base(view, model)
    {
        Model = model;
        Model.SetViewModel(this);
    }

    public override void Notify(string eventPath, object source, params object[] data)
    {
        throw new NotImplementedException();
    }
}