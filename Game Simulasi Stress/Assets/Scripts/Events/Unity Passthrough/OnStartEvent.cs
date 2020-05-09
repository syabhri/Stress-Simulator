using UnityEngine;
using UnityEngine.Events;

public class OnStartEvent : MonoBehaviour
{
    public UnityEvent onStart;

    void Start()
    {
        onStart.Invoke();
    }
}
