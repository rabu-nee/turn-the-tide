using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            this.GetComponent<Rigidbody2D>().isKinematic = true;
            this.GetComponent<Rigidbody2D>().freezeRotation = true;
            this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            this.transform.localRotation = Quaternion.Euler(new Vector3(0,0,90));
        }
    }
}
