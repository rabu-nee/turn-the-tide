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


    Selected sel;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        sel = gameObject.GetComponent<Selected>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        selected = sel.selected;
        if (selected)
        {
            if (Input.GetAxis("Horizontal") > 0f) //move right
            {
                anim.SetFloat("State", 0);
                anim.SetBool("IsWalking", true);
                rb2d.AddForce(Vector2.right * speed, ForceMode2D.Force);
            }
            else if (Input.GetAxis("Horizontal") < 0f) //move left
            {
                anim.SetFloat("State", 1);
                anim.SetBool("IsWalking", true);
                rb2d.AddForce(-Vector2.right * speed, ForceMode2D.Force);
            }
            else
            {
                anim.SetBool("IsWalking", false);
            }

            if (Input.GetKey(KeyCode.Space) && grounded) //jump
            {
                if (rb2d.gravityScale > 0)
                {
                    rb2d.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Force);
                }
                else if (rb2d.gravityScale < 0)
                {
                    rb2d.AddForce(-Vector2.up * jumpSpeed, ForceMode2D.Force);
                }
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
}
