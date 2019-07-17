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
    [Tooltip("Text In ui that need to be relace")]
    public TextMeshProUGUI Text; // <changes>

    [Space]
    public bool updateString;
    public StringVariable stringVariable;
    public bool updateFloat;
    public FloatVariable floatVariable;

    [Space]
    public bool AlwaysUpdate;

    public string BeforeText = "";
    public string AfterText = "";

    private void OnEnable()
    {
        UpdateText();
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
        
        if (updateFloat)
        {
            Text.text = BeforeText + floatVariable.value.ToString() + AfterText;
        }
        if (updateString)
        {
            Text.text = BeforeText + stringVariable.Value + AfterText;
        }
    }
}