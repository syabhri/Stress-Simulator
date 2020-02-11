using UnityEngine;
using System;

[CreateAssetMenu(menuName = "VariableContainer/FloatContainer")]
public class FloatContainer : VariableContainer<float>
{
    public Action<FloatContainer> OnValueChanged = delegate { };

    public override float Value
    {
        set { this.value = value; OnValueChanged.Invoke(this); }
        get { return value; }
    }

    public void AddAmount(float amount)
    {
        Value += amount;
    }

    public void AddAmount(FloatContainer amount)
    {
        Value += amount.Value;
    }
}