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
    /// Controller name
    /// </summary>
    public BaseController Controller { get; private set; }

    public void Notify(string eventPath, object source, params object[] data)
    {
        switch (eventPath)
        {
            case ControllerNotification.SyncViewState:
                var healthObject = GameObject.FindGameObjectWithTag(UnityObjectTagName.HealthBar);
                var staminaObject = GameObject.FindGameObjectWithTag(UnityObjectTagName.StaminaBar);

                if (healthObject != null)
                    SetSliderValue(healthObject, (int)data[0]);

                if (staminaObject)
                    SetSliderValue(staminaObject, (int)data[1]);

                break;
        }
    }

    public void SetController(BaseController controller)
    {
        if (Controller != null)
            throw new ApplicationException("Controller has been already initialized.");

        Controller = controller;
    }

    private void SetSliderValue(GameObject gameObject, int value)
    {
        var slider = gameObject.GetComponent<Slider>();
        if (slider != null)
            slider.value = value;
    }

    public void Dispose()
    {
        Controller = null;
    }
}
