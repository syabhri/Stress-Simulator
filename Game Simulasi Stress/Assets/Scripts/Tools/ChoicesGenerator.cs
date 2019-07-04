using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoicesGenerator : MonoBehaviour
{
    private Queue<Button> unusedButtons;
    private Queue<Button> usedButtons;

    [Header("Reference")]
    public Button buttonPrefab;

    [Header("Data Passer")]
    public Dialogue dialoguePasser;
    public Activity activityPasser;
    public StringVariable ButtonTextPasser;

    [Header("Event")]
    public GameEvent OnDialogStart;
    public GameEvent OnActivityStart;
    public GameEvent OnDecisionEnd;
    public GameEvent OnDialogEnd;

    private Navigation customNav;

    private void Awake()
    {
        usedButtons = new Queue<Button>();
        unusedButtons = new Queue<Button>();
        customNav = new Navigation();
        customNav.mode = Navigation.Mode.None;
    }

    //assign each button to a next dialog and/or activity. and also adding dissmiss button
    public void AssignButton(Dialogue dialogue)
    {
        ClearUsedButton();
        Button button;
        foreach (Dialogue nextDialogue in dialogue.nextDialog)
        {
            ButtonTextPasser.Value = nextDialogue.response;
            button = AddButton();            
            button.onClick.AddListener(delegate { dialoguePasser.PassValue(nextDialogue); });
            button.onClick.AddListener(delegate { OnDialogStart.Raise(); });
            button.onClick.AddListener(delegate { OnDecisionEnd.Raise(); });
        }
        foreach (Activity doActivity in dialogue.doActivities)
        {
            ButtonTextPasser.Value = doActivity.name;
            button = AddButton();
            button.onClick.AddListener(delegate { activityPasser.PassValue(doActivity); });
            button.onClick.AddListener(delegate { OnDialogEnd.Raise(); });
            button.onClick.AddListener(delegate { OnActivityStart.Raise(); });
            button.onClick.AddListener(delegate { OnDecisionEnd.Raise(); });

            if (doActivity.isLimited)
            {
                if (ActivityManager.LimitPerDayReached(doActivity))
                {
                    button.interactable = false;
                }
            }
        }
        ButtonTextPasser.Value = dialogue.dismisses;
        //Debug.Log(dialogue.dismisses + dialogue.name);
        button = AddButton();
        button.onClick.AddListener(delegate { OnDialogEnd.Raise(); });
        button.onClick.AddListener(delegate { OnDecisionEnd.Raise(); });
    }

    public bool ClearUsedButton()
    {
        if (usedButtons != null)
        {
            while (usedButtons.Count > 0)
            {
                Button usedbutton = usedButtons.Dequeue();
                usedbutton.gameObject.SetActive(false);
                unusedButtons.Enqueue(usedbutton);
            }
            return true;
        }
        else
        {
            Debug.Log("There Is No Used Buttons : " + usedButtons.Count);
            return false;
        }
    }

    public Button AddButton()
    {
        Button button;
        if (unusedButtons.Count > 0)
        {
            button = unusedButtons.Dequeue();
            button.onClick.RemoveAllListeners();
            button.gameObject.SetActive(true);
            button.interactable = true;
            usedButtons.Enqueue(button);
        }
        else
        {
            button = Instantiate(buttonPrefab, gameObject.transform, worldPositionStays:false);
            //button.transform.SetParent(gameObject.transform,worldPositionStays:false);
            button.navigation = customNav;
            usedButtons.Enqueue(button);
        }
        return button;
    }
}
