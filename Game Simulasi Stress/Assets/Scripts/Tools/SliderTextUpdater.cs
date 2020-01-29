using System.Collections;
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

    // called when value changed
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
                TextTarget.text = (SliderSource.value * Multiplier ).ToString("0") + "%";
            }
            else
            {
                TextTarget.text = (SliderSource.value * Multiplier).ToString("0");
            }
        }
        else
        {
            if (UsePercentSymbol)
            {
                TextTarget.text = SliderSource.value.ToString("0") + "%";
            }
            else
            {
                TextTarget.text = SliderSource.value.ToString("0");
            }
        }
        Debug.Log("text Updated");
    }
}
