using System;

public class NpcViewModel : BaseViewModel
{
    public NpcModel Model { get; private set; }

    public NpcViewModel(IView view, NpcModel model) :
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