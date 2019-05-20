using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Activity", menuName = "ScriptableObject/Activity")]
public class Activity : ScriptableObject
{
    public new string name;
    public string[] activityType;

    public Animator animator;
    public Dialogue dialogue;

    public bool isScaduled;
    public string day;

    public float ValueLimit;

    public float AmountLimit;

    public float duration;

    public float price;

    public string[] interest;

    public Ability abilityEffector;

    public void PassValue(Activity activity)
    {

    }
}
