using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyString : MonoBehaviour {

	public float velocityMultiplier = 1f;
	public float shiftAmount = 0.2f;
	public bool shiftDirection = false;

	private int shiftPosition = -1;
	private Vector3 standardPosition;
	public GameObject[] playerObjs = new GameObject[2];
	private GameObject[] actualPlayers = new GameObject[2];
	private int playerIndex = 0;

	//BUILT-IN FUNCTIONS===================================================================================================================
	void Start () {
		standardPosition = transform.localPosition;
		actualPlayers [0] = GameObject.FindGameObjectWithTag ("Player1");
		actualPlayers [1] = GameObject.FindGameObjectWithTag ("Player2");
	}

	void OnTriggerEnter2D(Collider2D other) {
			Debug.Log (other.gameObject.name);
			if (playerIndex == 0) {
				playerObjs [playerIndex] = other.gameObject;
				playerIndex++;
			} else {
				if (playerIndex == 1) {
					playerObjs [playerIndex] = other.gameObject;
					playerIndex++;
				}
				if (playerIndex == 2) {
					addVelocities (playerObjs [0], playerObjs [1]);
					addShift (2 * getPlayerGravity(other.gameObject));
					playerObjs [0] = playerObjs [1];
					playerObjs [1] = null;
					playerIndex--;
				}
			}		
	}

	void OnTriggerExit2D(Collider2D other) {
		if (playerObjs [0] == other.gameObject) {
			playerObjs [0] = playerObjs [1];
			playerObjs [1] = null;
		}
		if (playerObjs [1] == other.gameObject) {
			playerObjs [1] = null;
			playerIndex--;
		}
	}

	//CUSTOM FUNCTIONS===================================================================================================================
	private void addVelocities(GameObject addObj, GameObject velObj) {
		if ((addObj != null) && (velObj != null)) {
			Rigidbody2D rb1 = addObj.GetComponent<Rigidbody2D> ();
			Rigidbody2D rb2 = velObj.GetComponent<Rigidbody2D> ();
			Vector2 newVel = new Vector2 (0, rb2.velocity.y) * velocityMultiplier;
			Debug.Log (newVel.y);
			rb1.velocity = newVel;
			rb2.velocity = Vector2.zero;
		}
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
			if ((actualPlayers [i] == playerObjs[0]) || (actualPlayers[i] == playerObjs[1])) {
				newPos = actualPlayers [i].transform.localPosition;
				newPos.y += ((shiftAmount*3) * shiftPosition);
				actualPlayers [i].transform.localPosition = newPos;
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
