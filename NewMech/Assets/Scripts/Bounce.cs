using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    public float bounceVel;

    private Rigidbody2D rb2d;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb2d = collision.gameObject.GetComponent<Rigidbody2D>();
        if(rb2d.gravityScale > 0)
        {
            rb2d.velocity = new Vector2(0, bounceVel);
        }
        else if(rb2d.gravityScale < 0)
        {
            rb2d.velocity = new Vector2(0, -bounceVel);
        }
    }
}
