using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QueuedRuntimeSet<T> : ScriptableObject
{
    public Queue<T> Items = new Queue<T>();

    public void Enqueue(T thing)
    {
        Items.Enqueue(thing);
    }

    public void Dequeue()
    {
        Items.Dequeue();
    }
}
