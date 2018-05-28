using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour {

    public float hitAngle = 90f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            this.GetComponent<Rigidbody2D>().isKinematic = true;

            this.transform.localRotation = Quaternion.Euler(new Vector3(0,0,hitAngle));
            this.GetComponent<Rigidbody2D>().freezeRotation = true;
        }
    }
}
