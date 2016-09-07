using System;
using UnityEngine;

/// <summary>
/// Unity script for main menu screen.
/// </summary>
public class MainMenuView : MonoBehaviour, IView
{
    public BaseController Controller { get; private set; }

    public object[] GetViewData(string viewDataPath)
    {
        throw new NotImplementedException();
    }

    public void Notify(string eventPath, object source, params object[] data)
    {
        // todo: find item with required name and update its visibility
        //gameObject.GetComponentInChildren<T>

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
