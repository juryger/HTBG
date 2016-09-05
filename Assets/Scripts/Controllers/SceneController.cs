using UnityEngine;
using System.Collections;
using System;

public class SceneController : BaseController
{
    public ScenModel Model { get; private set; }

    public SceneController(IView view, ScenModel model) : base(view)
    {
        Model = model;
        Model.SetController(this);
    }

    public override void Notify(string eventPath, object source, params object[] data)
    {
        throw new NotImplementedException();
    }
}
