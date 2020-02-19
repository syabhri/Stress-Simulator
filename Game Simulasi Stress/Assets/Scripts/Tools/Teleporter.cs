using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Vector2Container destination;
    public float delay;
    public bool isTrigger;
    public bool isInstant = true;

    private Transform target;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isTrigger)
        {
            if (isInstant)
            {
                StartCoroutine(MoveTarget(collision.transform, delay));
                return;
            }
            target = collision.transform;
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isTrigger)
        {
            if (isInstant)
            {
                StartCoroutine(MoveTarget(collision.transform , delay));
                return;
            }
            target = collision.transform;
        }
    } 

    // move targeted object to destination coordinate
    public void Teleport()
    {
        StartCoroutine(MoveTarget(target, delay));
    }

    public void Teleport(float delay)
    {
        StartCoroutine(MoveTarget(target, delay));
    }

    IEnumerator MoveTarget(Transform target, float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        try
        {
            target.SetPositionAndRotation(
            new Vector3(destination.Value.x, destination.Value.y),
            Quaternion.identity);
        }
        catch (System.Exception)
        {
            throw;
        }
    }
}
