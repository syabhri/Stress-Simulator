﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Used to generate a set of buttons
public class ButtonGenerator : MonoBehaviour
{
    public Button ButtonTemplate;
    [Tooltip("close/disable when any of the assiged button is clicked")]
    public bool DisableOnClick = true;
    [Tooltip("clear assigned button when disabled")]
    public bool ClearOnDisabled = true;

    private Stack<Button> unusedButtons;
    private Stack<Button> usedButtons;

    private void Awake()
    {
        usedButtons = new Stack<Button>();
        unusedButtons = new Stack<Button>();
    }

    // assign a button to the set
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

        if (DisableOnClick)
        {
            button.onClick.AddListener(delegate { gameObject.SetActive(false); });
        }
        return button;
    }

    public void ClearUsedButtons()
    {
        while (usedButtons.Count > 0)
        {
            Button button = usedButtons.Pop();
            button.gameObject.SetActive(false);
            unusedButtons.Push(button);
        }
    }

    private void OnDisable()
    {
        if (ClearOnDisabled)
        {
            ClearUsedButtons();
        }
    }
}
