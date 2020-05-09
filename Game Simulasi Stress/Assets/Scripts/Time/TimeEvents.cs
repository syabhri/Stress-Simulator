using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeEvents : MonoBehaviour
{
    [System.Serializable]
    public class TimeEvent
    {
#if UNITY_EDITOR
        [Multiline]
        public string Description;
#endif
        public bool useDayName;
        public TimeFormat TriggerTime;
        public TimeFormat Tolerance; // add day if event extended to tomorrow
        public UnityEvent OnTriggerTime;

        public bool executed;
        public int expirationTime;
    }
    public enum UpdateMethode { Manual, Smart, Realtime}
    public TimeContainer CurrentTime;
    public UpdateMethode updateMethode;

    public List<TimeEvent> timeEvents;

    private void OnEnable()
    {
        ResetEvent();

        switch (updateMethode)
        {
            case UpdateMethode.Manual:
                CheckEvents();
                break;
            case UpdateMethode.Smart:
                CurrentTime.OnValueChanged += CheckEvents;
                break;
            case UpdateMethode.Realtime:
                StartCoroutine("RoutineCheck");
                break;
            default:
                break;
        }
    }

    private void OnDisable()
    {
        CurrentTime.OnValueChanged -= CheckEvents;
        StopAllCoroutines();
        ResetEvent();
    }

    private IEnumerator RoutineCheck()
    {
        while (true)
        {
            CheckEvents();
            yield return null;
        }
    }

    public void CheckEvents()
    {
        foreach (TimeEvent timeEvent in timeEvents)
        {
            CheckEvent(timeEvent);
        }
    }

    public void CheckEvents(TimeContainer time)
    {
        foreach (TimeEvent timeEvent in timeEvents)
        {
            CheckEvent(timeEvent);
        }
    }

    public void CheckEvent(TimeEvent timeEvent)
    {
        // convert all time into 1 time scale (Minutes)
        int currentTime = CurrentTime.Value.ToMinutes();
        int triggerTime = timeEvent.TriggerTime.ToMinutes();
        int tolerance = timeEvent.Tolerance.ToMinutes();

        // if TimeEvent is executed and not expired then return, otherwise continue
        if (timeEvent.executed)
        {
            if (currentTime > timeEvent.expirationTime)
            {
                timeEvent.executed = false;
                CheckEvent(timeEvent);
            }
            return;
        }

        // if the TriggerTime days is 0 then add current day instead
        if (timeEvent.TriggerTime.days <= 0 && timeEvent.Tolerance.days <= 0)
        {
            int currentDay = TimeManager.DaysToMinute(CurrentTime.Value.days);
            triggerTime += currentDay;
            if (tolerance > 0)
                tolerance += currentDay;
        }

        // if TimeEvent time Tolerance is 0 then only execute TimeEvent at TriggerTime
        if (tolerance <= 0)
        {
            if (triggerTime == currentTime)
            {
                timeEvent.OnTriggerTime.Invoke();
                timeEvent.expirationTime = currentTime + 1;
                timeEvent.executed = true;
            }
        }
        else //otherwise continue
        {
            // if TimeTrigger Time Is Later Than Tolerance then execute time event
            if (triggerTime > tolerance)
            {
                // if day name is used and is match with current day name then continue otherwise return
                if (timeEvent.useDayName)
                    if (TimeManager.NextDayName(timeEvent.TriggerTime.dayName) != CurrentTime.Value.dayName)
                        return;

                // and set expiration time to today if tolerance is later than current time
                if (currentTime <= tolerance)
                {
                    timeEvent.OnTriggerTime.Invoke();
                    timeEvent.expirationTime = tolerance;
                    timeEvent.executed = true;
                }
                // or set it to tomorrow if triggerTime is later than current time
                else if (currentTime >= triggerTime)
                {
                    timeEvent.OnTriggerTime.Invoke();
                    timeEvent.expirationTime = tolerance + TimeManager.DaysToMinute(1);
                    timeEvent.executed = true;
                }
            }

            else
            {
                // if day name is used and is match with current day name then continue otherwise return
                if (timeEvent.useDayName)
                    if (timeEvent.TriggerTime.dayName != CurrentTime.Value.dayName)
                        return;

                if (currentTime >= triggerTime && currentTime <= tolerance)
                {
                    timeEvent.OnTriggerTime.Invoke();
                    timeEvent.expirationTime = tolerance;
                    timeEvent.executed = true;
                }
            } 
        }
    }


    /*
    public void CheckEventOld(TimeEvent timeEvent)
    {
        // convert all time to 1 scale of time (in this case minutes)
        int currentDayTime = CurrentTime.Value.ToMinutes();

        if (timeEvent.executed)
        {
            if (currentDayTime > timeEvent.expirationTime)
            {
                timeEvent.executed = false;
                CheckEvent(timeEvent);
            }
        }

        // if the tollerance time is 0 then 
        else if (timeEvent.Tolerance.ToMinutes() <= 0)
        {
            if (timeEvent.TriggerTime.days > 0)
            {
                if (timeEvent.TriggerTime.ToMinutes() == currentDayTime)
                {
                    timeEvent.OnTriggerTime.Invoke();
                    timeEvent.expirationTime = currentDayTime;
                    timeEvent.executed = true;
                }
            }
            else
            {
                if (timeEvent.TriggerTime.ToMinutesExt() + TimeManager.DaysToMinute(CurrentTime.Value.days) == currentDayTime)
                {
                    timeEvent.OnTriggerTime.Invoke();
                    timeEvent.expirationTime = currentDayTime;
                    timeEvent.executed = true;
                }
            }
        }

        else if (timeEvent.TriggerTime.days > 0) // change current time to use
        {
            if (currentDayTime >= timeEvent.TriggerTime.ToMinutes() &&
              currentDayTime <= timeEvent.Tolerance.ToMinutes())
            {
                timeEvent.OnTriggerTime.Invoke();
                timeEvent.expirationTime = timeEvent.Tolerance.ToMinutes();
                timeEvent.executed = true;
            }
        }

        else
        {
            if (timeEvent.useDayName)
                if (timeEvent.TriggerTime.dayName != CurrentTime.Value.dayName)
                    return;

            int currentTime = CurrentTime.Value.ToMinutesExt();
            int currentDay = TimeManager.DaysToMinute(CurrentTime.Value.days);

            if (timeEvent.TriggerTime.ToMinutesExt() > timeEvent.Tolerance.ToMinutesExt())
            {
                if (currentTime <= timeEvent.Tolerance.ToMinutesExt())
                {
                    timeEvent.OnTriggerTime.Invoke();
                    timeEvent.expirationTime = timeEvent.Tolerance.ToMinutesExt() + currentDay;
                    timeEvent.executed = true;
                }
                else
                {
                    if (currentTime >= timeEvent.TriggerTime.ToMinutesExt())
                    {
                        timeEvent.OnTriggerTime.Invoke();
                        timeEvent.expirationTime = timeEvent.Tolerance.ToMinutesExt() + currentDay + 1;
                        timeEvent.executed = true;
                    }
                }
            }
            else
            {
                if (currentTime >= timeEvent.TriggerTime.ToMinutesExt() &&
                    currentTime <= timeEvent.Tolerance.ToMinutesExt() )
                {
                    timeEvent.OnTriggerTime.Invoke();
                    timeEvent.expirationTime = timeEvent.Tolerance.ToMinutesExt() + currentDay;
                    timeEvent.executed = true;
                }
            }
        }
    }*/

    public void ResetEvent()
    {
        foreach (TimeEvent timeEvent in timeEvents)
        {
            timeEvent.executed = false;
        }
    }
}
