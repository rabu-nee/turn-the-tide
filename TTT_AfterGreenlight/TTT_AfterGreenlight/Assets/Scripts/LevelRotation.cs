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
		controllerInput ();
		turnLevel ();
	}

	//####> CUSTOM FUNCTIONS <###############################################################################################
	void turnLevel() {
		elapsedTurnTime += Time.deltaTime;

		Vector3 newEuler = Vector3.Lerp (transform.rotation.eulerAngles, desiredRotation, Time.deltaTime * standardTurnTime);
		transform.rotation = Quaternion.Euler(newEuler);
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
		if (Input.GetKeyDown (KeyCode.Q)) {
			Debug.Log ("hit q");
			advanceScreen (-1);
		}

		if (Input.GetKeyDown (KeyCode.W)) {
			Debug.Log ("hit w");
			advanceScreen (1);
		}
	}

	public int getNumberWeight(float num) {
		int ret = 0;

		if (num > 0) {
			ret = 1;
		} 
		if (num < 0) {
			ret = -1;
		}

		return ret;
	}

	public bool isDivBy(float input, float div) {
		if ((input % div) == 0) {
			return true;
		} else {
			return false;
		}
	}
}
