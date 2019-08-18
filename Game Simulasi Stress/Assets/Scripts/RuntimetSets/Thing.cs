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

    [Tooltip("Used if theres only one object per set")]
    public bool isSingle;

    [Tooltip("Select weather or not the object is removed From The set while disabled")]
    public bool removeOnDisabled;

    [Tooltip("Select Wether or no the object is disabled after initialized")]
    public bool isDisabled;

    private void OnEnable()
    {
        if (removeOnDisabled)
        {
            AddToSet();
        } 
    }
            

    private void Awake()
    {
        if (isDisabled)
            gameObject.SetActive(false);

        if (!removeOnDisabled)
        {
            AddToSet();
        }
    }

    private void OnDisable()
    {
        if (removeOnDisabled)
        {
            RemoveFromSet();
        }
    }

    private void OnDestroy()
    {
        RemoveFromSet();
    }

    public void AddToSet()
    {
        if (isSingle)
        {
            RuntimeSet.Item = gameObject;
            RuntimeSet.isSingle = true;
        }
        else
        {
            RuntimeSet.Add(gameObject);
        }
    }

    public void RemoveFromSet()
    {
        if (RuntimeSet.isSingle)
            RuntimeSet.Item = null;
        else
            RuntimeSet.Remove(gameObject);
    }
}