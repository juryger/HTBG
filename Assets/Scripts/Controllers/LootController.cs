using System;

public class LootController : BaseController
{
    public LootModel Model { get; private set; }

    public LootController(IView view, LootModel model) :
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