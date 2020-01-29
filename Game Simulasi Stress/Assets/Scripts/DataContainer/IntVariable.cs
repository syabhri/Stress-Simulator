using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Variables/Int Variable")]
public class IntVariable : ScriptableObject
{
#if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "";
#endif
    public int value;
    public Action<IntVariable> OnValueChange = delegate { };

    public void SetValue(int value)
    {
        this.value = value;
        OnValueChange.Invoke(this);
    }

    public void SetValue(IntVariable value)
    {
        this.value = value.value;
        OnValueChange.Invoke(this);
    }

    public void ApplyChange(int amount)
    {
        this.value += amount;
    }

    public void ApplyChange(IntVariable amount)
    {
        this.value += amount.value;
    }
}
