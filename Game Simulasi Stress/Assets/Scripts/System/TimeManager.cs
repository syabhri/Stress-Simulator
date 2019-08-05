using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    [Header("Properties")]
    public float totalTime;
    public float dayNormalized;
    public float dayInAWeek;

    [Space]
    public const float dayPerWeek = 7f;
    public const float hoursPerDay = 24f;
    public const float minutesPerHours = 60f;
    public string[] dayName = { "Senin", "Selasa", "Rabu", "Kamis", "Jumat", "Sabtu", "Minggu" };

    [Header("External Variable")]
    public FloatVariable TimeScale;
    public TimeContainer StartTime;
    public TimeContainer CurrentTime;
    public IntVariable dayShift;

    [Header("UI Output")]
    public StringVariable timeText;
    public StringVariable dayCountText;
    public StringVariable dayNameText;

    private void Start()
    {
        SetTime(StartTime);
    }

    private void Update()
    {
        UpdateTime();       
    }

    private void OnDisable()
    {
        CurrentTime.time.days = 0;
        CurrentTime.time.hours = 0;
        CurrentTime.time.minutes = 0;
    }

    private void UpdateTime()
    {
        totalTime += Time.deltaTime / TimeScale.value;

        dayNormalized = totalTime % 1f;

        CurrentTime.time.hours = Mathf.Floor(dayNormalized * hoursPerDay);
        CurrentTime.time.minutes = Mathf.Floor(((dayNormalized * hoursPerDay) % 1f) * minutesPerHours);
        CurrentTime.time.days = Mathf.Floor(totalTime);

        dayInAWeek = Mathf.Floor(totalTime) % dayPerWeek;

        timeText.Value = CurrentTime.time.hours.ToString("00") + ":" + CurrentTime.time.minutes.ToString("00");
        dayCountText.Value = (CurrentTime.time.days + 1).ToString("00");
        dayNameText.Value = dayName[(int)Mathf.Clamp(dayInAWeek + dayShift.value, 0, dayPerWeek)];
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
}