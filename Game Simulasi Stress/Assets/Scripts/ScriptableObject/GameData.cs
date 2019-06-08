using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Variable/Custom/GameData")]
public class GameData : ScriptableObject
{
    public TimeContainer timeLimit;

    public Vector2Variable[] SpawnPoints;

    public List<FloatVariable> subjects;
}
