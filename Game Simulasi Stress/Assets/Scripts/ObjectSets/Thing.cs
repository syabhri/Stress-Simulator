// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
//
// Modified By Syabhri Mustafa
// Date:    18/05/19
// ----------------------------------------------------------------------------

using UnityEngine;

public class Thing : MonoBehaviour
{
    public ThingRuntimeSet RuntimeSet;

    [Tooltip("Select weather or not the object is removed From The set when disabled")]
    public bool WhileEnabled;

    [Tooltip("Wether or no the object is disabled OnStart")]
    public bool IsDisabled;

    private void OnEnable()
    {
        if (WhileEnabled)
            RuntimeSet.Add(this);
    }

    private void OnDisable()
    {
        if (WhileEnabled)
            RuntimeSet.Remove(this);
    }

    private void Start()
    {
        if (!WhileEnabled)
            RuntimeSet.Add(this);
        if (IsDisabled)
            gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        if (!WhileEnabled)
            RuntimeSet.Remove(this);
    }

}