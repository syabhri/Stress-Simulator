using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{
    [SerializeField]
    private float REAL_SECOND_PER_INGAME_DAY = 60f;

    public Text timeText;
    public Text dayText;
    private float day;
    string[] dayName = { "Senin", "Selasa", "Rabu", "Kamis", "Jumat", "Sabtu", "Minggu" };

    private float dayPerWeek = 7f;
    private float hoursPerDay = 24f;
    private float minutesPerHours = 60f;


    private void Awake()
    {

    }

    private void Update()
    {
        day += Time.deltaTime / REAL_SECOND_PER_INGAME_DAY;

        float dayNormalized = day % 1f;

        float dayInAWeek = Mathf.Floor(day) % dayPerWeek;

        string hoursString = Mathf.Floor(dayNormalized * hoursPerDay).ToString("00");

        string minutesString = Mathf.Floor(((dayNormalized * hoursPerDay) % 1f) * minutesPerHours).ToString("00");

        string daysString = Mathf.Floor(day + 1).ToString("00");


        timeText.text = hoursString + ":" + minutesString;
        dayText.text = "Hari Ke - " + daysString + ", " + dayName[(int)dayInAWeek];
    }

    public void skipMinutes(int minutes)
    {
        
    }
}
