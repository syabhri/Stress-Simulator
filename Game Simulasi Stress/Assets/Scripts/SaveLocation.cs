using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLocation : MonoBehaviour
{
    public Vector2Variable target;
    public bool realtimeSaving;

    private void Awake()
    {
        GetLocation();
    }

    // Update is called once per frame
    void Update()
    {
        if (realtimeSaving)
        {
            GetLocation();
        }
    }

    public void GetLocation()
    {
        target.position.x = transform.position.x;
        target.position.y = transform.position.y;
    }
}
