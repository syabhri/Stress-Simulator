using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Linq;

public class ActivityManager : MonoBehaviour
{
    #region Activity Variables and Object References
    [Header("Data")]
    public PlayerData playerData;

    [Header("References")]
    public GameObjectContainer player;

    [Header("Time Setter")]
    public TimeContainer currentTime;
    public GameEvent TimeSetterTrigger;

    [Header("Time Skip Panel")]
    public TimeContainer timePasser;
    public GameEvent onTimeSkip;

    [Header("Transition")]
    public float TransitionLength = 2;
    public GameEvent InTransition;
    public GameEvent OutTransition;

    [Header("Notice Panel")]
    public GameEvent NoticeTrigger;
    public StringContainer noticePanelText;

    [Header("Events")]
    public UnityEvent OnActivityStart;
    public UnityEvent OnActivityEnd;

    //reference to the current activity
    private Activity currentActivity;
    #endregion

    #region Activity Excecution
    public void DoActivity(Activity activity)
    {
        Debug.Log("activity started : " + activity.name);//testing
        currentActivity = activity;
        OnActivityStart.Invoke();

        if (activity.limitPerDay > 0)
        {
            if (LimitPerDayReached(activity, playerData))
            {
                noticePanelText.Value = "Limit Aktifitas Telah Tercapai, Coba Lagi Besok";
                NoticeTrigger.Raise();
                CancelActivity();
                OnActivityEnd.Invoke();
                return;
            }
        }
        
        noticePanelText.Value = string.Empty;

        if(activity.isDutrationAjustable)
        {
            //open panel to insert duration and change duration to the inputed amount
            TimeSetterTrigger.Raise();
            return;
        }

        Phase2();
    }

    public void Phase2()
    {
        Debug.Log("Activity Phase2 begin");

        if (currentActivity.energyPerHour > 0)
            if (!CheckEnergy())
                return;
        if (currentActivity.cost > 0)
            if (!CheckMoney())
                return;

        if (currentActivity.animationTrigger != null)
        {
            player.Value.SetActive(false);
            currentActivity.animationTrigger.Raise();
            StopAllCoroutines();
            StartCoroutine(StartTransition(currentActivity.trannsitionDelay, TransitionLength));
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(StartTransition(TransitionLength));
        }
    }

    public void Phase3() {
        //reset notice panel text
        noticePanelText.Value = string.Empty;

        if (currentActivity.isIncreaseStress)
        {
            IncreaseStress();
        }
        
        if (currentActivity.isDecreaseStress)
        {
            DecreaseStress();
        }
        
        if (currentActivity.isChangeOtherStat)
        {
            //change other stat and applied stress penalty
            ChangeOtherStat();
        }

        EndActivity();
    }

    public void EndActivity()
    {
        //decrease energy according to activity duration if activity used energy
        if (currentActivity.energyPerHour > 0)
        {
            float consuption = currentActivity.energyPerHour * currentActivity.duration.ToHours();
            playerData.energy.Value = playerData.energy.Value - consuption;
            noticePanelText.Value += "<color=red>Energy -" + Mathf.Round(consuption) + "/n";
            Debug.Log("Energy Decreased by " + consuption + ", Energy = " + playerData.energy.Value);
        }
            
        // decrease the money if activity using money
        if (currentActivity.cost > 0)
        {
            playerData.coins.Value = playerData.coins.Value - currentActivity.cost;
            noticePanelText.Value += "<color=red>Money -" + currentActivity.cost;
            Debug.Log("Money Decreased by " + currentActivity.cost + ", Money = " + playerData.coins.Value);
        }

        // if activity is limited increase activity count
        if (currentActivity.limitPerDay > 0)
        {
            if (!playerData.ActivityLimitCount.Any(l => l.name == currentActivity.name))
            {
                playerData.ActivityLimitCount.Add(new PlayerData.ActivityLimit(currentActivity.name, 1));
            }
            else
            {
                playerData.ActivityLimitCount.FirstOrDefault(l => l.name == currentActivity.name).count += 1;
            }
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
        NoticeTrigger.Raise();
    }

    IEnumerator StartTransition(float delay,float duration)
    {
        yield return new WaitForSeconds(delay);
        OutTransition.Raise();
        yield return new WaitForSeconds(duration);
        player.Value.SetActive(true);
        Phase3();
        InTransition.Raise();
        NoticeTrigger.Raise();
    }

    public void AdjustDuration(TimeContainer duration)
    {
        currentActivity.duration.SetValue(duration.Value);
        Phase2();
    }

    //check wether the player have enough energy to do the activity
    public bool CheckEnergy()
    {
        float consuption = currentActivity.duration.ToHours() * currentActivity.energyPerHour;
        if (playerData.energy.Value >= consuption)
        {
            Debug.Log("Energy Enough, Energy -" + consuption);
            return true;
        }
        else //show message "you too tired"
        {
            string message = "Energi Tidak Cukup, Butuh Setidaknya " + consuption + " energi";
            noticePanelText.Value = message;
            NoticeTrigger.Raise();
            Debug.Log(message);
            Debug.Log("Activity Ended");
            OnActivityEnd.Invoke();
            return false;
        }
    }

    public bool CheckMoney()
    {
        if (playerData.coins.Value >= currentActivity.cost)
        {
            Debug.Log("Money Enough : -" + currentActivity.cost);
            return true;
        }
        else //show message "you dont have enough money"
        {
            string message = "Koin Tidak Cukup, Butuh Setidaknya " + currentActivity.cost + " Koin";
            noticePanelText.Value = message;
            NoticeTrigger.Raise();
            Debug.Log(message);
            Debug.Log("Activity Ended");
            OnActivityEnd.Invoke();
            return false;
        }
    }

    public void IncreaseStress()
    {
        float old = playerData.stressLevel.Value;
        playerData.stressLevel.Value += (currentActivity.increasedStressModifier * currentActivity.duration.ToHours());

        if (playerData.stressLevel.Value > 100)
            playerData.stressLevel.Value = 100;

        noticePanelText.Value += "<color=red>Stress +" + Mathf.Round(playerData.stressLevel.Value - old) + "/n";
        Debug.Log("Stress +" + (playerData.stressLevel.Value - old) + ", Stress = " + playerData.stressLevel.Value);
    }

    public void DecreaseStress()
    {
        float old = playerData.stressLevel.Value;
        playerData.stressLevel.Value -= (currentActivity.decreasedStressModifier * currentActivity.duration.ToHours());

        if (playerData.stressLevel.Value < 0)
            playerData.stressLevel.Value = 0;

        noticePanelText.Value += "<color=green>Stress -" + Mathf.Abs(Mathf.Round(playerData.stressLevel.Value - old)) + "/n";
        Debug.Log("Stress " + (playerData.stressLevel.Value - old) + ", Stress = " + playerData.stressLevel.Value);
    }

    public void ChangeOtherStat()
    {
        float changedAmount = currentActivity.changedStatModifier;
        float stressPenalty = 0;

        if (currentActivity.isEffectedByStress)
        {
            stressPenalty = playerData.stressLevel.Value / 100 * changedAmount;
            changedAmount -= stressPenalty;
        }

        if (currentActivity.interest != null)
            if (playerData.interest.Contains(currentActivity.interest))
                changedAmount += 5;

        if (currentActivity.ability != null && playerData.ability != null)
            if (playerData.ability.Equals(currentActivity.ability))
                changedAmount += currentActivity.ability.Value;

        float old = currentActivity.changedStat.Value;
        float duration = currentActivity.duration.ToHours();

        currentActivity.changedStat.Value += changedAmount * duration;

        //limit range so the stat can't go above 100 or below 0
        currentActivity.changedStat.Value = Mathf.Clamp(currentActivity.changedStat.Value, 0, 100);

        changedAmount = currentActivity.changedStat.Value - old;

        noticePanelText.Value += 
            "<color=" + Mathf.Sign(changedAmount).ToString().Replace("-1","red").Replace("1","green") + ">" + 
            currentActivity.changedStat.name + " " + 
            Mathf.Sign(changedAmount).ToString().Replace("-1","-").Replace('1','+') + 
            Mathf.Abs(Mathf.Round(currentActivity.changedStat.Value - old)) + "/n";

        if (currentActivity.isEffectedByStress)
        {
            noticePanelText.Value += "<color=red>" + "(Stress Penalty -" + Mathf.Round(stressPenalty * duration) + ") /n";
        }

        if (currentActivity.interest != null && playerData.interest.Contains(currentActivity.interest))
        {
            noticePanelText.Value += "<color=red>" + "(Interest Bonus +" + Mathf.Round(5 * duration) +") /n";
        }

        if (currentActivity.ability != null && playerData.ability != null)
            if (playerData.ability.Equals(currentActivity.ability))
                noticePanelText.Value += "<color=red>" + "(Ability Bonus +" + currentActivity.ability.Value * duration + ") /n";

    }

    public static bool LimitPerDayReached(Activity activity, PlayerData playerData)
    {
        if (playerData.ActivityLimitCount.Any(l => l.name == activity.name))
        {
            if (playerData.ActivityLimitCount.First(l => l.name == activity.name).count > activity.limitPerDay)
                return true;
            return false;
        }

        return false;
    }

    public void SkipTime()
    {
        timePasser.Value = currentActivity.duration;
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
