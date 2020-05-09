using UnityEngine;
using System;

public abstract class VariableContainer1<T> : ScriptableObject
{
#if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "";
#endif

    [SerializeField]
    protected T value;
    public Action<string> OnValueChanges = delegate { };

    public virtual T Value
    {
        set { this.value = value; OnValueChanges.Invoke(name); }
        get { return value; }
    }
}
