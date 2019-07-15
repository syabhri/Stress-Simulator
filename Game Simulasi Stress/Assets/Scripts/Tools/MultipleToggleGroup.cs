using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultipleToggleGroup : MonoBehaviour
{
    public int limit;
    public List<Toggle> toggles;

    private int toggleCount;

    public void CheckToggleLimit()
    {
        toggleCount = 0;

        foreach (Toggle toggle in toggles)
        {
            if (toggle.isOn)
            {
                toggleCount++;
            }
        }

        if (toggleCount >= limit)
        {
            DisableToggle();
        }
        else
        {
            EnableToggles();
        }
    }

    public void EnableToggles()
    {
        foreach (Toggle toggle in toggles)
        {
            toggle.interactable = true;
        }
    }

    public void DisableToggle()
    {
        foreach (Toggle toggle in toggles)
        {
            if (!toggle.isOn)
            {
                toggle.interactable = false;
            }
        }
    }

    public void AddToggle(Toggle toggle)
    {
        toggles.Add(toggle);
    }

    public void RemoveToggle(Toggle toggle)
    {
        toggles.Remove(toggle);
    }
}
