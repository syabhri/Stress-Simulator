using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderTextUpdater : MonoBehaviour
{
    public Slider SliderSource;
    public TextMeshProUGUI TextTarget;

    [Space]
    public bool UsePercentage = true;

    private void Start()
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
