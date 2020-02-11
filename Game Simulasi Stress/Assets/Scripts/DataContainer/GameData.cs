using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DataContainer/GameData")]
public class GameData : ScriptableObject
{
    public IntContainer AnimationVariant;
    public List<GameObject> Avatars;
    public List<FloatPairContainer> Abilities;
    public List<FloatPairContainer> Insterest;
}
