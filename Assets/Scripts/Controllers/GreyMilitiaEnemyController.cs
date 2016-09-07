﻿using System;

public class GreyMilitiaEnemyController : BaseController
{
    public GreyMilitiaEnemyModel Model { get; private set; }

    public GreyMilitiaEnemyController(IView view, GreyMilitiaEnemyModel model) :
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