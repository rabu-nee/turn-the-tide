using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositioning : MonoBehaviour {

	Quaternion standardRotation;
	Vector3 CurEuler = Vector3.zero;
	Vector3 curEuler;
	Vector3 desiredEuler;
	public float rotationSpeed = 2f;
	public float movementSpeed = 2f;
	Vector3 standardPosition;
	public float yOffset = 1;
	int curScreen = 1;
	private bool allowInput = true;

	//BUILT-IN-FUNCTIONS===================================================================================================================

	void Start() {
		standardRotation = transform.rotation;
		desiredEuler = standardRotation.eulerAngles;
		curEuler = standardRotation.eulerAngles;

		standardPosition = transform.position;
	}

	void Update () {
		//==============
		debugInput ();
		//==============

		//Change camera rotation to frame current screen
		curEuler = Vector3.Lerp(curEuler, desiredEuler, Time.deltaTime * rotationSpeed);
		transform.rotation = Quaternion.Euler (curEuler);

		//Keep curEuler & desiredEuler as low as possible to avoid potential overflow
		while (Mathf.Abs (curEuler.z) >= 360) {
			curEuler.z -= (360 * getSign (curEuler.z));
			desiredEuler.z -= (360 * getSign (desiredEuler.z));
		}

		//Change camera height based on current screen
		Vector3 offsetPosition = new Vector3 (standardPosition.x, standardPosition.y + (curScreen * yOffset), standardPosition.z);
		transform.position = Vector3.Lerp (transform.position, offsetPosition, Time.deltaTime * movementSpeed);
	}

	//FUNCTIONS===================================================================================================================

	public void advanceScreen(int dir) {
		desiredEuler = addEulerRotation (desiredEuler, dir);
		curScreen = -curScreen;
	}

	Vector3 addEulerRotation(Vector3 euler, int dir) {
		euler.z += 180 * (dir);

		return euler;
	}

	int getSign(float num) {
		if (num >= 0) {
			return 1;
		} else {
			return -1;
		}
	}


	//GETTER===================================================================================================================

	public int getCurScreen() {
		return curScreen;
	}

	//DEBUG===================================================================================================================

	void debugInput() {
		if (allowInput) { 
			//Turn Screen to the left
			if (Input.GetButtonDown ("LBumper")) {
				advanceScreen (-1);
			}

			//Turn Screen to the right
			if (Input.GetButtonDown ("RBumper")) {
				advanceScreen (1);
			}
		}
	}

	public void setAllowInput(bool input) {
		allowInput = input;
	}
}
