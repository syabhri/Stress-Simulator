using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ActivityManager : MonoBehaviour
{
    #region Activity Variables and Object References
    [Header("Properties")]
    [Tooltip("How many energy consumed per hourse of activity")]
    public FloatVariable EnergyPerHour;

    [Header("Data")]
    public FloatVariable energy;
    public FloatVariable stress;
    public FloatVariable money;
    public TimeContainer currentTime;
    public PlayerData playerData;

    [Header("References")]
    public ThingRuntimeSet timeSetterPanel;
    public ThingRuntimeSet timeSetterOkButton;
    public ThingRuntimeSet noticePanel;
    public ThingRuntimeSet player;

    [Header("Events")]
    public GameEvent onTimeSkip;
    public UnityEvent OnActivityStart;
    public UnityEvent OnActivityEnd;

    [Header("Transition")]
    public GameEvent InTransition;
    public GameEvent OutTransition;

    [Header("Data Passer")]
    public TimeContainer timePasser;
    public Activity activity;//reference to the current activity

    [Header("UI Output")]
    public StringVariable noticePanelText;
    public StringVariable schedule;


    //time setter panel ok button;
    private Button okButton;
    #endregion

    #region Unity Calback Function
    // Start is called before the first frame update
    private void Start()
    {
        okButton = timeSetterOkButton.Item.GetComponent<Button>();
    }
    #endregion

    #region Activity Excecution
    public void DoActivity(Activity activity)
    {
        Debug.Log("activity started : " + activity.activityName);//testing
        this.activity = activity;
        OnActivityStart.Invoke();

        //check activity scadule
        if (activity.isScheduled)
        {
            if (!CheckSchedule(activity))
            {
                noticePanelText.Value =
                    "Aktifitas belum tersedia, aktifitas baru bisa di lakukan mulai jam "
                    + activity.schedule.hours.ToString("00") + " : " + activity.schedule.minutes.ToString("00");
                noticePanel.Item.SetActive(true);
                CancelActivity();
                OnActivityEnd.Invoke();
                return;
            }
        }

        if (activity.isLimited)
        {
            if (LimitPerDayReached(activity))
            {
                noticePanelText.Value = "Limit Aktifitas Telah Tercapai, Coba Lagi Besok";
                noticePanel.Item.SetActive(true);
                CancelActivity();
                OnActivityEnd.Invoke();
                return;
            }
        }
        
        noticePanelText.Value = string.Empty;

        if(activity.isDutrationAjustable)
        {
            //open panel to insert duration and change duration to the inputed amount
            timeSetterPanel.Item.SetActive(true);
            Debug.Log("TimeSetter opened");
            okButton.onClick.RemoveAllListeners();
            okButton.onClick.AddListener(delegate { AdjustDuration(); });
            okButton.onClick.AddListener(delegate { Phase2(); });
            return;
        }

        Phase2();
    }

    public void Phase2()
    {
        Debug.Log("Activity Phase2 begin");

        if (activity.isUseEnergy)
            if (!CheckEnergy())
                return;
        if (activity.isCostMoney)
            if (!CheckMoney())
                return;

        if (activity.AnimationTrigger != null)
        {
            player.Deactivate();
            activity.AnimationTrigger.Raise();
            StopAllCoroutines();
            StartCoroutine(StartTransition(1, 2));
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(StartTransition(2));
        }
    }

    public void Phase3() {
        //reset notice panel text
        noticePanelText.Value = "";

        if (activity.isIncreaseStress)
        {
            IncreaseStress();
        }
        
        if (activity.isDecreaseStress)
        {
            DecreaseStress();
        }
        
        if (activity.isChangeOtherStat)
        {
            //change other stat and applied stress penalty
            ChangeOtherStat();
        }

        //apply the bonus interest if activity interest match any of player interest
        if (activity.interest != null && playerData.interest.Count > 0)
        {
            ApplyBonusInterest();
        }

        //apply the ability bonus if activity ability match the player ability
        if (activity.ability != null && playerData.ability != null)
        {
            ApplyBonusAbility();
        }

        EndActivity();
    }

    public void EndActivity()
    {
        //decrease energy according to activity duration if activity used energy
        if (activity.isUseEnergy)
        {
            float consuption = EnergyPerHour.value * activity.duration.ToHours();
            energy.value -= consuption;
            noticePanelText.Value += "<color=red>Energy -" + consuption + "/n";
            Debug.Log("Energy Decreased by " + consuption + ", Energy = " + energy.value);
        }
            
        // decrease the money if activity using money
        if (activity.isCostMoney)
        {
            money.value -= activity.cost;
            noticePanelText.Value += "<color=red>Money -" + activity.cost;
            Debug.Log("Money Decreased by " + activity.cost + ", Money = " + money.value);
        }

        // if activity is limited increase activity count
        if (activity.isLimited)
        {
            activity.currentCount += 1;
        }

        SkipTime();

        OnActivityEnd.Invoke();
        Debug.Log("Activity Ended");
    }

    public void CancelActivity()
    {
        Debug.Log("Activity Canceled");
    }

    IEnumerator StartTransition(float duration)
    {
        OutTransition.Raise();
        yield return new WaitForSeconds(duration);
        Phase3();
        InTransition.Raise();
        noticePanel.Acitvate();
    }

    IEnumerator StartTransition(float delay,float duration)
    {
        yield return new WaitForSeconds(delay);
        OutTransition.Raise();
        yield return new WaitForSeconds(duration);
        player.Acitvate();
        Phase3();
        InTransition.Raise();
        noticePanel.Acitvate();
    }

    public void AdjustDuration()
    {
        activity.duration.PassValue(timePasser.time);
    }

    //check wether the player have enough energy to do the activity
    public bool CheckEnergy()
    {
        float consuption = activity.duration.ToHours() * EnergyPerHour.value;
        if (energy.value >= consuption)
        {
            Debug.Log("Energy Enough, Energy -" + consuption);
            return true;
        }
        else //show message "you too tired"
        {
            string message = "Energi Tidak Cukup, Butuh Setidaknya " + consuption + " energi";
            noticePanelText.Value = message;
            noticePanel.Item.SetActive(true);
            Debug.Log(message);
            Debug.Log("Activity Ended");
            OnActivityEnd.Invoke();
            return false;
        }
    }

    public bool CheckMoney()
    {
        if (money.value >= activity.cost)
        {
            Debug.Log("Money Enough : -" + activity.cost);
            return true;
        }
        else //show message "you dont have enough money"
        {
            string message = "Koin Tidak Cukup, Butuh Setidaknya " + activity.cost + " Koin";
            noticePanelText.Value = message;
            noticePanel.Item.SetActive(true);
            Debug.Log(message);
            Debug.Log("Activity Ended");
            OnActivityEnd.Invoke();
            return false;
        }
    }

    public void IncreaseStress()
    {
        float old = stress.value;
        if (activity.increaseStressByHours)
            stress.value += activity.increasedStressMultiplier * activity.duration.ToHours();
        else
            stress.value += activity.increasedStressMultiplier;

        if (stress.value > 100)
            stress.value = 100;
        noticePanelText.Value += "<color=red>Stress +" + (stress.value - old) + "/n";
        Debug.Log("Stress +" + (stress.value - old) + ", Stress = " + stress.value);
    }


    public void DecreaseStress()
    {
        float old = stress.value;
        if (activity.decreaseStressByHours)
            stress.value -= activity.decreasedStressMultiplier * activity.duration.ToHours();
        else
            stress.value -= activity.decreasedStressMultiplier;
        if (stress.value < 0)
            stress.value = 0;
        noticePanelText.Value += "<color=green>Stress " + (stress.value - old) + "/n";
        Debug.Log("Stress " + (stress.value - old) + ", Stress = " + stress.value);
    }

    //change other stat based on the operation configured and stress level if effected
    public void ChangeOtherStat()
    {
        // create temp reference for sorter call
        FloatPair pair = activity.otherStat;
        // create temp storage to store the effector value 
        float effector;
        // create temp storage for storing the target stat value before the change
        float old = pair.target.value;

        //assign the effector to local or external effector
        if (pair.useExternalEeffector)
            effector = pair.externalEffector.value;
        else
            effector = pair.effector;

        // multiply the effector value with the activity duration if the stat is changed by the hours
        if (activity.ChangeStatByHours)
        {
            effector *= activity.duration.ToHours();
        }

        // process the effector if the target stat is effected by stress 
        if (activity.isEffectedByStress)
            effector = EffectedByStress(effector);

        //change the target stat according to the configured operation and effector
        pair.DoOperation(effector);

        //limit range so the stat can't go above 100 or below 0
        pair.target.value = Mathf.Clamp(pair.target.value, 0, 100);

        noticePanelText.Value += 
            "<color=" + Mathf.Sign(pair.target.value - old).ToString().Replace("-1","red").Replace("1","green") + ">" + 
            pair.target.name + " " + 
            Mathf.Sign(pair.target.value - old).ToString().Replace("-1","+").Replace('1','+') + 
            Mathf.Abs(pair.target.value - old) + "/n";

        Debug.Log("Stat " + pair.target.name + " Changed from " +
            old + " to " + pair.target.value +
            " using " + pair.operation + " operation with " +
            effector + " effector value");
    }

    private float EffectedByStress(float effector)
    {
        //decrease factor is how much the effector value will decrease by stress
        float decreaseFactor;

        //find the decrease factor
        decreaseFactor = Mathf.Round(effector * stress.value / 100);

        //decrease the effector according to the decrease factor
        effector -= decreaseFactor;

        Debug.Log("Activity Effected By Stress, effector decreased by "+ decreaseFactor);

        //output the decreaased effector
        return effector;
    }

    public void ApplyBonusInterest()
    {
        //cycle trough player interest
        foreach (FloatPairContainer interest in playerData.interest)
        {
            float old = activity.interest.pair.target.value; ;
            //if activity interest match the player interest
            if (activity.interest.name == interest.name)
            {
                old = activity.interest.pair.target.value;

                //and the stat is change by the hours
                if (activity.ChangeStatByHours)
                {
                    //apply the interest bonus multiply by the activity duration
                    activity.interest.pair.DoOperation((int)activity.duration.ToHours());
                }
                else
                {
                    activity.interest.pair.DoOperation();
                }

                Debug.Log("Interest Applied " + old + " > " + activity.interest.pair.target.value);
            }     
        }   
    }

    public void ApplyBonusAbility()
    {
        float old;
        if (activity.ability.name == playerData.ability.name)
        {
            old = activity.ability.pair.target.value;
            if (activity.ChangeStatByHours)
                activity.ability.pair.DoOperation((int)activity.duration.ToHours());
            else
                activity.ability.pair.DoOperation();

            Debug.Log("Ability Applied " + old + " > " + activity.ability.pair.target.value);
        }
    }
    #endregion

    #region Activity Scheduling

    // return true if atcivity is on scadule
    public bool CheckSchedule(Activity activity)
    {
        
        if (currentTime.time.hours >= activity.schedule.hours
            && currentTime.time.minutes >= activity.schedule.minutes
            && currentTime.time.hours <= activity.tolerance.hours
            && currentTime.time.minutes <= activity.tolerance.minutes)
        {
            return true;
        }
        return false;
    }

    public void PrintCurrrentSchedule(ActivitySet activities)
    {
        schedule.Value = string.Empty;

        foreach (Activity activity in activities.activities)
        {
            if (activity.isScheduled)
            {
                if (activity.schedule.days == TimeManager.dayOfTheWeek)
                {
                    if (schedule.Value != "")
                        schedule.Value += "/n";

                    schedule.Value += activity.name + " : " + activity.schedule.hours.ToString("00")
                        + ":" + activity.schedule.minutes.ToString("00");
                }
            }
        }
    }

    public static bool LimitPerDayReached(Activity activity)
    {
        if (activity.currentCount >= activity.limitPerDay)
            return true;
        else
            return false;
    }

    public void SkipTime()
    {
        timePasser.time.PassValue(activity.duration);
        onTimeSkip.Raise();
    }

    /*
    public bool CheckValueLimit()
    {
        if (activity.currentValue > activity.valueLimit)
            return true;
        else
            return false;
    }*/
    #endregion
}
