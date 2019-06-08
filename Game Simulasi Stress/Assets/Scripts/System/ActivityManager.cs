using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivityManager : MonoBehaviour
{
    #region Activity Variables and Object References
    [Header("Properties")]
    public FloatVariable EnergyPerHour;

    [Header("External Variables")]
    public FloatVariable energy;
    public FloatVariable stress;
    public FloatVariable money;
    public TimeContainer currentTime;
    public PlayerData playerData;

    [Header("References")]
    public ThingRuntimeSet timeSetterPanel;
    public ThingRuntimeSet timeSetterOkButton;

    [Header("Conditions")]
    public BoolVariable isTimeSetterOpen;

    [Header("Events")]
    public GameEvent onPlayerMove;
    public GameEvent onPlayerStop;
    public GameEvent onPhase2;
    public GameEvent onAjustDuration;

    [Header("Data Passer")]
    public TimeContainer timePasser;

    //time setter panel ok button;
    private Button okButton;
    //reference to the current activity
    [SerializeField]
    private Activity activity;
    #endregion

    #region Unity Calback Function
    // Start is called before the first frame update
    void Start()
    {

    }
    #endregion

    #region Activity Excecution
    public void DoActivity(Activity activity)
    {
        Debug.Log("activity started : " + activity.activityName);//testing
        onPlayerStop.Raise();
        this.activity = activity;

        if(activity.isDutrationAjustable)
        {
            //open panel to insert duration and change duration to the inputed amount
            foreach (Thing thing in timeSetterPanel.Items)
            {
                thing.gameObject.SetActive(true);
                Debug.Log("TimeSetter active");
            }
            foreach (Thing thing in timeSetterOkButton.Items)//sementara
            {
                okButton = thing.GetComponent<Button>();
            }
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

        if(activity.isUseEnergy)
            if (!CheckEnergy())
                return;

        if (activity.isCostMoney)
            if (!CheckMoney())
                return;
        
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

        if (activity.interest != null)
        {
            
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
            Debug.Log("Energy Decreased by " + consuption + ", Energy = " + energy.value);
        }
            
        //decrease the money if activity using money
        if (activity.isCostMoney)
        {
            money.value -= activity.cost;
            Debug.Log("Money Decreased by " + activity.cost + ", Money = " + money.value);
        }

        onPlayerMove.Raise();
        Debug.Log("Activity Ended");
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
            Debug.Log("Not Enough Energy, need at least " + consuption + " energy");
            Debug.Log("Activity Ended");
            onPlayerMove.Raise();
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
            Debug.Log("Not Enough Money, need at least " + activity.cost + " coin");
            Debug.Log("Activity Ended");
            onPlayerMove.Raise();
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

        // process the effector if the
        if (activity.interest != null)
        {
            effector = ApplyBonusInterest(effector);
        }

        if (activity.ability != null)
        {
            effector = ApplyBonusAbility(effector);
        }

        //change the target stat according to the configured operation and effector
        switch (activity.operation)
        {
            case "=":
                pair.target.value = effector;
                break;
            case "+":
                pair.target.value += effector;
                break;
            case "-":
                pair.target.value -= effector;
                break;
            case "*":
                pair.target.value *= effector;
                break;
            case "/":
                pair.target.value /= effector;
                break;
            default:
                Debug.LogError("Operator Not Match : " +
                    activity.operation + ", available operator are \"=+-*/\"");
                break;
        }

        //limit range so the stat can't go above 100 or below 0
        if (pair.target.value > 100)
        {
            pair.target.value = 100;
        }
        if (pair.target.value < 0)
        {
            pair.target.value = 0;
        }

        Debug.Log("Stat " + pair.target.name + " Changed from " +
            old + " to " + pair.target.value +
            " using " + activity.operation + " operation with " +
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

    public float ApplyBonusInterest(float effector)
    {
        foreach (FloatVariable interest in playerData.interest)
        {
            if (activity.interest == interest)
            {
                effector += interest.value;
                return effector;
            }
        }
        return effector;
    }

    public float ApplyBonusAbility(float effector)
    {
        if (activity.ability == playerData.ability)
        {
            effector += activity.ability.value;
        }
        return effector;
    }
    #endregion

    #region Activity Scheduling
    public bool CkeckScadule(Activity activity)
    {
        if (currentTime.time.hours >= activity.scadule.hours
            && currentTime.time.minutes >= activity.scadule.minutes
            && currentTime.time.hours <= activity.tolerance.hours
            && currentTime.time.minutes <= activity.tolerance.minutes)
        {
            return true;
        }
        return false;
    }

    public bool CheckLimitPerDay()
    {
        if (activity.currentCount > activity.limitPerDay)
            return true;
        else
            return false;
    }

    public bool CheckValueLimit()
    {
        if (activity.currentValue > activity.valueLimit)
            return true;
        else
            return false;
    }
    #endregion
}
