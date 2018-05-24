﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool selected;
    public bool grounded;

    public float speed, jumpVelocity;
    private float maxSpeed;

    //public Animator anim;
    public Rigidbody2D rb2d;
    public float x, y; //localscale

    public void Start()
    {
        //anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        x = this.transform.localScale.x;
        y = this.transform.localScale.y;
    }

    public void Move()
    {

        if (selected)
        {
            if (Input.GetAxis("Horizontal") > 0f) //move right
            {
                if (grounded)
                {
                    //anim.SetBool("IsWalking", true);
                }
                this.transform.localScale = new Vector2(x,y);
                rb2d.AddForce(Vector2.right * speed, ForceMode2D.Force);
            }
            else if (Input.GetAxis("Horizontal") < 0f) //move left
            {
                if (grounded)
                {
                    //anim.SetBool("IsWalking", true);
                }
                this.transform.localScale = new Vector2(-x,y);
                rb2d.AddForce(-Vector2.right * speed, ForceMode2D.Force);
            }
            else
            {
                //anim.SetBool("IsWalking", false);
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

    public void Jump()
    {
        if (selected)
        {
            if (Input.GetKey(KeyCode.Space) && grounded) //jump
            {
                if (rb2d.gravityScale > 0)
                {
                    rb2d.velocity = new Vector2(0, jumpVelocity);

                }
                else if (rb2d.gravityScale < 0)
                {
                    rb2d.velocity = new Vector2(0, -jumpVelocity);
                }
                if (rb2d.velocity.y != 0)
                {
                    //anim.SetBool("Jumping", true);
                    //anim.SetBool("Grounded", false);
                    //anim.SetBool("IsWalking", false);
                }
            }
        }
    }
}