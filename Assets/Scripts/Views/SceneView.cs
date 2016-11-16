using System;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Unity script for Scene View.
/// </summary>
public class SceneView : MonoBehaviour, IView
{
    // Idea for executing coroutine by timer (background thread) are grabed from
    // http://stackoverflow.com/questions/22513881/unity3d-how-to-process-events-in-the-correct-thread#answer-25271152
    private System.Object m_queueLock = new System.Object();
    private List<Action> m_queuedEvents = new List<Action>();
    private List<Action> m_executingEvents = new List<Action>();

    // Used to request state of multiplayers from game server.
    private Timer multiplayersTimer;

    /// <summary>
    /// Constructor.
    /// </summary>
    public void Start()
    {
        multiplayersTimer = new Timer(1500);
        multiplayersTimer.Elapsed += (sender, args) => { QueueEvent(new Action(RefreshGamePlayersState)); };
        multiplayersTimer.Start();
    }

    void Update()
    {
        MoveQueuedEventsToExecuting();

        while (m_executingEvents.Count > 0)
        {
            Action e = m_executingEvents[0];
            m_executingEvents.RemoveAt(0);
            e();
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

    public void QueueEvent(Action action)
    {
        lock (m_queueLock)
        {
            m_queuedEvents.Add(action);
        }
    }

    private void MoveQueuedEventsToExecuting()
    {
        lock (m_queueLock)
        {
            while (m_queuedEvents.Count > 0)
            {
                Action e = m_queuedEvents[0];
                m_executingEvents.Add(e);
                m_queuedEvents.RemoveAt(0);
            }
        }
    }

    /// <summary>
    /// Requests a state of players from game server.
    /// </summary>
    private void RefreshGamePlayersState()
    {
        var gameManager = GameStateManager.Instance;
        StartCoroutine(
            gameManager.GameServer.GetGamePlayers(
                gameManager.ActiveScene.Id));
    }
}
