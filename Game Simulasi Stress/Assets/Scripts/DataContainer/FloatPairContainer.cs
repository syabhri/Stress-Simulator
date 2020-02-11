using UnityEngine;
using System;

[CreateAssetMenu(menuName = "VariableContainer/FloatPairContainer")]
public class FloatPairContainer : VariableContainer<FloatPair>
{
    public Action<FloatPairContainer> OnValueChanged = delegate { };

    public override FloatPair Value
    {
        set { this.value = value; OnValueChanged.Invoke(this); }
        get { return value; }
    }
}
