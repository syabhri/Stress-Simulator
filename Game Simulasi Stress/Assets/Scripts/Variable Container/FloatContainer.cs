using UnityEngine;

[CreateAssetMenu(menuName = "Variables /Float Container")]
public class FloatContainer : VariableContainer<float>
{
    public void AddAmount(float amount)
    {
        Value += amount;
    }

    public void AddAmount(FloatContainer amount)
    {
        Value += amount.Value;
    }
}
