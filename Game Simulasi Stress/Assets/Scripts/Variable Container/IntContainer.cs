using UnityEngine;
using System;

[CreateAssetMenu(menuName = "VariableContainer/IntContainer")]
public class IntContainer : VariableContainer<int>
{
    public Action<IntContainer> OnValueChanged = delegate { };

    public override int Value
    {
        set { this.value = value; OnValueChanged.Invoke(this); }
        get { return value; }
    }

    public void AddAmount(int amount)
    {
        Value += amount;
    }

    public void AddAmount(IntContainer amount)
    {
        Value += amount.Value;
    }
}
