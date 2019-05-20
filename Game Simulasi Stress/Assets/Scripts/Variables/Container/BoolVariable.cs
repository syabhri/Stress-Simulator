using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Bool Variable")]
public class BoolVariable : ScriptableObject
{
#if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "";
#endif

    public bool value;

    public void SetBool(bool value)
    {
        this.value = value;
    }

    public void ToggleBool()
    {
        if (value)
            value = false;
        else
            value = true;
    }
}
