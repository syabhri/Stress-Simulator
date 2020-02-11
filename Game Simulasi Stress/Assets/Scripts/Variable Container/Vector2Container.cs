using UnityEngine;
using System;

[CreateAssetMenu(menuName = "VariableContainer/Vector2Container")]
public class Vector2Container : VariableContainer<Vector2>
{
    public Action<Vector2Container> OnValueChanged = delegate { };

    public override Vector2 Value
    {
        set { this.value = value; OnValueChanged.Invoke(this); }
        get { return value; }
    }

    public void SetPosition(float x, float y)
    {
        if (value != null) {
            value.x = x;
            value.y = y;
        }
        else {
            value = new Vector2(x, y);
        }
    }
}
