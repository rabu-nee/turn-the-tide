using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAlongPlatforms : MonoBehaviour {

	private Vector3 lastPosition;
	private Vector3	curPosition;
	private Vector3 difPosition;

	private GameObject desObj;

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.CompareTag ("MovingPlatform")) {
			lastPosition = other.gameObject.transform.position;
		}
	}

	void OnCollisionStay2D(Collision2D other) {
		if (other.gameObject.CompareTag ("MovingPlatform")) {
			Debug.Log ("testThisShit");
			curPosition = other.gameObject.transform.position;
			difPosition = curPosition - lastPosition;
			lastPosition = curPosition;

			Vector3 playerPosition = transform.position;
			playerPosition += difPosition;
			transform.position = difPosition;
		}
	}

	/*
	void OnCollisionExit2D(Collision2D other) {
		if (other.gameObject.CompareTag ("MovingPlatform")) {
			transform.parent = null;
		}
	}
	*/
}
