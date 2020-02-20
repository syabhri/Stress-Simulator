using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeEvents : MonoBehaviour
{
    [System.Serializable]
    public struct TimeEvent
    {
#if UNITY_EDITOR
        [Multiline]
        public string Description;
#endif
        public bool useDayName;
        public TimeFormat TriggerTime;
        public TimeFormat Tolerance; // add day if event extended to tomorrow
        public UnityEvent OnTriggerTime;

        [HideInInspector]
        public bool Executed;

        public void SetExecuted(bool value)
        {
            Executed = value;
        }
    }
    public TimeContainer CurrentTime;
    public bool AlwaysUpdate;

    public List<TimeEvent> timeEvents;
    
    private void OnEnable()
    {
        if (AlwaysUpdate)
            StartCoroutine("RoutineCheck");
        else
            CheckEvents();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator RoutineCheck()
    {
        while (true)
        {
            foreach (TimeEvent timeEvent in timeEvents)
            {
                if (timeEvent.TriggerTime.days > 0)
                {
                    float currentTime = CurrentTime.Value.ToHours();

                    if (timeEvent.TriggerTime.ToHours() >= currentTime &&
                        timeEvent.Tolerance.ToHours() <= currentTime)
                    {
                        if (!timeEvent.Executed)
                        {
                            timeEvent.OnTriggerTime.Invoke();
                            timeEvent.SetExecuted(true);
                            Debug.Log("TimeEvent Executed");
                        }
                    }
                    else
                    {
                        timeEvent.SetExecuted(false);
                    }
                }
                else
                {
                    if (timeEvent.useDayName)
                        if (timeEvent.TriggerTime.dayName != CurrentTime.Value.dayName)
                            yield return null;

                    float currentTime = CurrentTime.Value.ToHoursExt();

                    if (timeEvent.TriggerTime.ToHoursExt() + (CurrentTime.Value.days * TimeManager.hoursPerDay) >= currentTime &&
                        timeEvent.Tolerance.ToHoursExt() + (CurrentTime.Value.days * TimeManager.hoursPerDay) <= currentTime)
                    {
                        if (!timeEvent.Executed)
                        {
                            timeEvent.OnTriggerTime.Invoke();
                            timeEvent.SetExecuted(true);
                            Debug.Log("TimeEvent Executed");
                        }
                    }
                    else
                    {
                        timeEvent.SetExecuted(false);
                    }
                }
            }
            yield return null;
        }
    }

    public void CheckEvents()
    {
        foreach (TimeEvent timeEvent in timeEvents)
        {
            if (timeEvent.TriggerTime.days > 0)
            {
                float currentTime = CurrentTime.Value.ToHours();

                if (timeEvent.TriggerTime.ToHours() >= currentTime &&
                    timeEvent.Tolerance.ToHours() <= currentTime)
                    timeEvent.OnTriggerTime.Invoke();
            }
            else
            {
                if (timeEvent.useDayName)
                    if (timeEvent.TriggerTime.dayName != CurrentTime.Value.dayName)
                        return;

                float currentTime = CurrentTime.Value.ToHoursExt();

                if (timeEvent.TriggerTime.ToHoursExt() + (CurrentTime.Value.days * TimeManager.hoursPerDay) >= currentTime &&
                    timeEvent.Tolerance.ToHoursExt() + (CurrentTime.Value.days * TimeManager.hoursPerDay) <= currentTime)
                    timeEvent.OnTriggerTime.Invoke();
            }
        }
    }
}
