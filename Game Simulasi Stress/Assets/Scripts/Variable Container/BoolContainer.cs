using UnityEngine;

[CreateAssetMenu(menuName = "Variables /Bool Container")]
public class BoolContainer : VariableContainer<bool>
{
    public void Toggle()
    {
        if (Value)
            Value = false;
        else
            Value = true;
    }
}
