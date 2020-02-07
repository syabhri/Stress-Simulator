using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

[RequireComponent(typeof(TMP_Dropdown))]
public class DropdownEvent : MonoBehaviour
{
    [Tooltip("Invoke Selected Event on enable")]
    public bool isInvokeOnEnable;
    public UnityEvent[] OnDropdownSelect;

    private TMP_Dropdown dropdown;

    private void Awake()
    {
        dropdown = GetComponent<TMP_Dropdown>();
    }

    private void OnEnable()
    {
        if (isInvokeOnEnable)
            InvokeSelected(dropdown.value);
    }

    public void InvokeSelected(int selected)
    {
        OnDropdownSelect[selected].Invoke();
    }
}
