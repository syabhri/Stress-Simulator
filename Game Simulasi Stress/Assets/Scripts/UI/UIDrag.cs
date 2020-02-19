using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
public class UIDrag : MonoBehaviour
{
    float OffsetX;
    float OffsetY;

    private void Start()
    {
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener(delegate { BeginDrag(); });
        trigger.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.Drag;
        entry.callback.AddListener(delegate { OnDrag(); });
        trigger.triggers.Add(entry);
    }

    public void BeginDrag()
    {
        OffsetX = transform.position.x - Input.mousePosition.x;
        OffsetY = transform.position.y - Input.mousePosition.y;
    }

    public void OnDrag()
    {
        transform.position = new Vector3(OffsetX + Input.mousePosition.x, OffsetY + Input.mousePosition.y);
    }
}
