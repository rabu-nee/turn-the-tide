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
        pm.grounded = true;
	}

    void OnTriggerStay2D(Collider2D col)
    {
        pm.grounded = true;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        pm.grounded = false;
    }
}
