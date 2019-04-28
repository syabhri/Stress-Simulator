using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivityManager : MonoBehaviour
{
    #region Activity Variables and Object References
    public float EnergyPerHour;
    public Activity[] ActivityList;

    public TimeManager timeManager;
    public DialogManager dialogManager;
    #endregion

    #region Unity Calback Function
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion

    #region Activity Excecution
    public void DoActivity(Activity activity)
    {

    }
    #endregion

    #region Activity Scheduling
    #endregion
}
