using UnityEngine;

[CreateAssetMenu(menuName = "Variables /GameObject Container")]
public class GameObjectContainer : VariableContainer<GameObject>
{
    public void Clear()
    {
        value = null;
    }
}
