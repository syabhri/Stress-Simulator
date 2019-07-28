using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColliderEvent : MonoBehaviour
{
    [Tooltip("use triggered colider instead")]
    public bool onTrigger;
    public GameEvent ExternalEvent;
    public UnityEvent LocalEvent;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("ping");
        if (!onTrigger)
        {
            if (ExternalEvent != null)
            {
                ExternalEvent.Raise();
            }
            else
            {
                LocalEvent.Invoke();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (onTrigger)
        {
            if (ExternalEvent != null)
            {
                ExternalEvent.Raise();
            }
            else
            {
                LocalEvent.Invoke();
            }
        }
    }
}
