using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Unity script for scene.
/// </summary>
public class SceneView : MonoBehaviour, IView
{
    public BaseController Controller { get; private set; }

    public object[] GetViewData(string viewDataPath)
    {
        var result = new object[] { };

        switch(viewDataPath)
        {
            case ViewDataName.GlobalMapConnection1:
                // todo: GetComponents<MapPoint>
                break;
            default:
                break;
        }

        return result;
    }

    public void Notify(string eventPath, object source, params object[] data)
    {
        switch(eventPath)
        {
            case ControllerNotification.SyncViewState:
                break;
        }
    }

    public void SetController(BaseController controller)
    {
        if (Controller != null)
            throw new ApplicationException("Controller has been already initialized.");

        Controller = controller;
    }

    public void SyncState()
    {
        if (Controller != null)
            Controller.Notify(ControllerNotification.SyncViewState, this);
    }
}
