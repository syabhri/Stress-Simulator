using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Variable/Custom/GameData")]
public class GameData : ScriptableObject
{
    public List<GameObject> Avatars;
    public List<FloatPairContainer> Abilities;
    public List<FloatPairContainer> Insterest;
}
