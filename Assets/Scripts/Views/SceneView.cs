using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Unity script for Scene View.
/// </summary>
public class SceneView : MonoBehaviour, IView
{
    /// <summary>
    /// Scene name.
    /// </summary>
    public string SceneName;

    /// <summary>
    /// Scene command hints.
    /// </summary>
    public string[] SceneCommandHints;

    public void Start()
    {
        var commandHints = GameObject.Find("SceneCommandHintsText");
        if (commandHints != null)
        {
            var textComponent = commandHints.GetComponent<Text>();
            textComponent.text = String.Join(" | ", SceneCommandHints ?? new string[] { });
        }
    }

    /// <summary>
    /// ViewModel name
    /// </summary>
    public BaseViewModel ViewModel { get; private set; }

    public void Notify(string eventPath, object source, params object[] data)
    {
        switch (eventPath)
        {
            case ViewModelNotification.SyncViewState:
                var healthObject = GameObject.FindGameObjectWithTag(UnityObjectTagName.HealthBar);
                var staminaObject = GameObject.FindGameObjectWithTag(UnityObjectTagName.StaminaBar);

                if (healthObject != null)
                    SetSliderValue(healthObject, (int)data[0]);

                if (staminaObject)
                    SetSliderValue(staminaObject, (int)data[1]);

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
