using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

// Used to generate a set of buttons
public class ButtonGenerator : MonoBehaviour
{
    public Button ButtonTemplate;

    [Tooltip("clear assigned button when disabled")]
    public bool ClearOnDisabled = true;

    [Tooltip("Default action of every button generated when pressed")]
    public UnityEvent DefaultAction;

    private Stack<Button> unusedButtons;
    private Stack<Button> usedButtons;

    private void Awake()
    {
        usedButtons = new Stack<Button>();
        unusedButtons = new Stack<Button>();
    }

    // assign a button witout text
    public Button AssignButton()
    {
        Button button;
        if (unusedButtons.Count > 0)
        {
            button = unusedButtons.Pop();
            button.onClick.RemoveAllListeners();
            button.gameObject.SetActive(true);
            button.interactable = true;
            usedButtons.Push(button);
        }
        else
        {
            button = Instantiate(ButtonTemplate, gameObject.transform, worldPositionStays: false);
            usedButtons.Push(button);
        }

        button.onClick.AddListener(delegate { DefaultAction.Invoke(); });
        return button;
    }

    // assign button with text
    public Button AssignButton(string ButtonText)
    {
        Button button;
        if (unusedButtons.Count > 0)
        {
            button = unusedButtons.Pop();
            button.onClick.RemoveAllListeners();
            button.GetComponentInChildren<TextMeshProUGUI>().text = ButtonText;
            button.gameObject.SetActive(true);
            button.interactable = true;
            usedButtons.Push(button);
        }
        else
        {
            button = Instantiate(ButtonTemplate, gameObject.transform, worldPositionStays: false);
            button.GetComponentInChildren<TextMeshProUGUI>().text = ButtonText;
            usedButtons.Push(button);
        }

        button.onClick.AddListener(delegate { DefaultAction.Invoke(); });
        return button;
    }

    // clear button set
    public void ClearUsedButtons()
    {
        while (usedButtons.Count > 0)
        {
            Button button = usedButtons.Pop();
            button.gameObject.SetActive(false);
            unusedButtons.Push(button);
        }
    }

    // clear button set on disbale
    private void OnDisable()
    {
        if (ClearOnDisabled)
        {
            ClearUsedButtons();
        }
    }
}
