using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Variables/StringList Variable")]
public class StringListVariable : ScriptableObject
{
    [SerializeField]
    private List<string> values = new List<string>();
    public Action<StringListVariable> OnValueChange = delegate { };

    public List<string> Values
    {
        get { return values; }
        set { values = new List<string>(value); OnValueChange.Invoke(this); }
    }
}
