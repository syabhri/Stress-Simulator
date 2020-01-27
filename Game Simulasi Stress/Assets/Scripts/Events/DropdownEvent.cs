using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class DropdownEvent : MonoBehaviour
{
    public UnityEvent[] unityEvent;

    public void InvokeSelected(int selected)
    {
        unityEvent[selected].Invoke();
    }
}
