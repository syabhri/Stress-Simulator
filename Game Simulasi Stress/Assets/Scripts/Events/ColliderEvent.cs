using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColliderEvent : MonoBehaviour
{
    [Tooltip("use triggered colider instead")]
    public bool onTrigger;
    public UnityEvent enterEvent;
    public UnityEvent exitEvent;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!onTrigger)
        {
            enterEvent.Invoke();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!onTrigger)
        {
            exitEvent.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (onTrigger)
        {
            enterEvent.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (onTrigger)
        {
            exitEvent.Invoke();
        }
    }
}
