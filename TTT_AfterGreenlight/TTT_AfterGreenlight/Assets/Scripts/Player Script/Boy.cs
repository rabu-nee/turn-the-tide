﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boy : Player {

    public float distance = 1f;
    public float wallJumpSpeed = 2f;

    // Use this for initialization
    new void  Start () {
        base.Start();
	}
	
    new void Update()
    {
        base.Update();
        if (selected)
        {
            WallJump();
        }
    }


    void WallJump()
    {
        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance);
        if (Input.GetButtonDown("Jump") && !grounded && hit.collider != null && hit.collider.CompareTag("Wall"))
        {
            anim.SetBool("IsWallSliding", false);
            anim.SetBool("Jumping", true);
            outsideForce = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(wallJumpSpeed * hit.normal.x, wallJumpSpeed);
            canMove = false;

            StartCoroutine("TurnIt");
        }

        if (grounded)
        {
            anim.SetBool("IsWallSliding", false);
            //anim.SetBool("Jumping", false);
        }
    }

    IEnumerator TurnIt()
    {
        yield return new WaitForFixedUpdate();
        transform.localScale = transform.localScale.x > 0 ? new Vector2(-scaleX, scaleY) : new Vector2(scaleX, scaleY);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * distance);
    }

    private new void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.CompareTag("Wall"))
        {
            anim.SetBool("IsWallSliding", true);
            anim.SetBool("Jumping", false);
        }
    }
}
