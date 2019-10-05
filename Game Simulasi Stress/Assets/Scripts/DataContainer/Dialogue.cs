using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Dialogue", menuName = "ScriptableObject/Dialogue")]
public class Dialogue : ScriptableObject
{
    [System.Serializable]
    public class ChoiceEvent
    {
        public string name;
        public UnityEvent unityEvent;
    }

    [Tooltip("Response for dialog before this"), Header("Options")]
    public string response;

    [Tooltip("Dissmiss massage when choses the decisison to end dialog")]
    public string dismisses;

    [Header("Content")]
    public Speaker[] speakers;

    [Header("Response Choice")]
    public ChoiceEvent[] choiceEvent;
    public Dialogue[] nextDialog;

    public Activity[] doActivities;

    public void PassValue(Dialogue dialogue)
    {
        response = dialogue.response;
        dismisses = dialogue.dismisses;
        speakers = dialogue.speakers;
        nextDialog = dialogue.nextDialog;
        doActivities = dialogue.doActivities;
    }
}
