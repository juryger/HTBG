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
                var healthObject = GameObject.FindGameObjectWithTag(UnityObjectTagName.HealthBar);
                var staminaObject = GameObject.FindGameObjectWithTag(UnityObjectTagName.StaminaBar);

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

                if (healthObject != null)
                    SetSliderValue(healthObject, (int)data[2]);

                if (staminaObject)
                    SetSliderValue(staminaObject, (int)data[3]);

                break;
        }
    }

    public void SetViewModel(BaseViewModel viewModel)
    {
        if (ViewModel != null)
            throw new ApplicationException("ViewModel has been already initialized.");

        ViewModel = viewModel;
    }

    private void SetSliderValue(GameObject gameObject, int value)
    {
        var slider = gameObject.GetComponent<Slider>();
        if (slider != null)
            slider.value = value;
    }

    public void Dispose()
    {
        ViewModel = null;
    }
}
