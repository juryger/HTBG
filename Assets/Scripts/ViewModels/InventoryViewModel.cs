using System;

public class InventoryViewModel : BaseViewModel
{
    public InventoryModel Model { get; private set; }

    public InventoryViewModel(IView view, InventoryModel model) :
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
