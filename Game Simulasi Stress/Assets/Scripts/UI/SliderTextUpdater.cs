using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderTextUpdater : MonoBehaviour
{
    public TextMeshProUGUI TextTarget;
    public Slider SliderSource;

    [Space]
    public bool UsePercentage = true;

    // called when value changed
    public void updateText ()
    {
        if (TextTarget == null && SliderSource == null)
        {
            Debug.LogError("variable not set");
            return;
        }

        if (UsePercentage)
        {
            TextTarget.text = (SliderSource.normalizedValue * 100).ToString() + "%";
        }
        else
        {
            TextTarget.text = SliderSource.value.ToString();
        }
    }
}
