// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------

using UnityEngine;

[CreateAssetMenu(menuName ="Variables/Float Variable")]
public class FloatVariable : ScriptableObject
{
#if UNITY_EDITOR
[Multiline]
public string DeveloperDescription = "";
#endif
    public float value;

    public void SetValue(float value)
    {
        this.value = value;
    }

    public void SetValue(FloatVariable value)
    {
        this.value = value.value;
    }

    public void ApplyChange(float amount)
    {
        this.value += amount;
    }

    public void ApplyChange(FloatVariable amount)
    {
        this.value += amount.value;
    }
}
