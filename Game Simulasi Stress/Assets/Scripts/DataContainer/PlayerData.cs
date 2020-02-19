using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Custom Data Container/PlayerData")]
public class PlayerData : ScriptableObject
{
    public StringContainer characterName;
    [SerializeField]
    public GameObject avatar;
    public Vector2Container playerPosition;

    [Space]
    public TimeContainer playTime;

    [Space]
    public FloatContainer stressLevel;
    public FloatContainer energy;
    public FloatContainer coins;

    [Space]
    public FloatPairContainer ability;

    [Space]
    public List<FloatPairContainer> interest;

    [Space]
    public List<FloatContainer> knowleges;

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
