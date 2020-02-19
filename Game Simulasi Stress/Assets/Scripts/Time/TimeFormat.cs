[System.Serializable]
public class TimeFormat
{
    public int days;
    public int hours;
    public int minutes;
    public TimeManager.DayName dayName;

    //default constructor
    public TimeFormat() { }

    //constructor
    public TimeFormat(int days, int hours, int minutes)
    {
        this.days = days;
        this.hours = hours;
        this.minutes = minutes;
    }

    public TimeFormat(int days, int hours, int minutes, TimeManager.DayName dayName)
    {
        this.days = days;
        this.hours = hours;
        this.minutes = minutes;
        this.dayName = dayName;
    }

    public void SetValue(TimeFormat time)
    {
        days = time.days;
        hours = time.hours;
        minutes = time.minutes;
        dayName = time.dayName;
    }

    public void Reset()
    {
        days = 0;
        hours = 0;
        minutes = 0;
        dayName = TimeManager.DayName.Senin;
    }

    public float ToHours()
    {
        return (days * TimeManager.hoursPerDay) + hours + (minutes / TimeManager.minutesPerHours);
    }

    public float ToDays()
    {
        return days + (hours / TimeManager.hoursPerDay) +
            ((minutes / TimeManager.minutesPerHours) / TimeManager.hoursPerDay);
    }

    public bool Matches(TimeFormat timeFormat)
    {
        if (days == timeFormat.days && hours == timeFormat.hours && minutes == timeFormat.minutes)
        {
            return true;
        }
        return false;
    }
}
