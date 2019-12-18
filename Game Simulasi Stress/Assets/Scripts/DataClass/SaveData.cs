using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    //player data
    public string character_name;
    public string avatar;
    public string[] player_position;

    public float stress_level;
    public float energy;
    public float coin;

    public FloatPairContainer ability;

    public List<FloatPairContainer> interest;

    public List<FloatVariable> knowleges;

    public SaveData(PlayerData playerData)
    {
        
    }
}
