using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Vector2Variable destination;
    public GameEvent animationEvent;

    [Header("Interaction")]
    public bool isRequireInteraction;
    public string triggerButton = "Submit";

    private bool isInRange = false;
    private Transform target;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isInRange = true;
        target = collision.transform;
        if (!isRequireInteraction)
        {
            Teleport(collision.transform);
        }  
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isInRange = false;
    }

    public void Update()
    {
        if (isInRange)
            if (Input.GetButtonDown(triggerButton))
                Teleport(target);
    }

    // move targeted object to destination coordinate
    public void Teleport(Transform target)
    {
        if (animationEvent != null)
        {
            animationEvent.Raise();
        }
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
