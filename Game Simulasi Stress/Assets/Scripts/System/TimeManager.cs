using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    [Header("Public Variable")]
    public FloatVariable TimeScale;
    public TimeContainer CurrentTime;
    public TimeContainer StartTime;
    public BoolVariable updateTime;

    [Header("UI Output")]
    public StringVariable timeText;
    public StringVariable dayCountText;
    public StringVariable dayNameText;
    
    
    private float totalTime;

    const float dayPerWeek = 7f;
    const float hoursPerDay = 24f;
    const float minutesPerHours = 60f;
    readonly string[] dayName = { "Senin", "Selasa", "Rabu", "Kamis", "Jumat", "Sabtu", "Minggu" };

    private void Start()
    {
        SetTime(StartTime);
    }

    private void Update()
    {
        if (updateTime.value)
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

        float dayNormalized = totalTime % 1f;

        CurrentTime.time.hours = Mathf.Floor(dayNormalized * hoursPerDay);
        CurrentTime.time.minutes = Mathf.Floor(((dayNormalized * hoursPerDay) % 1f) * minutesPerHours);
        CurrentTime.time.days = Mathf.Floor(totalTime);

        float dayInAWeek = Mathf.Floor(totalTime) % dayPerWeek;

        timeText.Value = CurrentTime.time.hours.ToString("00") + ":" + CurrentTime.time.minutes.ToString("00");
        dayCountText.Value = "Hari Ke - " + (CurrentTime.time.days + 1).ToString("00");
        dayNameText.Value = dayName[(int)dayInAWeek];
    }

    public void ToggleTime()
    {
        if (updateTime.value)
            updateTime.value = false;
        else
            updateTime.value = true;
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