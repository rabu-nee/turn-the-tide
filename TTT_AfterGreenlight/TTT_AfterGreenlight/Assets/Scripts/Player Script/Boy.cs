using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boy : Player {

    [Header("Wall Jump", order = 2)]
    public float wallJumpSpeed = 2f;
    public string WallslideSound;

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
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + raycastYOffset), Vector2.right * transform.localScale.x, distance);
        if (Input.GetButtonDown("Jump") && !grounded && hit.collider != null && hit.collider.CompareTag("Wall"))
        {
            anim.SetBool("IsWallSliding", false);
            anim.SetBool("Jumping", true);
            outsideForce = true;
            SoundManager.instance.PlaySound(ThrowOrWallJumpSound);
            rb.velocity = new Vector2(wallJumpSpeed * hit.normal.x, wallJumpSpeed);
            canMove = false;

            StartCoroutine("TurnIt");
        }

        if (grounded)
        {
            anim.SetBool("IsWallSliding", false);
            SoundManager.instance.StopSound(WallslideSound);
            //anim.SetBool("Jumping", false);
        }
    }

    IEnumerator TurnIt()
    {
        yield return new WaitForFixedUpdate();
        transform.localScale = transform.localScale.x > 0 ? new Vector2(-scaleX, scaleY) : new Vector2(scaleX, scaleY);
    }

    private new void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.CompareTag("Wall"))
        {
            anim.SetBool("IsWallSliding", true);
            anim.SetBool("Jumping", false);
            SoundManager.instance.PlaySound(WallslideSound);
        }
    }
}
