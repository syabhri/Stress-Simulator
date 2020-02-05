using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Variables/String Variable")]
public class StringVariable : ScriptableObject
{
    [SerializeField]
    private string value = "";
    public Action<StringVariable> OnValueChange = delegate { };

    public string Value
    {
        get { return value; }
        set { this.value = value; OnValueChange.Invoke(this); }
    }

    public StringVariable ValueExt
    {
        set { this.value = value.value; OnValueChange.Invoke(this); }
    }
}
