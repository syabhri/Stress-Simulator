using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DataContainer/GameData")]
public class GameData : ScriptableObject
{
    public Vector2Container DefaultSpawnPoint;
    public List<GameObject> Avatars;
    public List<FloatContainer> Abilities;
    public List<BoolContainer> Insterest;
    
}
