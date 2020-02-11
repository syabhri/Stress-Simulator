using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SaveLocation : MonoBehaviour
{
    public Vector2Container target;
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
        target.Value = new Vector2(transform.position.x, transform.position.y);
    }
}
