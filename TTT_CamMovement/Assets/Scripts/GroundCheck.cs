using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {

    private PlayerMovement pm;

	// Use this for initialization
	void Start () {
        pm = gameObject.GetComponentInParent<PlayerMovement>();
	}

	void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Wall"))
        {
			//pm.isGrounded() = false;
        }
        else
        {
			//pm.isGrounded() = true;
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
			//pm.isGrounded() = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
		//pm.isGrounded() = false;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Wall"))
        {
			//pm.isGrounded() = false;
        }
        else
        {
			//pm.isGrounded() = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
		//pm.isGrounded() = false;
    }
}
