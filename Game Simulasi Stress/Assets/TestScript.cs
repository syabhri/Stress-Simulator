using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
               
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openpanel()
    {
        if (anim.GetBool("IsOpen"))
        {
            anim.SetBool("IsOpen", false);
        }
        else
        {
            anim.SetBool("IsOpen", true);
        }
    }
}
