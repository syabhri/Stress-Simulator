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
        if (target.Value == null)
        {
            target.Value = new Vector2();
        }
        GetLocation();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (realtimeSaving)
        {
            GetLocation();
        }
    }

    public void GetLocation()
    {
        target.SetPosition(transform.position.x, transform.position.y);
    }
}
