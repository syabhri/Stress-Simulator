using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextSetter : MonoBehaviour
{
    public StringVariable target;
    [Tooltip("Weather or not the text is updated at start")]
    public bool onStart = true;

    private void Start()
    {
        if (onStart)
        {
            SetText(GetComponent<TMP_InputField>());
        }
    }

    public void SetText(TMP_InputField textMesh)
    {
        target.Value = textMesh.text;
    }
}
