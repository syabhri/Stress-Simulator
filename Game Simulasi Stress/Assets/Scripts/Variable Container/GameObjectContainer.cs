using UnityEngine;
using System;

[CreateAssetMenu(menuName = "VariableContainer/GameObjectContainer")]
public class GameObjectContainer : VariableContainer<GameObject>
{
    public Action<GameObjectContainer> OnValueChanged = delegate { };

    public override GameObject Value
    {
        set { this.value = value; OnValueChanged.Invoke(this); }
        get { return value; }
    }

    public void SetActive(bool value)
    {
        Value.SetActive(value);
    }

    public void Clear()
    {
        value = null;
    }
}
