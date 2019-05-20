using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stats
{
    //character stat value
    public string character_name;

    public Dictionary<string, float> baseStats;

    /*
    [Range(0, 100)]
    public float stress_level;
    [Range(0, 100)]
    public float energy_level;
    public float coin;
    */
    public Subject[] Pelajaran;

    public string[] ability;

    
    public Stats()
    {
        //initialize base stats
        baseStats = new Dictionary<string, float>()
        {
            {"stress_level", 0},
            {"energy_level", 0},
            {"coin", 0}
        };
    }

    //reset character stat
    public void reset()
    {
        /*
        character_name = null;
        stress_level = 0;
        energy_level = 0;
        coin = 0;
        */
        character_name = null;
        baseStats["stress_level"] = 0;
        baseStats["energy_level"] = 0;
        baseStats["coin"] = 0;

    }
}
