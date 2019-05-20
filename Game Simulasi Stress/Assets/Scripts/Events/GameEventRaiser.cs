using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventRaiser : MonoBehaviour
{
    public UnityEvent[] unityEvent;

    public void OnGameEventRaised(int i)
    {
        unityEvent[i].Invoke();
    }
}
