using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Ability"), System.Serializable]
public class Ability : ScriptableObject
{
    public new string name;
    public string effectedStats;
    public float value;
}
