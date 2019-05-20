using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObject/PlayerData")]
public class PlayerData : ScriptableObject
{
    public string character_name;
    public Sprite avatar;

    public Dictionary<string, float> baseStats = 
        new Dictionary<string, float>()
        {
            {"stress_level", 0},
            {"energy_level", 0},
            {"coin", 0}
        };

    public Subject[] Pelajaran;
}
