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
    
    [Header("Output UI")]
    public StringContainer ClockText;
    public StringContainer dayCountText;
    public StringContainer dayNameText;

    [Header("Events")]
    public UnityEvent OnRoutine;

    public static int currentDay;
    public static int currentHours;
    public static int currentMinutes;
    public static DayName currentDayName;

    // routine properties
    private bool IsRoutineExecuted = true;
    private int lastDayExecuted;

    // time counter
    private float totalTime;
    private float dayNormalize;

    private void Update()
    {
        UpdateTime();
        ExecuteRoutine();
    }

    private void UpdateTime()
    {
        totalTime += Time.deltaTime / TimeScale;

        dayNormalize = totalTime % 1f;

        currentDay = Mathf.CeilToInt(totalTime);
        currentHours = Mathf.FloorToInt(dayNormalize * hoursPerDay);
        currentMinutes = Mathf.FloorToInt(dayNormalize * hoursPerDay % 1f * minutesPerHours);
        
        ClockText.Value = currentHours.ToString("00") + ":" + currentMinutes.ToString("00");
        dayCountText.Value = currentDay.ToString("00");
        dayNameText.Value = currentDayName.ToString();
    }

    public void SetTime(TimeContainer time)
    {
        totalTime =
            time.Value.days + 
            (time.Value.hours / hoursPerDay) + 
            (time.Value.minutes / hoursPerDay / minutesPerHours);
        currentDayName = time.Value.dayName;
    }

    public void SkipTime(TimeContainer time)
    {
        for (int i = currentDay; i < currentDay + time.Value.days - 1; i++)
        {
            setNextDayName();
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
        currentDayName = DayName.Senin;
    }

    // execute routine event every day
    public void ExecuteRoutine()
    {
        if (!IsRoutineExecuted)
        {
            setNextDayName();
            lastDayExecuted = currentDay;
            OnRoutine.Invoke();
            IsRoutineExecuted = true;
            Debug.Log("Routine Executed!");
        }
        else
        {
            if (currentDay != 1 && currentDay > lastDayExecuted)
            {
                IsRoutineExecuted = false;
            }
        }
    }

    private void setNextDayName()
    {
        switch (currentDayName)
        {
            case DayName.Senin:
                currentDayName = DayName.Selasa;
                break;
            case DayName.Selasa:
                currentDayName = DayName.Rabu;
                break;
            case DayName.Rabu:
                currentDayName = DayName.Kamis;
                break;
            case DayName.Kamis:
                currentDayName = DayName.Jumat;
                break;
            case DayName.Jumat:
                currentDayName = DayName.Sabtu;
                break;
            case DayName.Sabtu:
                currentDayName = DayName.Minggu;
                break;
            case DayName.Minggu:
                currentDayName = DayName.Senin;
                break;
            default:
                break;
        }
    }


}