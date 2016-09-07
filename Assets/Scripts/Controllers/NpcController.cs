﻿using System;

public class NpcController : BaseController
{
    public NpcModel Model { get; private set; }

    public NpcController(IView view, NpcModel model) :
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