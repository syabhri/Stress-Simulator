using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class TimeManager : MonoBehaviour
{
    // constant values
    public const float hoursPerDay = 24f;
    public const float minutesPerHours = 60f;
    public enum DayName { Senin, Selasa, Rabu, Kamis, Jumat, Sabtu, Minggu }

    [Header("Properties")]
    [SerializeField] private float TimeScale = 1;

    [Header("Data Container")]
    public TimeContainer CurrentTime;
    
    [Header("Output UI")]
    public StringContainer ClockText;
    public StringContainer dayCountText;
    public StringContainer dayNameText;

    [Header("Events")]
    public UnityEvent OnRoutine;

    // routine properties
    private bool IsRoutineExecuted = true;
    private int lastDayExecuted;

    // time counter
    private float totalTime;
    private float dayNormalize;

    private int dayBuffer;
    private int hourBuffer;
    private int minuteBuffer;

    private void OnEnable()
    {
        SetTime(CurrentTime);
    }

    private void Update()
    {
        UpdateTime();
        ExecuteRoutine();
    }

    private void UpdateTime()
    {
        totalTime += Time.deltaTime / TimeScale;

        dayNormalize = totalTime % 1f;

        dayBuffer = Mathf.CeilToInt(totalTime);
        hourBuffer = Mathf.FloorToInt(dayNormalize * hoursPerDay);
        minuteBuffer = Mathf.FloorToInt(dayNormalize * hoursPerDay % 1f * minutesPerHours);

        if (CurrentTime.Value.days != dayBuffer)
        {
            CurrentTime.Value.days = dayBuffer;
            dayCountText.Value = CurrentTime.Value.days.ToString("00");
            dayNameText.Value = CurrentTime.Value.dayName.ToString();
        }

        if (CurrentTime.Value.hours != hourBuffer)
        {
            CurrentTime.Value.hours = hourBuffer;
        }

        if (CurrentTime.Value.minutes != minuteBuffer)
        {
            CurrentTime.Value.minutes = minuteBuffer;
            ClockText.Value = CurrentTime.Value.hours.ToString("00") + ":" + CurrentTime.Value.minutes.ToString("00");
        }
    }

    public void SetTime(TimeContainer time)
    {
        totalTime =
            time.Value.days + 
            (time.Value.hours / hoursPerDay) + 
            (time.Value.minutes / hoursPerDay / minutesPerHours);
        CurrentTime.Value.dayName = time.Value.dayName;
    }

    public void SkipTime(TimeContainer time)
    {
        for (int i = CurrentTime.Value.days; i < CurrentTime.Value.days + time.Value.days - 1; i++)
        {
            CurrentTime.Value.dayName = NextDayName(CurrentTime.Value.dayName);
            OnRoutine.Invoke();
        }

        totalTime +=
            time.Value.days +
            (time.Value.hours / hoursPerDay) +
            (time.Value.minutes / hoursPerDay / minutesPerHours);
    }

    public void ResetTime()
    {
        totalTime = 0;
        CurrentTime.Value.dayName = DayName.Senin;
    }

    // execute routine event every day
    public void ExecuteRoutine()
    {
        if (!IsRoutineExecuted)
        {
            CurrentTime.Value.dayName = NextDayName(CurrentTime.Value.dayName);
            OnRoutine.Invoke();
            lastDayExecuted = CurrentTime.Value.days;
            IsRoutineExecuted = true;
            Debug.Log("Routine Executed!");
        }
        else
        {
            if (CurrentTime.Value.days != 1 && CurrentTime.Value.days > lastDayExecuted)
            {
                IsRoutineExecuted = false;
            }
        }
    }

    public static DayName NextDayName(DayName dayName)
    {
        switch (dayName)
        {
            case DayName.Senin:
                return DayName.Selasa;
            case DayName.Selasa:
                return DayName.Rabu;
            case DayName.Rabu:
                return DayName.Kamis;
            case DayName.Kamis:
                return DayName.Jumat;
            case DayName.Jumat:
                return DayName.Sabtu;
            case DayName.Sabtu:
                return DayName.Minggu;
            case DayName.Minggu:
                return DayName.Senin;
            default:
                return dayName;
        }
    }

    public static int DaysToMinute(int days)
    {
        return days * (int)hoursPerDay * (int)minutesPerHours;
    }
}