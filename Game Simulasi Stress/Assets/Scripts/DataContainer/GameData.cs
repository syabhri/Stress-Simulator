using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom Data Container/GameData")]
public class GameData : ScriptableObject
{
    public IntVariable AnimationVariant;
    public List<GameObject> Avatars;
    public List<FloatPairContainer> Abilities;
    public List<FloatPairContainer> Insterest;
}
