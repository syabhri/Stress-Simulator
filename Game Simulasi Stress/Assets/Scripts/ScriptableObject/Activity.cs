﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Activity", menuName = "ScriptableObject/Activity")]
public class Activity : ScriptableObject
{
    [Header("Properties")]
    public string activityName;
    public bool isUseEnergy;
    //public string[] tag;
    //public bool isActive;

    [Header("Cost")]
    public bool isCostMoney;
    public float cost;

    [Header("Scadule")]
    public bool isScaduled;
    public TimeFormat scadule;
    public TimeFormat tolerance = new TimeFormat(0,0,15);//default value

    [Header("Limit")]
    public bool isLimited;
    public float limitPerDay;
    public float currentCount;
    public float valueLimit;
    public float currentValue;

    [Header("Duration")]
    public bool isDutrationAjustable;
    public TimeFormat duration;

    [Header("Increase Stress")]
    public bool isIncreaseStress;
    public bool increaseStressByHours;
    public float increasedStressMultiplier;

    [Header("Decrease Stress")]
    public bool isDecreaseStress;
    public bool decreaseStressByHours;
    public float decreasedStressMultiplier;

    [Header("Effected By Stress")]
    public bool isEffectedByStress;
    //public List<FloatVariable> EffectedStat;

    [Header("Other Stat")]
    public bool isChangeOtherStat;
    public bool ChangeStatByHours;
    public FloatPair otherStat;
    [Tooltip("Operation is the basic operator \"=+-*/\" used to change the target")]
    public string operation;

    [Header("Bonus")]
    public FloatVariable interest;
    public FloatVariable ability;

    //incoplete
    public void PassValue(Activity activity)
    {
        activityName = activity.activityName;
        isUseEnergy = activity.isUseEnergy;
        //tag = activity.tag;
        //isActive = activity.isActive;

        isCostMoney = activity.isCostMoney;
        cost = activity.cost;

        isScaduled = activity.isScaduled;
        scadule.PassValue(activity.scadule);
        tolerance.PassValue(activity.tolerance);

        isLimited = activity.isLimited;
        limitPerDay = activity.limitPerDay;

        isDutrationAjustable = activity.isDutrationAjustable;
        duration.PassValue(activity.duration);

        isIncreaseStress = activity.isIncreaseStress;
        increaseStressByHours = activity.increaseStressByHours;
        increasedStressMultiplier = activity.increasedStressMultiplier;

        isDecreaseStress = activity.isDecreaseStress;
        decreaseStressByHours = activity.decreaseStressByHours;
        decreasedStressMultiplier = activity.decreasedStressMultiplier;

        isEffectedByStress = activity.isEffectedByStress;
        //EffectedStat = activity.EffectedStat;

        isChangeOtherStat = activity.isChangeOtherStat;
        ChangeStatByHours = activity.ChangeStatByHours;
        otherStat = activity.otherStat;
    }
}
