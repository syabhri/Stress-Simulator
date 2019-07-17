using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderEvent : MonoBehaviour
{
    public bool onTrigger;
    public GameEvent gameEvent;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("ping");
        if (!onTrigger)
        {
            gameEvent.Raise();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (onTrigger)
        {
            gameEvent.Raise();
        }
    }
}
