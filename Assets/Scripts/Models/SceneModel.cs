using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Represent scene of the game.
/// </summary>
public class ScenModel: IModel
{
    public ScenModel(string name, string[] commandHints)
    {
        this.Name = name;
        this.CommandHints = commandHints ?? new string[] { };
    }

    public BaseController Controller { get; private set; }

    /// <summary>
    /// Name of the scene.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// List of supported command hints.
    /// </summary>
    public string[] CommandHints { get; private set; }

    public void SetController(BaseController controller)
    {
        if (Controller != null)
            throw new ApplicationException("Controller has been already initialized.");

        Controller = controller;
    }
}
