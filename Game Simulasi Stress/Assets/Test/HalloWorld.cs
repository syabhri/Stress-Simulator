using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalloWorld : MonoBehaviour
{
    public TimeContainer test;

    private void Start()
    {
        test.OnValueChanged += HalloWorlds;
    }

    private void OnDestroy()
    {
        test.OnValueChanged -= HalloWorlds;
    }

    public void HalloWorlds(TimeContainer timeContainer)
    {
        Debug.Log("HelloWorlds");
    }
}
