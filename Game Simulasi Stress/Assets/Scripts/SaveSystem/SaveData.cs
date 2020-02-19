using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public struct Knowlage{
        public string name;
        public float value;
    }

    //game data
    public TimeFormat play_time;

    //player data
    public string character_name;
    public string avatar;
    public float[] player_position;

    public float stress_level;
    public float energy;
    public float coins;

    public string[] ability;

    public string[] interest;

    public Knowlage[] knowleges;

    //constructor
    public SaveData(PlayerData playerData)
    {
        character_name = playerData.characterName.Value;
        avatar = playerData.avatar.name;
        player_position = new float[2];

        if (playerData.playerPosition.Value != null)
        {
            player_position[0] = playerData.playerPosition.Value.x;
            player_position[1] = playerData.playerPosition.Value.y;
        }

        play_time = new TimeFormat(playerData.playTime.Value.days,
            playerData.playTime.Value.hours,
            playerData.playTime.Value.minutes,
            playerData.playTime.Value.dayName);

        stress_level = playerData.stressLevel.Value;
        energy = playerData.energy.Value;
        coins = playerData.coins.Value;

        
    }
}
