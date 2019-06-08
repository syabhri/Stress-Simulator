using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactables : MonoBehaviour
{
    [Header("Properties")]
    public Dialogue defaultDialogue;

    [Header("Event")]
    public GameEvent onDialogStart;

    [Header("Condition")]
    public BoolVariable[] ignoreInput;

    [Header("DataPasser")]
    public Dialogue dialoguePasser;

    private bool isInRange = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isInRange = false;
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Submit") && isInRange)
        {
            if (!IgnoreInput())
            {
                Debug.Log("Interaction Started : " + this.gameObject.name);
                dialoguePasser.PassValue(defaultDialogue);
                onDialogStart.Raise();
            } 
        }
    }

    private bool IgnoreInput()
    {
        foreach (BoolVariable condition in ignoreInput)
        {
            if (condition.value)
            {
                return true;
            }
        }
        return false;
    }

}
