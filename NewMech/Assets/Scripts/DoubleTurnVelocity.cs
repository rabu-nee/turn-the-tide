using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleTurnVelocity : MonoBehaviour {

	float standardYoffset;
	public bool aimMode = false;
	public GameObject[] Players;
	public PlayerSelection ps;
	int selectedIndex;
	bool triggerHasReset = false;

	//Trajectory variables
	public int numberOfDots = 5;
	public GameObject dotPrefab;
	private int dir;
	private int TriggerPressed = 0;
	public float appliedForce = 30f;

	void Start () {
		standardYoffset = GetComponent<CameraPositioning> ().yOffset;
	}

	void Update () {
		if (((Input.GetAxis ("LTrigger") > 0) || (Input.GetAxis ("RTrigger") > 0)) && (!Players[ps.currentPlayer()].GetComponent<PlayerMovement>().grounded /*isGrounded()*/)) {
			if (!Players[ps.currentPlayer()].GetComponent<PlayerMovement>().getDisableUntilContact()) {
				if (Input.GetAxis ("LTrigger") > 0) {
					dir =  1;
					TriggerPressed = -1;
				}
				if (Input.GetAxis ("RTrigger") > 0) {
					dir = -1;
					TriggerPressed = 1;
				}
				GetComponent<CameraPositioning> ().yOffset = 0;
				aimMode = true;
				disableInput ();

				triggerHasReset = false;
			}
		}

		if ((Input.GetAxis ("LTrigger") == 0) && (Input.GetAxis ("RTrigger") == 0)) {
			if (!triggerHasReset) {
				GetComponent<CameraPositioning> ().yOffset = standardYoffset;
				aimMode = false;
				enableInput ();
				//addVelocity(performAimMode (), appliedForce);
				GetComponent<CameraPositioning> ().advanceScreen (TriggerPressed);
				GetComponent<CameraPositioning> ().advanceScreen (TriggerPressed);
				TriggerPressed = 0;
				triggerHasReset = true;
			}
		}
	}

	void FixedUpdate() {
		if (aimMode) {
			//performAimMode ();
		}
	}

	//void performAimMode() {

	//}


	void addVelocity(Vector2 addVel, float force) {
		Players [ps.currentPlayer()].GetComponent<Rigidbody2D> ().AddForce (addVel * force);
	}

	void disableInput() {
		GetComponent<CameraPositioning> ().setAllowInput (false);
		for (int i = 0; i < Players.Length; i++) {
			if (Players [i].GetComponent<Selected> ().selected) {
				selectedIndex = i;
			}
			Players [i].GetComponent<Selected> ().selected = false;
			Players [i].GetComponent<Rigidbody2D> ().isKinematic = true;
			Vector2 nv2 = Players [i].GetComponent<Rigidbody2D> ().velocity;
			Players [i].GetComponent<Rigidbody2D> ().velocity = Vector2.Lerp (nv2, Vector3.zero, 23 * Time.deltaTime);
		}
	}

	void enableInput() {
		GetComponent<CameraPositioning> ().setAllowInput (true);
		for (int i = 0; i < Players.Length; i++) {
			Players [i].GetComponent<Selected> ().selected = false;
			Players [i].GetComponent<Rigidbody2D> ().isKinematic = false;
			if (i == selectedIndex) {
				Players [i].GetComponent<PlayerMovement> ().disableUntilContact ();
			}
		}
	}
}
