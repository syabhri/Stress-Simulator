using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveEnum : MonoBehaviour
{

    public TimeManager.DayName dayName;

    public void Save()
    {
        SaveManager.Save<EnumData>(new EnumData(dayName), "Edata");
    }

    public void Load()
    {
        Debug.Log(SaveManager.Load<EnumData>("Edata").dayName);
    }
}
