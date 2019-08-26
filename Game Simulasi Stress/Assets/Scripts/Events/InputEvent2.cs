using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputEvent2 : MonoBehaviour
{
    public string buttonName;

    [Tooltip("Higher number higher priority")]
    public int inputPriority;
    [Tooltip("currently Listening input group")]
    public static int activeInput;

    private static List<int> activePriority;

    [Header("Events")]
    public UnityEvent unityEvent;

    // Update is called once per frame
    void Update()
    {
        
    }

    
     
    public void ActivateInput()
    {
        if (activeInput < inputPriority)
        {
            if (!activePriority.Contains(inputPriority))
            {
                activePriority.Add(inputPriority);
            }
            activeInput = inputPriority;
        }
    }

    public void DeactivateInput()
    {
        activePriority.Remove(inputPriority);
    }
}
