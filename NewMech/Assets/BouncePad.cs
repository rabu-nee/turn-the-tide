using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		if ((other.gameObject.name == "PL1") || (other.gameObject.name == "PL2")) {
			Debug.Log ("hit");
			Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D> ();
			Vector2 vel = rb.velocity;
			vel.y = -vel.y * (1.1f);
			rb.velocity = vel;
		}
	}
}
