using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivityManager : MonoBehaviour
{
    #region Activity Variables and Object References
    public float EnergyPerHour = 1;
    public Activity[] ActivityList;

    public Animator transition;

    public TimeManager timeManager;
    public DialogManager dialogManager;
    private GameManager gameManager;
    #endregion

    #region Unity Calback Function
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion

    #region Activity Excecution
    public void DoActivity(Activity activity)
    {
        if(activity.duration != 0)
        {
            //open panel to insert duration
            //change duration to the imputed amount
        }

        if (gameManager.playerStats.baseStats["energy_level"] < (activity.duration * EnergyPerHour))
        {
            //show "you are too tired massage"
            return;
        }

        if (activity.price != 0)
        {
            if(gameManager.playerStats.baseStats["coin"] < activity.price)
            {
                //show "you dont have enough money"
                return;
            }
            else
            {
                //decrease the player money according to required amount
                gameManager.playerStats.baseStats["coin"] -= activity.price;
            }
        }

        if (activity.animator != null)
        {
            activity.animator.SetTrigger(activity.name);
        }

        foreach (var type in activity.activityType)
        {
            if (type == "reduce_stress")
            {

            }
            else if (type == "incrase_stress")
            {

            }
            else if (type == "effected")
            {

            }
        }

        foreach (var ability in gameManager.playerStats.ability)
        {
            if (ability == activity.abilityEffector.abilityName)
            {
                gameManager.playerStats.baseStats[activity.abilityEffector.effectedStats] += activity.abilityEffector.value;
            }
        }

        if (activity.dialogue != null)
        {
            dialogManager.StartDialogue(activity.dialogue);
        }
    }


    #endregion

    #region Activity Scheduling
    #endregion
}
