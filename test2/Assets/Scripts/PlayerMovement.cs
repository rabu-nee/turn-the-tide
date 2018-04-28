using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Vector2 position;

    bool selected;
    public float speed;
    public float maxSpeed;
    public float jumpSpeed;
    public bool grounded;

    private Animator anim;
    private Rigidbody2D rb2d;
    private SpriteRenderer sr;


    Selected sel;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        sel = gameObject.GetComponent<Selected>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        Jump();
        Fall();
        Debug.Log(rb2d.velocity.y);
    }

    private void Move()
    {
        selected = sel.selected;
        if (selected)
        {
            if (Input.GetAxis("Horizontal") > 0f) //move right
            {
                if (grounded)
                {
                    anim.SetBool("IsWalking", true);
                }
                sr.flipX = false;
                rb2d.AddForce(Vector2.right * speed, ForceMode2D.Force);
            }
            else if (Input.GetAxis("Horizontal") < 0f) //move left
            {
                if (grounded)
                {
                    anim.SetBool("IsWalking", true);
                }
                sr.flipX = true;
                rb2d.AddForce(-Vector2.right * speed, ForceMode2D.Force);
            }
            else
            {
                anim.SetBool("IsWalking", false);
            }

            //speed limit
            if (rb2d.velocity.x > maxSpeed)
            {
                rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
            }
            if (rb2d.velocity.x < -maxSpeed)
            {
                rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
            }
        }
    }

    private void Jump()
    {
        selected = sel.selected;
        if (selected)
        {
            if (Input.GetKey(KeyCode.Space) && grounded) //jump
            {
                anim.SetBool("Jumping", true);
                anim.SetBool("Grounded", false);
                anim.SetBool("IsWalking", false);
                if (rb2d.gravityScale > 0)
                {
                    rb2d.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Force);
                }
                else if (rb2d.gravityScale < 0)
                {
                    rb2d.AddForce(-Vector2.up * jumpSpeed, ForceMode2D.Force);
                }
            }
        }
    }

    private void Fall()
    {
        if (rb2d.velocity.y < 0f)
        {
            anim.SetBool("Jumping", false);
            anim.SetBool("Falling", true);
        }
        if (grounded)
        {
            anim.SetBool("Grounded", true);
            anim.SetBool("Falling", false);
        }
        else if (!grounded)
        {
            anim.SetBool("Grounded", false);
        }
    }
}
