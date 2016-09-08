using System;

public class LootViewModel : BaseViewModel
{
    public LootModel Model { get; private set; }

    public LootViewModel(IView view, LootModel model) :
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