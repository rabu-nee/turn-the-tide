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
            pm.grounded = false;
        }
        else
        {
            pm.grounded = true;
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            pm.grounded = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
            pm.grounded = false;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Wall"))
        {
            pm.grounded = false;
        }
        else
        {
            pm.grounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        pm.grounded = false;
    }
}
