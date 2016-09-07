using System;
using UnityEngine;

/// <summary>
/// Unity script for NPC object.
/// </summary>
public class NpcView : MonoBehaviour, IView
{
    public BaseController Controller { get; private set; }

    public object[] GetViewData(string viewDataPath)
    {
        throw new NotImplementedException();
    }

    public void Notify(string eventPath, object source, params object[] data)
    {
        throw new NotImplementedException();
    }

    public void SetController(BaseController controller)
    {
        if (Controller != null)
            throw new ApplicationException("Controller has been already initialized.");

        Controller = controller;
    }

    public void Dispose()
    {
        Controller = null;
    }
}
