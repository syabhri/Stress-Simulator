using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// script to manage time, currently limited to week
// disfunctional script : total time must be save in one of the time unit, chosing biggest time unit
// will result in more efficient memory but hinder the expandibiity. chosing smallest time unit will resulting otherwise
public class TimeManager2 : MonoBehaviour
{
    [Header("Properties")]
    public float timeScale = 1; // default 1:1 second
    public TimeFormat startTime; //offset to starting time;

    [Header("External Variables")]


    // saved timeunit
    public float currentSecond;
    public float currentMinute;
    public float currentHour;
    public float currentDay;
    public float dayOfTheWeek;

    // timeunit scale
    public const float secondsPerMinute = 60f;
    public const float minutesPerHour = 60f;
    public const float hoursPerDay = 24f;
    public const float dayPerWeek = 7f;

    // dayname
    public readonly string[] dayName = { "Senin", "Selasa", "Rabu",
        "Kamis", "Jumat", "Sabtu", "Minggu" };


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTime();
    }

    public void UpdateTime()
    {
        currentSecond += Mathf.Floor(Time.deltaTime * timeScale);

        currentMinute = Mathf.Floor(currentSecond / secondsPerMinute % minutesPerHour);
        currentHour = Mathf.Floor(currentMinute / minutesPerHour % hoursPerDay);
        currentDay = Mathf.Floor(currentHour / hoursPerDay) + 1;

        dayOfTheWeek = Mathf.Floor(currentDay % dayPerWeek) -1;

    }

}
