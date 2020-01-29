using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrentResDisplay : MonoBehaviour
{
    public TextMeshProUGUI text;

    // Update is called once per frame
    void Update()
    {
        text.SetText(Screen.currentResolution.ToString());
    }
}
