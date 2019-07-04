using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventRaiser : MonoBehaviour
{
    [Tooltip("invoke event when disabled")]
    public bool onDisable;
    [Tooltip("invoke event when enabled")]
    public bool onEnable;
    public UnityEvent unityEvents;

    private void OnEnable()
    {
        if (onEnable)
        {
            OnGameEventRaised();
        }
    }

    private void OnDisable()
    {
        if (onDisable)
        {
            OnGameEventRaised();
        }
    }

    public void OnGameEventRaised()
    {
        unityEvents.Invoke();
    }

    /* reserved
    public void OnGameEventRaised()
    {
        foreach (UnityEvent unityEvent in unityEvents)
        {
            unityEvent.Invoke();
        }
    }

    public void OnGameEventRaised(int i)
    {
        unityEvents[i].Invoke();
    }
    */
}
