using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "VariableContainer/StringListContainer")]
public class StringListContainer : VariableContainer<List<string>>
{
    public Action<StringListContainer> OnValueChanged = delegate { };

    public override List<string> Value
    {
        set { this.value = value; OnValueChanged.Invoke(this); }
        get { return value; }
    }
}
