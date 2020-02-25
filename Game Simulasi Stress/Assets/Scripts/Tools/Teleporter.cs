using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Teleporter : MonoBehaviour
{
    public Transform destination;
    public float delay;
    public bool isTrigger;
    public bool isInstant = true;
    public UnityEvent OnTeleport;
    public UnityEvent OnArrive;

    private Transform target;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isTrigger)
        {
            if (isInstant)
            {
                OnTeleport.Invoke();
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
                OnTeleport.Invoke();
                StartCoroutine(MoveTarget(collision.transform , delay));
                return;
            }
            target = collision.transform;
        }
    } 

    public bool Instant
    {
        set { isInstant = value; }
        get { return isInstant; }
    }

    // move targeted object to destination coordinate
    public void Teleport()
    {
        OnTeleport.Invoke();
        StartCoroutine(MoveTarget(target, delay));
    }

    public void Teleport(float delay)
    {
        OnTeleport.Invoke();
        StartCoroutine(MoveTarget(target, delay));
    }

    IEnumerator MoveTarget(Transform target, float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        try
        {
            target.SetPositionAndRotation(destination.position, Quaternion.identity);
        }
        catch (System.Exception)
        {
            throw;
        }
        finally
        {
            OnArrive.Invoke();
        }
    }
}
