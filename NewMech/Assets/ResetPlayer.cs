using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayer : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			other.gameObject.GetComponent<PlayerMovement> ().setToStartPosition ();
			other.gameObject.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
		}
	}
}
