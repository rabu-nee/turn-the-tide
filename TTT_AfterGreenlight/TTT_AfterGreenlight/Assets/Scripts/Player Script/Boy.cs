using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boy : Player {

    public bool canWallJump;
    public float wallJumpVelocity;

	// Use this for initialization
	void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Move();
        Jump();
        WallJump();
	}

    private void WallJump()
    {
        if (selected)
        {
            if (Input.GetKey(KeyCode.Space) && canWallJump)
            {
                if (this.transform.localScale.x > 0) //facing right, jump left
                {
                    rb2d.velocity = new Vector2(-1,1) * wallJumpVelocity;
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
