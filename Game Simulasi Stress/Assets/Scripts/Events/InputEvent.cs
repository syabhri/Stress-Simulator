using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputEvent : MonoBehaviour
{
    public string buttonName;
    public BoolVariable InputGroup;
    [Space]
    public UnityEvent unityEvent;

    private bool isEnable = true;

    private void Start()
    {
        if (InputGroup != null)
            InputGroup.OnValueChange += setEnable;
    }

    private void OnDestroy()
    {
        if (InputGroup != null)
            InputGroup.OnValueChange -= setEnable;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(buttonName) && isEnable)
        {
            unityEvent.Invoke();
        }
    }

    public void setEnable(BoolVariable intVariable)
    {
        isEnable = InputGroup.value;
    }
}
