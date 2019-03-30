using UnityEngine;

public class player_movement : MonoBehaviour
{
    public Rigidbody2D player;
    public Vector2 speed;
    bool toogle = false;
    public Animator anim;

    private void Start()
    {
        anim = anim.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("space"))
        {
            toogle = true;
            player.AddForce(speed);
            anim.SetBool("Jump", true);
        }
        else
        {
            anim.SetBool("Jump", false);
            toogle = false;
        }
    }
}
