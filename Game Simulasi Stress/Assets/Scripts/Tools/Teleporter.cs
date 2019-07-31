using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Vector2Variable destination;
    public float delay;

    [Header("Interaction")]
    public bool isRequireInteraction;
    public string triggerButton = "Submit";

    private bool isInRange = false;
    private Transform target;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (destination == null)
        {
            Debug.LogError("Destination is not set");
        }
        isInRange = true;
        target = collision.transform;
        if (!isRequireInteraction)
        {
            Teleport();
        }
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isInRange = false;
    }

    public void Update()
    {
        if (isInRange)
            if (Input.GetButtonDown(triggerButton))
                Teleport();
    }

    // move targeted object to destination coordinate
    public void Teleport()
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
