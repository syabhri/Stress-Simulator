using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextSetter : MonoBehaviour
{
    public StringVariable target;

    public void SetText(TMP_InputField textMesh)
    {
        target.Value = textMesh.text;
    }
}
