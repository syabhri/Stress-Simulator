﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/ActivitySet")]
public class ActivitySet : ScriptableObject
{
    public List<Activity> activities;

    public void ResetLimit()
    {
        foreach (Activity activity in activities)
        {
            if (activity.isLimited)
            {
                activity.currentCount = 0;
            }
        }
    }
}
