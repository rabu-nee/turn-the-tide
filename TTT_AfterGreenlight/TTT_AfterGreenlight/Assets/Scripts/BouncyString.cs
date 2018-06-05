using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyString : MonoBehaviour {

	private GameObject[] Players;
	private bool[] playerOnTrigger;

	//BUILT-IN FUNCTIONS===================================================================================================================
	void Start () {
		Players = new GameObject[2];
		Players [0] = GameObject.FindGameObjectWithTag ("Player1");
		Players [1] = GameObject.FindGameObjectWithTag ("Player2");

		playerOnTrigger = new bool[2];
		playerOnTrigger [0] = false;
		playerOnTrigger [1] = false;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (isPlayerTag(other.gameObject.tag)) {
			int curIndex = getPlayerIndex (other.gameObject, Players);
			playerOnTrigger [curIndex] = true;

			if (arrayAllTrue (playerOnTrigger)) {
				addVelocities (Players [(-curIndex) + 1], Players [curIndex]);
			}
			
		}
	}

	void OnTriggerExit2D(Collider2D other) {

	}

	//CUSTOM FUNCTIONS===================================================================================================================
	private void addVelocities(GameObject addObj, GameObject velObj) {
		if ((addObj != null) && (velObj != null)) {
			Vector2 vel = velObj.GetComponent<Rigidbody2D> ().velocity;
			Vector2 mult = addObj.transform.localScale;
			vel.x = 0f;

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

	private bool arrayAllTrue(bool[] a) {
		bool ret = true;
		for (int i = 0; i < a.Length; i++) {
			if (!a [i]) {
				ret = false;
			}
		}

		return ret;
	}

	private int getPlayerIndex(GameObject n, GameObject[] a) {
		int ret = 0;
		for (int i = 0; i < a.Length; i++) {
			if (n == a [i]) {
				ret = i;
			}
		}

		return ret;
	}

	private int getNumWeight(float num) {
		int ret = Mathf.RoundToInt(Mathf.Clamp (num*1000, -1, 1));
		Debug.Log (ret);
		return ret;
	}
}
