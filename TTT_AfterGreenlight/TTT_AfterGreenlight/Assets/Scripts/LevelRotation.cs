using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRotation : MonoBehaviour {

	public float standardTurnTime = 2f;

	private float elapsedTurnTime = 0;
	private Vector3 desiredRotation;
	private Vector3 normalRotation;

	//####> BUILT-IN FUNCTIONS <#############################################################################################
	void Start () {
		normalRotation = transform.rotation.eulerAngles;
		desiredRotation = normalRotation;
	}

	void Update () {
		turnLevel ();
	}

	//####> CUSTOM FUNCTIONS <##############################################################################################
	void turnLevel() {
		elapsedTurnTime += Time.deltaTime;
	}

	void resetOvershootRotation() {
		//Remove any rotation amount over 360, to remove redundancy and high rotation values
		Vector3 dr = desiredRotation;
		Vector3 cr = transform.rotation.eulerAngles;
		while (Mathf.Abs (cr.z) > 360) {
			dr.z += (getNumberWeight (dr.z) * 360);
			cr.z += (getNumberWeight (dr.z) * 360);
		}
			
		desiredRotation = dr;
		transform.rotation = Quaternion.Euler (cr);
	}

	void advanceScreen(int dir) {
		if (isDivBy (transform.rotation.eulerAngles.z, 180)) {
			elapsedTurnTime = 0;
			resetOvershootRotation ();

			//Add 180 Degrees to desiredRotation in given direction
			Vector3 dr = desiredRotation;
			dr.z += (dir * 180);
			desiredRotation = dr;
		}
	}

	void controllerInput() {
		if (Input.GetKeyDown ("Q")) {
			advanceScreen (-1);
		}

		if (Input.GetKeyDown ("W")) {
			advanceScreen (1);
		}
	}

	public int getNumberWeight(float num) {
		if (num > 0) {
			return 1;
		}
		if (num < 0) {
			return -1;
		}
		if (num == 0) {
			return 0;
		}
	}

	public bool isDivBy(float input, float div) {
		if ((input % div) == 0) {
			return true;
		} else {
			return false;
		}
	}
}
