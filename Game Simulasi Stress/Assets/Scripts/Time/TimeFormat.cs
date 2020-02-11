[System.Serializable]
public class TimeFormat 
{
    public float days;
    public float hours;
    public float minutes;
    public TimeManager.DayName dayName;

    //default constructor
    public TimeFormat() { }

    //constructor
    public TimeFormat(float days, float hours, float minutes)
    {
        this.days = days;
        this.hours = hours;
        this.minutes = minutes;
    }

    public TimeFormat(float days, float hours, float minutes, TimeManager.DayName dayName)
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
