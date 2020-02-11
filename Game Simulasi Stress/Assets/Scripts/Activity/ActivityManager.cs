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
    public float EnergyPerHour;

    [Header("Data")]
    public PlayerData playerData;
    public TimeContainer currentTime;

    [Header("References")]
    public GameObjectContainer timeSetterPanel;
    public GameObjectContainer timeSetterOkButton;
    public GameObjectContainer noticePanel;
    public GameObjectContainer player;

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
    public StringContainer noticePanelText;
    public StringContainer schedule;


    //time setter panel ok button;
    private Button okButton;
    #endregion

    #region Unity Calback Function
    // Start is called before the first frame update
    private void Start()
    {
        okButton = timeSetterOkButton.Value.GetComponent<Button>();
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
                noticePanel.Value.SetActive(true);
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
                noticePanel.Value.SetActive(true);
                CancelActivity();
                OnActivityEnd.Invoke();
                return;
            }
        }
        
        noticePanelText.Value = string.Empty;

        if(activity.isDutrationAjustable)
        {
            //open panel to insert duration and change duration to the inputed amount
            timeSetterPanel.Value.SetActive(true);
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
            player.Value.SetActive(false);
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
            float consuption = EnergyPerHour * activity.duration.ToHours();
            playerData.energy.Value = playerData.energy.Value - consuption;
            noticePanelText.Value += "<color=red>Energy -" + consuption + "/n";
            Debug.Log("Energy Decreased by " + consuption + ", Energy = " + playerData.energy.Value);
        }
            
        // decrease the money if activity using money
        if (activity.isCostMoney)
        {
            playerData.coins.Value = playerData.coins.Value - activity.cost;
            noticePanelText.Value += "<color=red>Money -" + activity.cost;
            Debug.Log("Money Decreased by " + activity.cost + ", Money = " + playerData.coins.Value);
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
        noticePanel.Value.SetActive(true);
    }

    IEnumerator StartTransition(float delay,float duration)
    {
        yield return new WaitForSeconds(delay);
        OutTransition.Raise();
        yield return new WaitForSeconds(duration);
        player.Value.SetActive(true);
        Phase3();
        InTransition.Raise();
        noticePanel.Value.SetActive(true);
    }

    public void AdjustDuration()
    {
        activity.duration.SetValue(timePasser.Value);
    }

    //check wether the player have enough energy to do the activity
    public bool CheckEnergy()
    {
        float consuption = activity.duration.ToHours() * EnergyPerHour;
        if (playerData.energy.Value >= consuption)
        {
            Debug.Log("Energy Enough, Energy -" + consuption);
            return true;
        }
        else //show message "you too tired"
        {
            string message = "Energi Tidak Cukup, Butuh Setidaknya " + consuption + " energi";
            noticePanelText.Value = message;
            noticePanel.Value.SetActive(true);
            Debug.Log(message);
            Debug.Log("Activity Ended");
            OnActivityEnd.Invoke();
            return false;
        }
    }

    public bool CheckMoney()
    {
        if (playerData.coins.Value >= activity.cost)
        {
            Debug.Log("Money Enough : -" + activity.cost);
            return true;
        }
        else //show message "you dont have enough money"
        {
            string message = "Koin Tidak Cukup, Butuh Setidaknya " + activity.cost + " Koin";
            noticePanelText.Value = message;
            noticePanel.Value.SetActive(true);
            Debug.Log(message);
            Debug.Log("Activity Ended");
            OnActivityEnd.Invoke();
            return false;
        }
    }

    public void IncreaseStress()
    {
        float old = playerData.stressLevel.Value;
        if (activity.increaseStressByHours)
            playerData.stressLevel.Value = playerData.stressLevel.Value + (activity.increasedStressMultiplier * activity.duration.ToHours());
        else
            playerData.stressLevel.Value = playerData.stressLevel.Value + activity.increasedStressMultiplier;

        if (playerData.stressLevel.Value > 100) playerData.stressLevel.Value = 100;

        noticePanelText.Value += "<color=red>Stress +" + (playerData.stressLevel.Value - old) + "/n";
        Debug.Log("Stress +" + (playerData.stressLevel.Value - old) + ", Stress = " + playerData.stressLevel.Value);
    }


    public void DecreaseStress()
    {
        float old = playerData.stressLevel.Value;
        if (activity.decreaseStressByHours)
            playerData.stressLevel.Value = playerData.stressLevel.Value - (activity.decreasedStressMultiplier * activity.duration.ToHours());
        else
            playerData.stressLevel.Value = playerData.stressLevel.Value - activity.decreasedStressMultiplier;
        if (playerData.stressLevel.Value < 0)
            playerData.stressLevel.Value = 100;
        noticePanelText.Value += "<color=green>Stress " + (playerData.stressLevel.Value - old) + "/n";
        Debug.Log("Stress " + (playerData.stressLevel.Value - old) + ", Stress = " + playerData.stressLevel.Value);
    }

    //change other stat based on the operation configured and stress level if effected
    public void ChangeOtherStat()
    {
        // create temp reference for sorter call
        FloatPair pair = activity.otherStat;
        // create temp storage to store the effector Value 
        float effector;
        // create temp storage for storing the target stat Value before the change
        float old = pair.target.Value;

        //assign the effector to local or external effector
        if (pair.useExternalEeffector)
            effector = pair.externalEffector.Value;
        else
            effector = pair.effector;

        // multiply the effector Value with the activity duration if the stat is changed by the hours
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
        pair.target.Value = Mathf.Clamp(pair.target.Value, 0, 100);

        noticePanelText.Value += 
            "<color=" + Mathf.Sign(pair.target.Value - old).ToString().Replace("-1","red").Replace("1","green") + ">" + 
            pair.target.name + " " + 
            Mathf.Sign(pair.target.Value - old).ToString().Replace("-1","+").Replace('1','+') + 
            Mathf.Abs(pair.target.Value - old) + "/n";

        Debug.Log("Stat " + pair.target.name + " Changed from " +
            old + " to " + pair.target.Value +
            " using " + pair.operation + " operation with " +
            effector + " effector Value");
    }

    private float EffectedByStress(float effector)
    {
        //decrease factor is how much the effector Value will decrease by stress
        float decreaseFactor;

        //find the decrease factor
        decreaseFactor = Mathf.Round(effector * playerData.stressLevel.Value / 100);

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
            float old = activity.interest.Value.target.Value; ;
            //if activity interest match the player interest
            if (activity.interest.name == interest.name)
            {
                old = activity.interest.Value.target.Value;

                //and the stat is change by the hours
                if (activity.ChangeStatByHours)
                {
                    //apply the interest bonus multiply by the activity duration
                    activity.interest.Value.DoOperation((int)activity.duration.ToHours());
                }
                else
                {
                    activity.interest.Value.DoOperation();
                }

                Debug.Log("Interest Applied " + old + " > " + activity.interest.Value.target.Value);
            }     
        }   
    }

    public void ApplyBonusAbility()
    {
        float old;
        if (activity.ability.name == playerData.ability.name)
        {
            old = activity.ability.Value.target.Value;
            if (activity.ChangeStatByHours)
                activity.ability.Value.DoOperation((int)activity.duration.ToHours());
            else
                activity.ability.Value.DoOperation();

            Debug.Log("Ability Applied " + old + " > " + activity.ability.Value.target.Value);
        }
    }
    #endregion

    #region Activity Scheduling

    // return true if atcivity is on scadule
    public bool CheckSchedule(Activity activity)
    {
        
        if (currentTime.Value.hours >= activity.schedule.hours
            && currentTime.Value.minutes >= activity.schedule.minutes
            && currentTime.Value.hours <= activity.tolerance.hours
            && currentTime.Value.minutes <= activity.tolerance.minutes)
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
                if (activity.schedule.days == TimeManager.currentDay % 7f)
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
        timePasser.Value = activity.duration;
        onTimeSkip.Raise();
    }

    /*
    public bool CheckValueLimit()
    {
        if (activity.currentValue > activity.ValueLimit)
            return true;
        else
            return false;
    }*/
    #endregion
}
