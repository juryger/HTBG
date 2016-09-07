using System;

public class InventoryController : BaseController
{
    public InventoryModel Model { get; private set; }

    public InventoryController(IView view, InventoryModel model) :
        base(view, model)
    {
        Model = model;
        Model.SetController(this);
    }

    public override void Notify(string eventPath, object source, params object[] data)
    {
        throw new NotImplementedException();
    }
}
