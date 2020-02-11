using UnityEngine;

public abstract class VariableContainer<T> : ScriptableObject
{
    #if UNITY_EDITOR
        [Multiline]
        public string DeveloperDescription = "";
    #endif

    [SerializeField]
    protected T value;

    public virtual T Value
    {
        set { this.value = value; }
        get { return value; }
    }
}
