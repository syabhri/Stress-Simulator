using UnityEngine;
using System;

public abstract class VariableContainer<T> : ScriptableObject
{
    #if UNITY_EDITOR
        [Multiline]
        public string DeveloperDescription = "";
    #endif

    protected T value;
    public Action<T> OnValueChanges = delegate { };

    public T Value
    {
        set { this.value = value; OnValueChanges.Invoke(value); }
        get { return value; }
    }

    public VariableContainer<T> ValueExt
    {
        set { Value = value.value; }
    }
}
