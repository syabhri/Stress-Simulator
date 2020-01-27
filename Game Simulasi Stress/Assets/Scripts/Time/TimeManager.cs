using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class TimeManager : MonoBehaviour
{
    // constant values
    public const float dayPerWeek = 7f;
    public const float hoursPerDay = 24f;
    public const float minutesPerHours = 60f;
    public readonly string[] dayName = { "Senin", "Selasa", "Rabu",
        "Kamis", "Jumat", "Sabtu", "Minggu" };

    [Header("Properties")]
    [SerializeField] private float TimeScale = 1;

    [Header("External Variables")]
    public TimeContainer StartTime;
    public TimeContainer EndTime;
    public TimeContainer CurrentTime;

    [Header("Output UI")]
    public StringVariable timeText;
    public StringVariable dayCountText;
    public StringVariable dayNameText;

    [Header("Events")]
    public UnityEvent OnEndTime;
    public UnityEvent OnRoutine;

    public static float currentDay;
    public static float currentHours;
    public static float currentMinutes;
    public static float dayOfTheWeek;

    // rotine properties
    private bool IsRoutineExecuted = false;
    private int lastDayExecuted;

    private bool IsEndTimeExecuted = false;

    // time counter
    private float totalTime;
    private float dayNormalized;

    private void Start()
    {
        // set the starting time, default is 00:00 at day 1
        SetTime(StartTime);
    }

    private void Update()
    {
        UpdateTime();
        ExecuteRoutine();

        if (CurrentTime.time.days >= EndTime.time.days &&
            CurrentTime.time.hours >= EndTime.time.hours &&
            CurrentTime.time.minutes >= EndTime.time.minutes &&
            IsEndTimeExecuted == false)
        {
            OnEndTime.Invoke();
            Debug.Log("Time Ended");
            IsEndTimeExecuted = true;
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
        dayNameText.Value = dayName[(int)dayOfTheWeek];
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
        if (!IsRoutineExecuted)
        {
            lastDayExecuted = (int)CurrentTime.time.days;
            OnRoutine.Invoke();
            IsRoutineExecuted = true;
            Debug.Log("Routine Executed!");
        }
        else
        {
            if (CurrentTime.time.days > lastDayExecuted)
            {
                IsRoutineExecuted = false;
            }
        }
    }
}