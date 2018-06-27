using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            this.transform.parent = collision.transform;
            Rigidbody2D rb = this.GetComponent<Rigidbody2D>();

            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
            rb.freezeRotation = true;
        }
    }
}
