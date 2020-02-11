using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    //game data
    public float[] current_time;

    //player data
    public string character_name;
    public string avatar;
    public float[] player_position;

    public float stress_level;
    public float energy;
    public float coins;

    //public FloatPairContainer ability;

    //public FloatPairContainer[] interest;

    //public float[] knowleges;

    //constructor
    public SaveData(PlayerData playerData, TimeFormat currentTime)
    {
        current_time = new float[3];
        current_time[0] = currentTime.minutes;
        current_time[1] = currentTime.hours;
        current_time[2] = currentTime.days;

        character_name = playerData.characterName.Value;

        avatar = playerData.avatar.name;
        
        player_position = new float[2];

        if (playerData.playerPosition.Value != null)
        {
            player_position[0] = playerData.playerPosition.Value.x;
            player_position[1] = playerData.playerPosition.Value.y;
        }

        stress_level = playerData.stressLevel.Value;
        energy = playerData.energy.Value;
        coins = playerData.coins.Value;


    }
}
