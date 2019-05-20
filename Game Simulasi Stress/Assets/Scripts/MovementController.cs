using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public FloatVariable speed;
    private Vector2 moveVelocity;

    [Space]
    public Rigidbody2D rigitbody2d;
    [Space]
    public Animator MovingAnimator;

    

    // Update is called once per frame
    void Update()
    {
        //control movement animation
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            MovingAnimator.SetBool("walk", true);
        }
        else
        {
            MovingAnimator.SetBool("walk", false);
        }

        //control character flip
        if (Input.GetAxis("Horizontal") > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if(Input.GetAxis("Horizontal") < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        //get input and set velocity base on speed and axis
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speed.value;

        //debug
        //Debug.Log(Input.GetAxis("Horizontal") + " " + Input.GetAxis("Vertical"));
    }

    private void FixedUpdate()
    {
        //move the character
        rigitbody2d.MovePosition(rigitbody2d.position + moveVelocity * Time.fixedDeltaTime);
        //Debug.Log(Mathf.Sin(5));
    }

    public void OnDisable()
    {
        MovingAnimator.SetBool("walk", false);
    }
}
