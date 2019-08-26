using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAssigner : MonoBehaviour
{
    [System.Serializable]
    public class GameObjectPair
    {
        [Tooltip("Wheteher or not the object is part of a collection")]
        public bool isSingle = true;
        [Tooltip("the referenced object")]
        public GameObject reference;
        [Tooltip("the reference container to be access by other class/system")]
        public ThingRuntimeSet container;
    }

    [Tooltip("Reference asiggner for disabled object")]
    public GameObjectPair[] referenceList;

    private void Awake()
    {
        foreach (GameObjectPair pair in referenceList)
        {
            if (pair.isSingle)
            {
                pair.container.Replace(pair.reference);
                pair.container.isSingle = true;
            }
            else
            {
                pair.container.Add(pair.reference);
            }
        }
    }

    private void OnDestroy()
    {
        foreach (GameObjectPair pair in referenceList)
        {
            if (pair.isSingle)
            {
                pair.container.Item = null;
            }
            else
            {
                pair.container.Remove(pair.reference);
            }
        }
    }
}
