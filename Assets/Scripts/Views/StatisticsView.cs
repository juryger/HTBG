﻿using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Unity script for statistics window.
/// </summary>
public class StatisticsView : MonoBehaviour, IView
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

    public void SyncState()
    {
        throw new NotImplementedException();
    }

    // Use this for initialization
    void Start () {
    
    }
    
    // Update is called once per frame
    void Update () {
    
    }
}