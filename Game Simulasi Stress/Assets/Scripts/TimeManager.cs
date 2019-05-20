using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    [Header("Public Variable")]
    public FloatVariable TimeScale;
    public TimeContainer CurrentTime;
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

    private void Update()
    {
        if (updateTime.value)
            UpdateTime();       
    }

    private void UpdateTime()
    {
        totalTime += Time.deltaTime / TimeScale.value;

        float dayNormalized = totalTime % 1f;

        CurrentTime.hours = Mathf.Floor(dayNormalized * hoursPerDay);
        CurrentTime.minutes = Mathf.Floor(((dayNormalized * hoursPerDay) % 1f) * minutesPerHours);
        CurrentTime.days = Mathf.Floor(totalTime);

        float dayInAWeek = Mathf.Floor(totalTime) % dayPerWeek;

        timeText.Value = CurrentTime.hours.ToString("00") + ":" + CurrentTime.minutes.ToString("00");
        dayCountText.Value = "Hari Ke - " + (CurrentTime.days + 1).ToString("00");
        dayNameText.Value = dayName[(int)dayInAWeek];
    }

    public void ToggleTime()
    {
        if (updateTime.value)
            updateTime.value = false;
        else
            updateTime.value = true;
    }

    public void SetTime(TimeContainer time)
    {
        totalTime = 
            time.days + 
            (time.hours / hoursPerDay) + 
            (time.minutes / hoursPerDay / minutesPerHours);
    }

    public void SkipTime(TimeContainer time)
    {
        totalTime +=
            time.days +
            (time.hours / hoursPerDay) +
            (time.minutes / hoursPerDay / minutesPerHours);
    }

    public void ResetTime()
    {
        totalTime = 0;
    }
}