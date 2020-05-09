using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresistenceReference : MonoBehaviour
{
    public GameObjectContainer container;

    private void Awake()
    {
        if (container != null)
        {
            container.Value = this.gameObject;
        }
    }
}
