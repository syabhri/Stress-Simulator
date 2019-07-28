using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OpenDoor : MonoBehaviour
{
    public string triggerButton = "Submit";
    public UnityEvent unityEvent;

    private bool isInRange = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isInRange = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isInRange = false;
    }

    private void Update()
    {
        if (isInRange)
        {
            if (Input.GetButton(triggerButton))
            {
                unityEvent.Invoke();
            }
        }
    }
}
