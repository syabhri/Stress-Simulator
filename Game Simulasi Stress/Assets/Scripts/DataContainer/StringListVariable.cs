using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Variables/StringList Variable")]
public class StringListVariable : ScriptableObject
{
    [SerializeField]
    public List<string> values = new List<string>();

    public List<string> Values
    {
        get { return values; }
        set { values = new List<string>(value); }
    }
}
