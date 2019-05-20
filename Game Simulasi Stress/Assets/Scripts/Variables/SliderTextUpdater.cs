﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode]
public class SliderTextUpdater : MonoBehaviour
{
    public TextMeshProUGUI TextTarget;
    public Slider SliderSource;

    [Space]
    public bool UsePercentSymbol = true;

    [Space]
    public bool UseMultiplier = true;
    public float Multiplier = 100;

    private void OnEnable()
    {
        updateText();
    }

    public void updateText ()
    {
        if (TextTarget == null && SliderSource == null)
        {
            Debug.LogError("variable not set");
            return;
        }

        if (UseMultiplier)
        {
            if (UsePercentSymbol)
            {
                TextTarget.text = (SliderSource.value * Multiplier ).ToString() + "%";
            }
            else
            {
                TextTarget.text = (SliderSource.value * Multiplier).ToString();
            }
        }
        else
        {
            if (UsePercentSymbol)
            {
                TextTarget.text = SliderSource.value.ToString() + "%";
            }
            else
            {
                TextTarget.text = SliderSource.value.ToString();
            }
        }
    }
}
