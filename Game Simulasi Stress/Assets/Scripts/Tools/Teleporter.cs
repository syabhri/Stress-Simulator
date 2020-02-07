using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Vector2Variable destination;
    public float delay;
    public bool isTrigger;
    public bool isInstant;

    private bool isInRange = false;
    private Transform target;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isTrigger)
        {
            if (isInstant)
            {
                Teleport();
                return;
            }
            isInRange = true;
            target = collision.transform;
        } 
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!isTrigger)
        {
            isInRange = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isTrigger)
        {
            if (isInstant)
            {
                Teleport();
                return;
            }
            isInRange = true;
            target = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isTrigger)
        {
            isInRange = false;
        }
    }

    // move targeted object to destination coordinate
    public void Teleport()
    {
        Invoke("moveTarget", delay);  
    }

    public void Teleport(float delay)
    {
        Invoke("moveTarget", delay);
    }

    private void moveTarget()
    {
        try
        {
            target.SetPositionAndRotation(
            new Vector3(destination.position.x, destination.position.y),
            Quaternion.identity);
        }
        catch (System.Exception)
        {
            throw;
        }
    }
}
