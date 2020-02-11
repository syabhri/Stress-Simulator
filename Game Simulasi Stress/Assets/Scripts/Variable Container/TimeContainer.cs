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

    public void SetDay(string day)
    {
        float.TryParse(day, out value.days);
    }

    public void SetHour(string hour)
    {
        float.TryParse(hour, out value.hours);
    }

    public void SetMinute(string minute)
    {
        float.TryParse(minute, out value.minutes);
    }

    public void SetDayName(string dayName)
    {
        Enum.TryParse(dayName, out value.dayName);
    }
}
