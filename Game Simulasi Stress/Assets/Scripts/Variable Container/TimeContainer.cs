using UnityEngine;
using System;

[CreateAssetMenu(menuName = "VariableContainer/TimeContainer")]
public class TimeContainer : VariableContainer<TimeFormat>
{
    public Action<TimeContainer> OnValueChanged = delegate { };

    public override TimeFormat Value
    {
        set { this.value = value; OnValueChanged.Invoke(this); }
        get { return value; }
    }

    public void SetDay(int day)
    {
        value.days = day;
        OnValueChanged.Invoke(this);
    }

    public void SetDay(string day)
    {
        int.TryParse(day, out value.days);
        OnValueChanged.Invoke(this);
    }

    public void SetHour(int hour)
    {
        value.hours = hour;
        OnValueChanged.Invoke(this);
    }

    public void SetHour(string hour)
    {
        int.TryParse(hour, out value.hours);
        OnValueChanged.Invoke(this);
    }

    public void SetMinute(int minute)
    {
        value.minutes = minute;
        OnValueChanged.Invoke(this);
    }

    public void SetMinute(string minute)
    {
        int.TryParse(minute, out value.minutes);
        OnValueChanged.Invoke(this);
    }

    public void SetDayName(string dayName)
    {
        Enum.TryParse(dayName, out value.dayName);
        OnValueChanged.Invoke(this);
    }

    public void Reset()
    {
        Value?.Reset();
        OnValueChanged.Invoke(this);
    }
}
