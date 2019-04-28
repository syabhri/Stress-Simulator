using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Activity", menuName = "ScriptableObject/Activity")]
public class Activity : ScriptableObject
{
    public string activityName;

    public bool isScaduled;
    public string day;

    public bool isValueLimited;
    public float ValueLimit;

    public bool isAmountLimited;
    public float AmountLimit;

    public bool isAjustable;
    public float duration;

    public bool isPriced;
    public float price;

    public string[] interest;
    public string[] ability;
}
