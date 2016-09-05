using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class PlayerStatController : BaseController
{
    public PlayerStatisticsModel Model { get; private set; }

    public PlayerStatController(IView view, PlayerStatisticsModel model) : base(view)
    {
        Model = model;
        Model.SetController(this);
    }

    public override void Notify(string eventPath, object source, params object[] data)
    {
        throw new NotImplementedException();
    }
}