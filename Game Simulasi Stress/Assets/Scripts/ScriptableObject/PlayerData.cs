using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Variable/Custom/PlayerData")]
public class PlayerData : ScriptableObject
{
    public StringVariable character_name;
    public GameObject avatar;
    public Vector2Variable playerPosition;

    public List<FloatVariable> stats;

    public FloatVariable ability;

    public List<FloatVariable> interest;

    public List<FloatVariable> skills;

    public void SetAvatar(GameObject avatar)
    {
        this.avatar = avatar;
    }
}
