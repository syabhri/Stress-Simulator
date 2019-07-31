using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputEvent : MonoBehaviour
{
    public string buttonName;

    [Header("Input Group")]
    public bool useInputGroup;
    [Tooltip("group with same number can Listen to an input at the same time. 0 is default input group")]
    public int inputGroup;
    [Tooltip("currently Listening input group")]
    public IntVariable activeInputGroup;

    [Header("Events")]
    public UnityEvent unityEvent;

    // Update is called once per frame
    void Update()
    {
        if (useInputGroup)
        {
            if (Input.GetButtonDown(buttonName) && activeInputGroup.value == inputGroup)
            {
                unityEvent.Invoke();
            }
        }
        else
        {
            if (Input.GetButtonDown(buttonName))
            {
                unityEvent.Invoke();
            }
        }
    }
     
    public void ActivateInputGroup()
    {
        activeInputGroup.value = inputGroup;
    }

    public void DeactivateInputGroup()
    {
        activeInputGroup.value = 0;
    }
}
