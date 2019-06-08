using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TimeFormat
{
    public float days;
    public float hours;
    public float minutes;

    //default constructor
    public TimeFormat() { }

    //constructor
    public TimeFormat(float days, float hours, float minutes)
    {
        this.days = days;
        this.hours = hours;
        this.minutes = minutes;
    }

    public void PassValue(TimeFormat time)
    {
        days = time.days;
        hours = time.hours;
        minutes = time.minutes;
    }

    public float ToHours()
    {
        return (days * 24) + hours + (minutes / 60);
    }
}
