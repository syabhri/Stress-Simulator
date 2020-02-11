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

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextReplacer : MonoBehaviour
{
    public enum UpdateType { StringContainer, FloatContainer }

    [Tooltip("Text In ui that need to be relace")]
    private TextMeshProUGUI UIText; // <changes>

    [Space]
    public UpdateType updateFrom;
    public StringContainer stringContainer;
    public FloatContainer floatContainer;

    [Space]
    public bool AlwaysUpdate;

    public string BeforeText = "";
    public string AfterText = "";

    private UpdateType selectedType;

    private void Awake()
    {
        UIText = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        switch (updateFrom)
        {
            case UpdateType.StringContainer:
                UpdateText(stringContainer);
                if (AlwaysUpdate) stringContainer.OnValueChanged += UpdateText;
                selectedType = UpdateType.StringContainer;
                break;
            case UpdateType.FloatContainer:
                UpdateText(floatContainer);
                if (AlwaysUpdate) floatContainer.OnValueChanged += UpdateText;
                selectedType = UpdateType.FloatContainer;
                break;
            default:
                break;
        }
        
    }

    private void OnDisable()
    {
        switch (selectedType)
        {
            case UpdateType.StringContainer:
                if (AlwaysUpdate) stringContainer.OnValueChanged -= UpdateText;
                break;
            case UpdateType.FloatContainer:
                if (AlwaysUpdate) floatContainer.OnValueChanged -= UpdateText;
                break;
            default:
                break;
        }
    }

    public void UpdateText(FloatContainer text)
    {
        UIText.text = BeforeText + text.Value.ToString() + AfterText;
        UIText.text = UIText.text.Replace("/n", "<br>");
    }

    public void UpdateText(StringContainer text)
    {
        UIText.text = BeforeText + text.Value + AfterText;
        UIText.text = UIText.text.Replace("/n", "<br>");
    }
}