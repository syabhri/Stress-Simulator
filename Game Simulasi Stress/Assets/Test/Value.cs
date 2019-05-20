using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewValue", menuName = "ScriptableObject/Value")]
public class Value : ScriptableObject
{
    [Range(0,1)]
    public float value;

    public void changevalue(float value)
    {
        this.value = value;
    }
}
