using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSetter : MonoBehaviour
{
    public Toggle toggle;
    public BoolVariable Value;

    private void OnEnable()
    {
        UpdateChanges(Value);
    }

    public void UpdateChanges(BoolVariable Value)
    {
        toggle.isOn = Value.value;
    }

    public void UpdateChanges(bool Value)
    {
        toggle.isOn = Value;
    }
}
