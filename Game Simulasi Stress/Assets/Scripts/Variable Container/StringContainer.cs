using UnityEngine;
using System;

[CreateAssetMenu(menuName = "VariableContainer/StringContainer")]
public class StringContainer : VariableContainer<string>
{
    public Action<StringContainer> OnValueChanged = delegate { };

    public override string Value
    {
        set { this.value = value; OnValueChanged.Invoke(this); }
        get { return value; }
    }
}