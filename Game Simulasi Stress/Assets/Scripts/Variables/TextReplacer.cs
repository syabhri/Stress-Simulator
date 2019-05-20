// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
//
// Modified By : Syabhri Mustafa
// Date:    5/8/19
// ----------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextReplacer : MonoBehaviour
{
    public TextMeshProUGUI Text; // <changes>

    public StringVariable Variable;

    public bool AlwaysUpdate;

    public string BeforeText = "";
    public string AfterText = "";

    private void OnEnable()
    {
        Text.text = Variable.Value;
    }

    private void Update()
    {
        if (AlwaysUpdate)
        {
            UpdateText();
        }
    }

    public void UpdateText()
    {
        Text.text = BeforeText + Variable.Value + AfterText;
    }
}