using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Custom Data Container/PlayerData")]
public class PlayerData : ScriptableObject
{
    [System.Serializable]
    public class ActivityLimit
    {
        public string name;
        public int count;

        public ActivityLimit(string name, int count)
        {
            this.name = name;
            this.count = count;
        }
    }

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
    public FloatContainer ability;

    [Space]
    public List<BoolContainer> interest;

    [Space]
    public List<FloatContainer> knowleges;

    [Space]
    public List<ActivityLimit> ActivityLimitCount;

    public GameObject Avatar
    {
        get { return avatar; }
        set { avatar = value; }
    }

    public void SetAbility(FloatContainer ability)
    {
        this.ability = ability;
    }

    public void ResetAbility()
    {
        ability = null;
    }

    public void SetInterest(BoolContainer interest)
    {
        this.interest.Add(interest);
    }

    public void RemoveInterest(BoolContainer interest)
    {
        this.interest.Remove(interest);
    }

    public void ResetInterest()
    {
        interest.Clear();
    }

    public void ResetActivityLimit()
    {
        ActivityLimitCount.Clear();
    }
}
