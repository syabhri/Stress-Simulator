using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputEvent : MonoBehaviour
{
    public string buttonName;
    [Space]
    public UnityEvent unityEvent;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(buttonName))
        {
            unityEvent.Invoke();
        }
    }
}
