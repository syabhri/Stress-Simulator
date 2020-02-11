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
    public bool UsePercentage = true;

    private void OnEnable()
    {
        updateText();
        SliderSource.onValueChanged.AddListener(delegate { updateText(); });
    }

    private void OnDisable()
    {
        SliderSource.onValueChanged.RemoveListener(delegate { updateText(); });
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
