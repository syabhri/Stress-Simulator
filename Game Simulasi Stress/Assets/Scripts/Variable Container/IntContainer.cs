using UnityEngine;

[CreateAssetMenu(menuName = "Variables /Int Container")]
public class IntContainer : VariableContainer<int>
{
    public void AddAmount(int amount)
    {
        Value += amount;
    }

    public void AddAmount(IntContainer amount)
    {
        Value += amount.Value;
    }
}
