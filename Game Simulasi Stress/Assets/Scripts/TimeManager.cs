using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeManager : MonoBehaviour
{
    [SerializeField]
    private float RealSecondPerInGameDay = 60f;

    public TextMeshProUGUI timeText;
    public TextMeshProUGUI dayText;
    private float day;
    string[] dayName = { "Senin", "Selasa", "Rabu", "Kamis", "Jumat", "Sabtu", "Minggu" };

    private float dayPerWeek = 7f;
    private float hoursPerDay = 24f;
    private float minutesPerHours = 60f;

    private void Update()
    {
        UpdateTime();
    }

    private void UpdateTime()
    {
        day += Time.deltaTime / RealSecondPerInGameDay;

        float dayNormalized = day % 1f;

        float dayInAWeek = Mathf.Floor(day) % dayPerWeek;

        string hoursString = Mathf.Floor(dayNormalized * hoursPerDay).ToString("00");
        string minutesString = Mathf.Floor(((dayNormalized * hoursPerDay) % 1f) * minutesPerHours).ToString("00");
        string daysString = Mathf.Floor(day + 1).ToString("00");


        timeText.text = hoursString + ":" + minutesString;
        dayText.text = "Hari Ke - " + daysString + ", " + dayName[(int)dayInAWeek];
    }

    public void StopTime()
    {
        if (Time.timeScale == 1)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
    }

    public void SkipTimeByDay(float days)
    {
        day += days;
    }

    public void SkipTimeByHour(float hours)
    {
        day += hours / hoursPerDay;
    }

    public void SkipTimeByMinute(float minutes)
    {
        day += minutes / hoursPerDay / minutesPerHours;
    }
}
