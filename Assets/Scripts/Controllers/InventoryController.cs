using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class InventoryController : BaseController
{
    public InventoryModel Model { get; private set; }

    public InventoryController(IView view, InventoryModel inventory) : base(view)
    {
        Model = Model;
        Model.SetController(this);
    }

    public override void Notify(string eventPath, object source, params object[] data)
    {
        throw new NotImplementedException();
    }
}
