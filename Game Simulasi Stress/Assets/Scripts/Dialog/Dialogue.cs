using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue")]
public class Dialogue : ScriptableObject
{
    [System.Serializable]
    public class ChoiceEvent
    {
        public string name;
        public UnityEvent unityEvent;
    }

    [Header("Content")]
    public List<Speaker> speakers;

    [Header("Response Choice")]
    public List<ChoiceEvent> choiceEvent;

    public void PassValue(Dialogue dialogue)
    {
        speakers = dialogue.speakers;
        choiceEvent = dialogue.choiceEvent;
    }
}
