using UnityEngine;
using System;

[CreateAssetMenu(menuName = "VariableContainer/BoolContainer")]
public class BoolContainer : VariableContainer<bool>
{
    public Action<BoolContainer> OnValueChanged = delegate { };

    public override bool Value
    {
        set { this.value = value; OnValueChanged.Invoke(this); }
        get { return value; }
    }

    public void Toggle()
    {
        if (Value)
            Value = false;
        else
            Value = true;
    }
}