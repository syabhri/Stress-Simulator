using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextEncloser : MonoBehaviour
{
    public TextMeshProUGUI Text;

    public string beforeText;
    public string afterText;

    public void EncloseText()
    {
        Text.text = beforeText + Text.text + afterText;
    }
}
