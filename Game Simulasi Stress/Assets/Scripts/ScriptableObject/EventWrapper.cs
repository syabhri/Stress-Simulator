using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Event", menuName = "ScriptableObject/Event")]
public class EventWrapper : ScriptableObject
{
    public Event[] eventList;
}
