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

    private void Update()
    {
        UpdateTime();
        ExecuteRoutine();
    }

    private void UpdateTime()
    {
        totalTime += Time.deltaTime / TimeScale;

        dayNormalize = totalTime % 1f;

        CurrentTime.Value.days = Mathf.CeilToInt(totalTime);
        CurrentTime.Value.hours = Mathf.FloorToInt(dayNormalize * hoursPerDay);
        CurrentTime.Value.minutes = Mathf.FloorToInt(dayNormalize * hoursPerDay % 1f * minutesPerHours);
        
        ClockText.Value = CurrentTime.Value.hours.ToString("00") + ":" + CurrentTime.Value.minutes.ToString("00");
        dayCountText.Value = CurrentTime.Value.days.ToString("00");
        dayNameText.Value = CurrentTime.Value.dayName.ToString();
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
        CurrentTime.Value.dayName = DayName.Senin;
    }

    // execute routine event every day
    public void ExecuteRoutine()
    {
        if (!IsRoutineExecuted)
        {
            setNextDayName();
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

    private void setNextDayName()
    {
        switch (CurrentTime.Value.dayName)
        {
            case DayName.Senin:
                CurrentTime.Value.dayName = DayName.Selasa;
                break;
            case DayName.Selasa:
                CurrentTime.Value.dayName = DayName.Rabu;
                break;
            case DayName.Rabu:
                CurrentTime.Value.dayName = DayName.Kamis;
                break;
            case DayName.Kamis:
                CurrentTime.Value.dayName = DayName.Jumat;
                break;
            case DayName.Jumat:
                CurrentTime.Value.dayName = DayName.Sabtu;
                break;
            case DayName.Sabtu:
                CurrentTime.Value.dayName = DayName.Minggu;
                break;
            case DayName.Minggu:
                CurrentTime.Value.dayName = DayName.Senin;
                break;
            default:
                break;
        }
    }


}