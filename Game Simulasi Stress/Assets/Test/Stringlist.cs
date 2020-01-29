using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "ScriptableObject/StringList")]
public class Stringlist : ScriptableObject
{
    public Action<Stringlist> OnValueChange = delegate { };

    public List<string> Values
    {
        get { return Values; }
        set { new List<string>(value); OnValueChange.Invoke(this); }
    }
}
