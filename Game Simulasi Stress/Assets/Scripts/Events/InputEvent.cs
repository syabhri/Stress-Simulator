using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputEvent : MonoBehaviour
{
    public string buttonName;
    public BoolContainer InputGroup;
    [Space]
    public UnityEvent OnButtonDown;

    private bool isEnable = true;

    private void Start()
    {
        if (InputGroup != null)
            InputGroup.OnValueChanged += setEnable;
    }

    private void OnDestroy()
    {
        if (InputGroup != null)
            InputGroup.OnValueChanged -= setEnable;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(buttonName) && isEnable)
        {
            OnButtonDown.Invoke();
        }
    }

    public void setEnable(BoolContainer intVariable)
    {
        isEnable = InputGroup.Value;
    }
}
