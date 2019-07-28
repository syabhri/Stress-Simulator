using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Variable/Custom/PlayerData")]
public class PlayerData : ScriptableObject
{
    public StringVariable character_name;
    public GameObject avatar;
    public Vector2Variable playerPosition;

    [Space]

    public List<FloatVariable> stats;

    [Space]

    public FloatPairContainer ability;

    [Space]

    public List<FloatPairContainer> interest;

    [Space]

    public List<FloatVariable> knowlage;

    public void SetAvatar(GameObject avatar)
    {
        this.avatar = avatar;
    }

    public void SetAbility(FloatPairContainer ability)
    {
        this.ability = ability;
    }

    public void ResetAbility()
    {
        ability = null;
    }

    public void SetInterest(FloatPairContainer interest)
    {
        this.interest.Add(interest);
    }

    public void RemoveInterest(FloatPairContainer interest)
    {
        this.interest.Remove(interest);
    }

    public void ResetInterest()
    {
        interest.Clear();
    }
}
