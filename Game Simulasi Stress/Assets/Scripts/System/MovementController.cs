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
    public bool IsBlendTree;
    public bool autoFlip;

    private void OnEnable()
    {
        Debug.Log("Player Movement Enabled");
    }

    // Update is called once per frame
    void Update()
    {
        //control movement animation
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            if (!IsBlendTree)
                MovingAnimator.SetBool("walk", true);
        }
        else
        {
            if (!IsBlendTree)
                MovingAnimator.SetBool("walk", false);
        }

        //control movement animation (blendtree)
        if (IsBlendTree)
        {
            MovingAnimator.SetFloat("X", Input.GetAxis("Horizontal"));
            MovingAnimator.SetFloat("Y", Input.GetAxis("Vertical"));
        }

        //control character flip
        if (Input.GetAxis("Horizontal") > 0 && autoFlip)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if(Input.GetAxis("Horizontal") < 0 && autoFlip)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // dumb char idle
        if (Input.GetAxis("Horizontal") > 0)
        {
            MovingAnimator.SetBool("right", true);
            MovingAnimator.SetBool("left", false);
            MovingAnimator.SetBool("back", false);
            MovingAnimator.SetBool("front", false);
        }
        else if (Input.GetAxis("Vertical") > 0)
        {
            MovingAnimator.SetBool("right", false);
            MovingAnimator.SetBool("left", false);
            MovingAnimator.SetBool("back", true);
            MovingAnimator.SetBool("front", false);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            MovingAnimator.SetBool("right", false);
            MovingAnimator.SetBool("left", true);
            MovingAnimator.SetBool("back", false);
            MovingAnimator.SetBool("front", false);
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            MovingAnimator.SetBool("right", false);
            MovingAnimator.SetBool("left", false);
            MovingAnimator.SetBool("back", false);
            MovingAnimator.SetBool("front", true);
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
        //MovingAnimator.SetBool("walk", false);
        MovingAnimator.SetFloat("X", 0);
        MovingAnimator.SetFloat("Y", 0);
        Debug.Log("Player Movemnt Disabled");
    }
}
