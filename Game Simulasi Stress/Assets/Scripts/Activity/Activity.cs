using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Activity", menuName = "ScriptableObject/Activity")]
public class Activity : ScriptableObject
{
    [Header("Animation")]
    public GameEvent animationTrigger;
    public float trannsitionDelay;

    [Header("Energy Consumption")]
    public float energyPerHour;

    [Header("Cost")]
    public float cost;

    [Header("Limit")]
    public int limitPerDay;

    [Header("Scaduling")]
    public bool isSceduled;
    public TimeFormat SceduleStart;
    public TimeFormat SceduleEnd;

    [Header("Duration")]
    public bool isDutrationAjustable;
    public TimeFormat duration;

    [Header("Increase Stress")]
    public bool isIncreaseStress;
    public float increasedStressModifier;

    [Header("Decrease Stress")]
    public bool isDecreaseStress;
    public float decreasedStressModifier;

    [Header("Other Stat")]
    public bool isChangeOtherStat;
    public bool isEffectedByStress;
    public FloatContainer changedStat;
    public float changedStatModifier;

    [Header("Bonus")]
    public BoolContainer interest;
    public FloatContainer ability;

    //incoplete
    public void PassValue(Activity activity)
    {
        name = activity.name;

        animationTrigger = activity.animationTrigger;
        trannsitionDelay = activity.trannsitionDelay;

        energyPerHour = activity.energyPerHour;

        cost = activity.cost;

        limitPerDay = activity.limitPerDay;

        isDutrationAjustable = activity.isDutrationAjustable;
        duration.SetValue(activity.duration);

        isIncreaseStress = activity.isIncreaseStress;
        increasedStressModifier = activity.increasedStressModifier;

        isDecreaseStress = activity.isDecreaseStress;
        decreasedStressModifier = activity.decreasedStressModifier;

        isChangeOtherStat = activity.isChangeOtherStat;
        isEffectedByStress = activity.isEffectedByStress;

        changedStat = activity.changedStat;
        changedStatModifier = activity.changedStatModifier;

        interest = activity.interest;
        ability = activity.ability;
    }
}
