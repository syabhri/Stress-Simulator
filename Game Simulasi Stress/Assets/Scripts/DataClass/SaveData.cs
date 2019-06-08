using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    //player data
    public string character_name;
    public GameObject avatar;
    public float[] playerPosition;

    public FloatVariable[] stats;

    public Ability[] abilities;

    public Subject[] subjects;

    public SaveData(PlayerData playerData, GameData gameData)
    {
        //player data
        character_name = playerData.character_name.Value;

        avatar = playerData.avatar;

        playerPosition = new float[2];
        playerPosition[0] = playerData.playerPosition.position.x;
        playerPosition[0] = playerData.playerPosition.position.y;

        
    }

    public void LoadData()
    {

    }
}
