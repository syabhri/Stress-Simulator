using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class DropdownEvent : MonoBehaviour
{
    public TMP_Dropdown dropdown;

    public UnityEvent[] unityEvent;

    public void InvokeSelected()
    {
        unityEvent[dropdown.value].Invoke();
    }

}
