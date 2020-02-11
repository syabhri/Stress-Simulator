using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSetter : MonoBehaviour
{
    public Toggle toggle;
    public BoolContainer Value;

    private void OnEnable()
    {
        UpdateChanges(Value);
    }

    public void UpdateChanges(bool Value)
    {
        toggle.isOn = Value;
    }
}
