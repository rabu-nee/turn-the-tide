using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour {

    public float hitAngle;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            Rigidbody2D rb = this.GetComponent<Rigidbody2D>();
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
            BoxCollider2D col = this.GetComponent<BoxCollider2D>();
            col.enabled = false;

            this.transform.localRotation = Quaternion.Euler(new Vector3(0,0, hitAngle));
            col.enabled = true;
            rb.freezeRotation = true;
        }
    }
}
