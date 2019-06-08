using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu]
public class TimeContainer : ScriptableObject
{
    public TimeFormat time;

    public void TMPInputSetDay(TMP_InputField inputText)
    {
        if (inputText.text == "")
            time.days = 0;
        else
            time.days = float.Parse(inputText.text);
    }

    public void TMPInputSetHours(TMP_InputField inputText)
    {
        if (inputText.text == "")
            time.hours = 0;
        else
            time.hours = float.Parse(inputText.text);
    }

    public void TMPInputSetMinutes(TMP_InputField inputText)
    {
        if (inputText.text == "")
            time.minutes = 0;
        else
            time.minutes = float.Parse(inputText.text);
    }
}
