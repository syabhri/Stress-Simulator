using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ToggleEvent : MonoBehaviour
{
    [Tooltip("the toggle that will trigger the event")]
    public Toggle toggle;
    [Tooltip("the event that will be triggered")]
    public UnityEvent onSelect;
    public UnityEvent onDeselect;

    private void Start()
    {
        if (toggle == null)
        {
            toggle = GetComponent<Toggle>();
        }
        InvokeEvent();
    }

    public void InvokeEvent()
    {
        if (toggle.isOn)
        {
            onSelect.Invoke();
        }
        else if (!toggle.isOn)
        {
            onDeselect.Invoke();
        }
    }
}
