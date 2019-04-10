using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float speed;
    private Vector2 moveVelocity;

    public Animator anim;
    public Rigidbody2D rb;

    // Update is called once per frame
    void Update()
    {
        //control movement animation
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            anim.SetBool("walk", true);
        }
        else
        {
            anim.SetBool("walk", false);
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
        moveVelocity = moveInput.normalized * speed;

        //debug
        //Debug.Log(Input.GetAxis("Horizontal") + " " + Input.GetAxis("Vertical"));
    }

    private void FixedUpdate()
    {
        //move the character
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
        //Debug.Log(Mathf.Sin(5));
    }
}
