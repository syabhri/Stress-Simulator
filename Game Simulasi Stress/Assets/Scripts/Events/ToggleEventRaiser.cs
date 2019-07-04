using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ToggleEventRaiser : MonoBehaviour
{
    [Tooltip("the toggle that will trigger the event")]
    public Toggle toggle;
    [Tooltip("trigger the event when toggle is off/unselected instead")]
    public bool whenOff;
    [Tooltip("the event that will be triggered")]
    public UnityEvent unityEvent;
    
    public void TryRaiseEvent()
    {
        if (!whenOff && toggle.isOn)
        {
            unityEvent.Invoke();
        }
        else if (whenOff && !toggle.isOn)
        {
            unityEvent.Invoke();
        }
    }
}
