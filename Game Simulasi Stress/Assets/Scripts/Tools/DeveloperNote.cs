using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeveloperNote : MonoBehaviour
{
#if UNITY_EDITOR
    [Multiline]
    public string developerNote = "";
#endif
}
