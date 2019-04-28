using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactables : MonoBehaviour
{
    public EventWrapper InteractablesEvent;
    public DialogManager dialogManager;
    private bool IsInRange = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IsInRange = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        IsInRange = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Submit") && IsInRange)
        {
            for (int i = 0; i < InteractablesEvent.eventList.Length; i++)
            {
                switch (InteractablesEvent.eventList[i].type)
                {
                    case "dialogue":
                        dialogManager.StartDialogue(InteractablesEvent.eventList[i].dialogue);
                        break;
                    default:
                        break;
                }
            }
        }
    }

}
