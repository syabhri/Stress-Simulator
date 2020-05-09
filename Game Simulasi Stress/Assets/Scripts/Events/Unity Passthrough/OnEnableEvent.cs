﻿using UnityEngine;
using UnityEngine.Events;

public class OnEnableEvent : MonoBehaviour
{
    public UnityEvent onEnable;

    private void OnEnable()
    {
        onEnable.Invoke();
    }
}
