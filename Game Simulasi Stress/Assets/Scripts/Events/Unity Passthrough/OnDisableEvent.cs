using UnityEngine;
using UnityEngine.Events;

public class OnDisableEvent : MonoBehaviour
{
    public UnityEvent onDisable;

    private void OnDisable()
    {
        onDisable.Invoke();
    }
}
