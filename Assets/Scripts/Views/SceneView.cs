using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Unity script for Scene View.
/// </summary>
public class SceneView : MonoBehaviour, IView
{
    public void Start()
    {
    }

    /// <summary>
    /// ViewModel name
    /// </summary>
    public BaseViewModel ViewModel { get; private set; }

    public void Notify(string eventPath, object source, params object[] data)
    {
        switch (eventPath)
        {
            case NotificationName.NotifyViewSyncState:
                var sceneTitleText = GameObject.Find("SceneTitleText");
                var commandHintsText = GameObject.Find("SceneCommandHintsText");

                if (sceneTitleText != null)
                {
                    var textComponent = sceneTitleText.GetComponent<Text>();
                    textComponent.text = (string)data[0];
                }

                if (commandHintsText != null)
                {
                    var textComponent = commandHintsText.GetComponent<Text>();
                    textComponent.text = (string)data[1];
                }
                break;
            default:
                break;
        }
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
