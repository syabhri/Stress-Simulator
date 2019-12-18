using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class TimeManager : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private float TimeScale = 1;
    [SerializeField] private float totalTime;
    [SerializeField] private float dayNormalized;

    // constant values
    public const float dayPerWeek = 7f;
    public const float hoursPerDay = 24f;
    public const float minutesPerHours = 60f;
    public readonly string[] dayName = { "Senin", "Selasa", "Rabu",
        "Kamis", "Jumat", "Sabtu", "Minggu" };

    [Header("Public Variable")]
    public TimeContainer StartTime;
    public TimeContainer CurrentTime;
    public IntVariable dayShift;

    [Header("Output UI")]
    public StringVariable timeText;
    public StringVariable dayCountText;
    public StringVariable dayNameText;

    [Header("Routine Events")]
    public TimeFormat TriggerTime;
    public UnityEvent OnTriggerTime;

    private bool IsRoutineExecuted;
    public static float currentDay;
    public static float currentHours;
    public static float currentMinutes;
    public static float dayOfTheWeek;
    

    private void Start()
    {
        // set the starting time, default is 00:00 at day 1
        SetTime(StartTime);
    }

    private void Update()
    {
        UpdateTime();

        if (TriggerTime.days == 0)
        {
            ExecuteRoutine();
        }
        else
        {
            ExecuteWeeklyRoutine();
        }
        
    }

    private void OnDisable()
    {
        CurrentTime.time.days = 0;
        CurrentTime.time.hours = 0;
        CurrentTime.time.minutes = 0;
    }

    private void UpdateTime()
    {
        totalTime += Time.deltaTime / TimeScale;

        dayNormalized = totalTime % 1f;

        currentDay = Mathf.Ceil(totalTime);
        currentHours = Mathf.Floor(dayNormalized * hoursPerDay);
        currentMinutes = Mathf.Floor(((dayNormalized * hoursPerDay) % 1f) * minutesPerHours);

        CurrentTime.time.days = currentDay;
        CurrentTime.time.hours = currentHours;
        CurrentTime.time.minutes = currentMinutes;

        dayOfTheWeek = Mathf.Ceil(totalTime) % dayPerWeek;

        timeText.Value = CurrentTime.time.hours.ToString("00") + ":" + CurrentTime.time.minutes.ToString("00");
        dayCountText.Value = (CurrentTime.time.days).ToString("00");
        dayNameText.Value = dayName[(int)Mathf.Clamp(dayOfTheWeek - 1 + dayShift.value, 0, dayPerWeek)];
    }

    public void SetTime(TimeContainer timeContainer)
    {
        totalTime =
            timeContainer.time.days + 
            (timeContainer.time.hours / hoursPerDay) + 
            (timeContainer.time.minutes / hoursPerDay / minutesPerHours);
    }

    public void SkipTime(TimeContainer timeContainer)
    {
        totalTime +=
            timeContainer.time.days +
            (timeContainer.time.hours / hoursPerDay) +
            (timeContainer.time.minutes / hoursPerDay / minutesPerHours);
    }

    public void ResetTime()
    {
        totalTime = 0;
    }

    // execute routine event every day
    public void ExecuteRoutine()
    {
        if (TriggerTime.hours >= CurrentTime.time.hours &&
            TriggerTime.minutes >= CurrentTime.time.minutes)
        {
            if (!IsRoutineExecuted)
            {
                Debug.Log("Executing Routine....");
                OnTriggerTime.Invoke();
                IsRoutineExecuted = true;
            }
        }
        else
        {
            IsRoutineExecuted = false;
        }
    }

    // execute routine event at spesific day of the week
    public void ExecuteWeeklyRoutine()
    {
        if (TriggerTime.hours >= CurrentTime.time.hours &&
            TriggerTime.minutes >= CurrentTime.time.minutes &&
            TriggerTime.days >= dayOfTheWeek)
        {
            if (!IsRoutineExecuted)
            {
                Debug.Log("Executing Routine....");
                OnTriggerTime.Invoke();
                IsRoutineExecuted = true;
            }
        }
        else
        {
            IsRoutineExecuted = false;
        }
    }
}