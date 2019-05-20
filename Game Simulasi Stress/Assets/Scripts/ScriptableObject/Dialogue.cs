using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "ScriptableObject/Dialogue")]
public class Dialogue : ScriptableObject
{
    [Tooltip("Required if this dialog is a Response"), Header("Options")]
    public string response;
    [Tooltip("Response for dismissing the dialog")]
    public string dismisses;

    [Header("Content")]
    public Speaker[] speakers;

    [Header("Response")]
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
