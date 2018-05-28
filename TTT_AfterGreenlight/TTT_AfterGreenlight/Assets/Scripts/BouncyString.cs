using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyString : MonoBehaviour {

	public float velocityMultiplier = 1f;
	public float shiftAmount = 0.2f;
	public bool shiftDirection = false;

	private int shiftPosition = -1;
	private Vector3 standardPosition;
	private GameObject[] playerObjs = new GameObject[2];
	private int playerIndex = 0;

	//BUILT-IN FUNCTIONS===================================================================================================================
	void Start () {
		standardPosition = transform.localPosition;
	}

	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (isPlayerTag(other.gameObject.tag)) {
			playerObjs [playerIndex] = other.gameObject;
			playerIndex++;

			if (playerIndex == 2) {
				addVelocities (playerObjs [0], playerObjs [1]);
				playerObjs [0] = playerObjs [1];
				playerObjs [1] = null;
				playerIndex--;
			}
		}

		addShift (2 * getPlayerGravity(other.gameObject));
	}

	void OnCollisionExit2D(Collision2D other) {
		if (playerObjs [0] == other.gameObject) {
			playerObjs [0] = playerObjs [1];
			playerObjs [1] = null;
		}
		if (playerObjs [1] == other.gameObject) {
			playerObjs [1] = null;
		}
	}

	//CUSTOM FUNCTIONS===================================================================================================================
	private void addVelocities(GameObject addObj, GameObject velObj) {
		Rigidbody2D rb1 = addObj.GetComponent<Rigidbody2D> ();
		Rigidbody2D rb2 = velObj.GetComponent<Rigidbody2D> ();
		Vector2 newVel = new Vector2(0, rb2.velocity.y) * velocityMultiplier;
		rb1.velocity = newVel;
		rb2.velocity = Vector2.zero;
	}

	private void addShift(int dir) {
		shiftPosition += dir;
		shiftPosition = Mathf.Clamp (shiftPosition, -1, 1);

		//Shift self
		Vector3 newPos = standardPosition;
		newPos.y += (shiftAmount * shiftPosition);
		transform.localPosition = newPos;

		//Shift Players
		for (int i = 0; i < 2; i++) {
			if (playerObjs [i] != null) {
				newPos = playerObjs [i].transform.localPosition;
				newPos.y += (shiftAmount * shiftPosition);
				playerObjs [i].transform.localPosition = newPos;
			}
		}
	}

	private int getPlayerGravity(GameObject player) {
		int ret = 0;
		float gs = player.GetComponent<Rigidbody2D> ().gravityScale;

		if (shiftDirection) {
			if (gs <= 0) {
				ret = -1;
			}
			if (gs >  0) {
				ret =  1;
			}
		}
		if (!shiftDirection) {
			if (gs <= 0) {
				ret =  1;
			}
			if (gs >  0) {
				ret = -1;
			}
		}
		return ret;
	}

	private bool isPlayerTag(string tag) {
		if ((tag == "Player1") || (tag == "Player2")) {
			return true;
		} else {
			return false;
		}
	}
}
