using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAlongPlatform : MonoBehaviour {

	private GameObject followObject = null;
	private Vector3 lastPos = Vector3.zero;

	void FixedUpdate() {
		//Check if follow object exists
		if (followObject != null) {
			Vector3 difPos = followObject.transform.position - lastPos;
			lastPos = followObject.transform.position;

			Vector3 curPos = transform.position;
			curPos += difPos;
			transform.position = curPos;
		}
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.CompareTag ("MovingPlatform")) {
			followObject = other.gameObject;
			lastPos = followObject.transform.position;
		}
	}

	void OnCollisionExit2D(Collision2D other) {
		if (other.gameObject == followObject) {
			followObject = null;
		}
	}
}
