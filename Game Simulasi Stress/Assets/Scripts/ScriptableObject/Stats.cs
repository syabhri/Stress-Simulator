using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stats
{
    public string character_name;

    [Range(0, 100)]
    public float stress_level;
    [Range(0, 100)]
    public float energy_level;
    public float coin;

    Subject[] Pelajaran;
}
