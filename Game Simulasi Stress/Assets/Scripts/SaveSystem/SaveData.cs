using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class SaveData
{
    [System.Serializable]
    public struct Knowlage {
        public string name;
        public float value;

        public Knowlage(string name, float value)
        {
            this.name = name;
            this.value = value;
        }
    }

    [System.Serializable]
    public struct PlayerPosition
    {
        public float x;
        public float y;

        public PlayerPosition(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    }

    //game data
    public TimeFormat play_time;

    //player data
    public string character_name;
    public string avatar;
    public PlayerPosition player_position;

    public float stress_level;
    public float energy;
    public float coins;

    public string ability;

    public List<string> interest;

    public List<Knowlage> knowleges;

    public List<PlayerData.ActivityLimit> activityLimits;

    //constructor
    public SaveData(PlayerData playerData)
    {
        character_name = playerData.characterName.Value;
        avatar = playerData.avatar.name;

        player_position = new PlayerPosition(playerData.playerPosition.Value.x,
            playerData.playerPosition.Value.y);

        play_time = new TimeFormat(playerData.playTime.Value.days,
            playerData.playTime.Value.hours,
            playerData.playTime.Value.minutes,
            playerData.playTime.Value.dayName);

        stress_level = playerData.stressLevel.Value;
        energy = playerData.energy.Value;
        coins = playerData.coins.Value;

        ability = playerData.ability?.name;

        interest = playerData.interest.Select(i => i.name).ToList();

        knowleges = new List<Knowlage>();

        foreach (FloatContainer knowlage in playerData.knowleges)
        {
            knowleges.Add(new Knowlage(knowlage.name, knowlage.Value));
        }

        activityLimits = playerData.ActivityLimitCount;
    }
}
