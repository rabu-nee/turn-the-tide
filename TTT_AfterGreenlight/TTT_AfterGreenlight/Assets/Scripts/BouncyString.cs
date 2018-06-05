using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyString : MonoBehaviour {

	private GameObject lastEnteredPlayer;

	//BUILT-IN FUNCTIONS===================================================================================================================
	void Start () {
		lastEnteredPlayer = null;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (isPlayerTag(other.gameObject.tag)) {
			Debug.Log (other.gameObject.name);
			if (lastEnteredPlayer == null) {
				lastEnteredPlayer = other.gameObject;
			} else {
				addVelocities (lastEnteredPlayer, other.gameObject);
				lastEnteredPlayer = other.gameObject;
			}
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject == lastEnteredPlayer) {
			lastEnteredPlayer = null;
		}
	}

	//CUSTOM FUNCTIONS===================================================================================================================
	private void addVelocities(GameObject addObj, GameObject velObj) {
		if ((addObj != null) && (velObj != null)) {
			Vector2 vel = velObj.GetComponent<Rigidbody2D> ().velocity;
			Vector2 mult = addObj.transform.localScale;
			//mult = new Vector2 (0, getNumWeight(mult.y));
			//vel *= mult;
			vel.x = 0f;
			//vel *= 10f;

			addObj.GetComponent<Rigidbody2D> ().AddForce (vel, ForceMode2D.Impulse);
		}
	}

	private bool isPlayerTag(string tag) {
		if ((tag == "Player1") || (tag == "Player2")) {
			return true;
		} else {
			return false;
		}
	}

	private int getNumWeight(float num) {
		int ret = Mathf.RoundToInt(Mathf.Clamp (num*1000, -1, 1));
		Debug.Log (ret);
		return ret;
	}
}
