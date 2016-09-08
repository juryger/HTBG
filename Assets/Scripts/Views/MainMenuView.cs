using System;
using UnityEngine;

/// <summary>
/// Unity script for main menu screen.
/// </summary>
public class MainMenuView : MonoBehaviour, IView
{
    public BaseViewModel ViewModel { get; private set; }

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

    public void SetViewModel(BaseViewModel viewModel)
    {
        if (ViewModel != null)
            throw new ApplicationException("ViewModel has been already initialized.");

        ViewModel = viewModel;
    }

    public void Dispose()
    {
        ViewModel = null;
    }
}
