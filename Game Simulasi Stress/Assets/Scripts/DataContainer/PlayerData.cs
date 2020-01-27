using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Variable/Custom/PlayerData")]
public class PlayerData : ScriptableObject
{
    public StringVariable characterName;
    [SerializeField]
    public GameObject avatar;
    public Vector2Variable playerPosition;

    [Space]
    public FloatVariable stressLevel;
    public FloatVariable energy;
    public FloatVariable coins;

    [Space]
    public FloatPairContainer ability;

    [Space]
    public List<FloatPairContainer> interest;

    [Space]
    public List<FloatVariable> knowleges;

    public GameObject Avatar
    {
        get { return avatar; }
        set { avatar = value; }
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
