using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JumpScript : MonoBehaviour
{

    public Vector2 jumpForce;
    public GameObject player;
    public float jumpTimerMax;
    bool jumping = false;
    float groundDist;
    int jumpCount;
    float jumpTimer;
    void Start()
    {
        // gets distance from center of object to ground
        groundDist = GetComponent<Collider2D>().bounds.extents.y;
        //initialize jump timer
        jumpTimer = jumpTimerMax;
    }
    void Update()
    {
        float gravScale = player.GetComponent<Rigidbody2D>().gravityScale;
        // mouse controls
        if (Input.GetButtonDown("Jump"))
        {
            jump();
            jumping = true;
        }
        //if jump button is held and jumptimer > 0 and gravsacle > 0 and jumping
        if (Input.GetButton("Jump") && jumpTimer > 0 && gravScale > 0 && jumping == true) 
        {
            //lower gravity scale
            player.GetComponent<Rigidbody2D>().gravityScale -= 0.05f;
        }
        if(IsGrounded() == false)
        {
            jumpTimer -= 0.1f;
        }
        //if jumptimer <= 0 set jumptimer max and reset gravity scale
        if (jumpTimer <= 0)
        {

            player.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
        if(IsGrounded())
        {
            jumpTimer = jumpTimerMax;
            player.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }

    public bool IsGrounded()
    {
        // sends raycast down towards ground and returns true if hits false if dosent
        return Physics2D.Raycast(player.transform.position, -Vector2.up, groundDist + 0.1f);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Platform")
        {
            IsGrounded();
        }
    }

    
    void OnMouseDown()
    {
        jump();
    }
     
    public void jump()
    {
        if (IsGrounded())
        {
            player.GetComponent<Rigidbody2D>().AddForce(jumpForce);
        }
    }
}
