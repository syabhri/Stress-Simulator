using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputEvent : MonoBehaviour
{
    public string buttonName;

    [Tooltip("group with same number can Listen to an input at the same time. 0 is default input group")]
    public int inputGroup;
    [Tooltip("Activate Input Group Upon Enable")]
    public bool ActivateOnEnable;

    public static int activeInputGroup;

    [Header("Events")]
    public UnityEvent unityEvent;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(buttonName) && activeInputGroup == inputGroup)
        {
            unityEvent.Invoke();
        }
    }

    private void OnEnable()
    {
        if (ActivateOnEnable)
        {
            ActivateInputGroup();
        } 
    }

    private void OnDisable()
    {
        if (ActivateOnEnable)
        {
            DeactivateInputGroup();
        }
    }

    public void ActivateInputGroup()
    {
        activeInputGroup = inputGroup;
        Debug.Log("active Input Group = " + activeInputGroup);
    }

    public void DeactivateInputGroup()
    {
        activeInputGroup = 0;
        Debug.Log("active Input Group = 0");
    }

    public void setInputGroup(int inputGroup)
    {
        activeInputGroup = inputGroup;
        Debug.Log("active Input Group = " + activeInputGroup);
    }
}
