using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Teleporter : MonoBehaviour
{
    public Vector2Variable destination;
    public GameEvent EnterAnimation;
    public GameEvent ExitAnimation;

    [Header("Interaction")]
    public bool isRequireInteraction;
    public string triggerButton = "Submit";

    private bool isInRange = false;
    private Transform target;

    private void OnCollisionEnter2D(Collision2D collision)
    {
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
        if (EnterAnimation != null)
        {
            EnterAnimation.Raise();
        }
        else
        {
            moveTarget();
        }
    }

    public void moveTarget()
    {
        if (isInRange)
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
            if (ExitAnimation != null)
            {
                ExitAnimation.Raise();
            }
        }
    }
}
