using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementController : MonoBehaviour
{
    public float speed;
    private Vector2 moveVelocity;

    private Rigidbody2D rigitbody2d;
    private Animator MovingAnimator;

    private void OnEnable()
    {
        Debug.Log("Player Movement Enabled");
    }

    private void Start()
    {
        rigitbody2d = GetComponent<Rigidbody2D>();
        MovingAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //reset walk status
        MovingAnimator.SetBool("walk", false);

        //send axis information to control movement animation
        MovingAnimator.SetFloat("X", Input.GetAxisRaw("Horizontal"));
        MovingAnimator.SetFloat("Y", Input.GetAxisRaw("Vertical"));

        //sending last axis information to control idle animation
        if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
        {
            MovingAnimator.SetFloat("wasX", Input.GetAxisRaw("Horizontal"));
            MovingAnimator.SetFloat("wasY", 0);
            MovingAnimator.SetBool("walk", true);
        }
        if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
        {
            MovingAnimator.SetFloat("wasX", 0);
            MovingAnimator.SetFloat("wasY", Input.GetAxisRaw("Vertical"));
            MovingAnimator.SetBool("walk", true);
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
        rigitbody2d.MovePosition(rigitbody2d.position + moveVelocity * Time.fixedDeltaTime);
        //Debug.Log(Mathf.Sin(5));
    }

    public void OnDisable()
    {
        MovingAnimator.SetBool("walk", false);
        MovingAnimator.SetFloat("X", 0);
        MovingAnimator.SetFloat("Y", 0);
        Debug.Log("Player Movemnt Disabled");
    }
}
