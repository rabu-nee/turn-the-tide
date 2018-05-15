using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObject : MonoBehaviour {

	private GameObject[] playercols = new GameObject[2];
	int numberCol = 0;
	public float shiftDistance = 0.3f;

	/*
	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			playercols [numberCol] = other.gameObject;

			int dir = playercols [numberCol].GetComponent<PlayerMovement> ().getGravityWeight ();
			Vector3 newPos = new Vector3 (transform.position.x, transform.position.y + (shiftDistance * dir), transform.position.z);
			transform.position = newPos;

			if (numberCol > 0) {
				Vector3 addVel = new Vector3 (0, playercols [numberCol].GetComponent<Rigidbody2D> ().velocity.y, 0);
				playercols [0].GetComponent<Rigidbody2D> ().AddForce (addVel, ForceMode2D.Force);

				playercols [0] = playercols [numberCol];
				playercols [numberCol] = null;
				numberCol--;
			} else {
				numberCol++;
			}
		}
	}*/

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			if (other.gameObject.name == "PL1") {
				Debug.Log ("test");
				transform.position = new Vector3 (transform.position.x, transform.position.y - shiftDistance, transform.position.z);
				GameObject.Find ("PL2").GetComponent<Rigidbody2D> ().AddForce (new Vector2(0,1500));
			}
		}
	}
}
