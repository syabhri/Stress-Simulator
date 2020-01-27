using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Variables/Bool Variable")]
public class BoolVariable : ScriptableObject
{
#if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "";
#endif

    public bool value;
    public Action<BoolVariable> OnValueChange = delegate { };

    public void SetBool(bool value)
    {
        this.value = value;
        OnValueChange.Invoke(this);
    }

    public void ToggleBool()
    {
        if (value)
            value = false;
        else
            value = true;
        OnValueChange.Invoke(this);
    }
}
