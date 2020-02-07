using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultipleToggleGroup : MonoBehaviour
{
    public int SelectionLimit;
    private List<Toggle> toggles = new List<Toggle>();

    private int toggleCount;

    private void Start()
    {
        List<Toggle> toggles = new List<Toggle>(GetComponentsInChildren<Toggle>());
        foreach (Toggle toggle in toggles)
        {
            AddToggle(toggle);
        }
    }

    public void CheckToggleLimit()
    {
        toggleCount = 0;

        foreach (Toggle toggle in toggles)
            if (toggle.isOn)
                toggleCount++;

        if (toggleCount >= SelectionLimit)
            DisableToggles();
        else
            EnableToggles();
    }

    public void EnableToggles()
    {
        foreach (Toggle toggle in toggles)
            toggle.interactable = true;
    }

    public void DisableToggles()
    {
        foreach (Toggle toggle in toggles)
            if (!toggle.isOn)
                toggle.interactable = false;
    }

    public void AddToggle(Toggle toggle)
    {
        toggles.Add(toggle);
        toggle.onValueChanged.AddListener(delegate { CheckToggleLimit(); });
    }

    public void RemoveToggle(Toggle toggle)
    {
        toggles.Remove(toggle);
        toggle.onValueChanged.RemoveListener(delegate { CheckToggleLimit(); });
    }

    public void ClearToggle()
    {
        foreach (var toggle in toggles)
            toggle.onValueChanged.RemoveListener(delegate { CheckToggleLimit(); });
        toggles.Clear();
    }
}
