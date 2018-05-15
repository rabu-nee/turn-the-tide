using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositioning : MonoBehaviour {

	Quaternion standardRotation;
	Vector3 CurEuler = Vector3.zero;
	public GameObject[] Players;
	Vector3 curEuler;
	Vector3 desiredEuler;
	public int maxTurns = 1;
	public float rotationSpeed = 2f;
	public float movementSpeed = 2f;
	public float velocityIntensity = 2f;
	Vector3 standardPosition;
	public float yOffset = 1;
	int curScreen = 1;
	private bool buttonHit = false;

	private bool allowInput = true;
	private PlayerSelection ps;

	//BUILT-IN-FUNCTIONS===================================================================================================================

	void Start() {
		standardRotation = transform.rotation;
		desiredEuler = standardRotation.eulerAngles;
		curEuler = standardRotation.eulerAngles;

		standardPosition = transform.position;
		ps = GameObject.Find ("Players").GetComponent<PlayerSelection> ();
	}

	void Update () {
		//==============
		debugInput ();
		//==============

		//Change camera rotation to frame current screen
		curEuler = Vector3.Lerp(curEuler, desiredEuler, Time.deltaTime * rotationSpeed);
		transform.rotation = Quaternion.Euler (curEuler);

		//Keep curEuler & desiredEuler as low as possible to avoid potential overflow
		while (Mathf.Abs (curEuler.y) >= 360) {
			curEuler.y -= (360 * getSign (curEuler.z));
			desiredEuler.y -= (360 * getSign (desiredEuler.y));
		}

		//Change camera height based on current screen
		Vector3 offsetPosition = new Vector3 (standardPosition.x, standardPosition.y + (curScreen * yOffset), standardPosition.z);
		transform.position = Vector3.Lerp (transform.position, offsetPosition, Time.deltaTime * movementSpeed);
	}

	//FUNCTIONS===================================================================================================================

	public void advanceScreen(int dir) {
		desiredEuler = addEulerRotation (desiredEuler, dir);
		curScreen = -curScreen;
		turnVelocity (dir);
	}

	public void turnVelocity(int dir) {
		Vector2 vel = new Vector2 (1, 1);
		vel.x *= dir;
		vel *= velocityIntensity;
		//Adding velocity to Physics Objects
		GameObject[] po = GameObject.FindGameObjectsWithTag("PhysObj");
		if (po != null) {
			foreach (GameObject g in po) {
				int nDir = (int)Mathf.Clamp (g.GetComponent<Rigidbody2D> ().gravityScale, -1, 1);
				g.GetComponent<Rigidbody2D> ().AddForce (vel * nDir * 0.3f);
			}
		}

		//Adding velocity to Players
		foreach (GameObject g in Players) {
			if ((!g.GetComponent<PlayerMovement> ().grounded /*isGrounded()*/) && (g.GetComponent<PlayerMovement> ().getTurns() < maxTurns)) {
				int nDir = g.GetComponent<PlayerMovement>().getGravityWeight();
				g.GetComponent<Rigidbody2D> ().AddForce (vel * nDir);
				g.GetComponent<PlayerMovement> ().disableUntilContact ();
				g.GetComponent<PlayerMovement> ().addTurn ();
			}
		}

	}

	Vector3 addEulerRotation(Vector3 euler, int dir) {
		euler.y += 180 * (dir);

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
			if ((Input.GetButtonDown ("LBumper") || (Input.GetAxis ("LTrigger") > 0)) && (buttonHit == false)) {
				advanceScreen (-1);
				buttonHit = true;
			}

			//Turn Screen to the right
			if ((Input.GetButtonDown ("RBumper") || (Input.GetAxis ("RTrigger") > 0)) && (buttonHit == false)) {
				advanceScreen (1);
				buttonHit = true;
			}

			if ((Input.GetAxis ("LTrigger") == 0) && (Input.GetAxis ("RTrigger") == 0)) {
				buttonHit = false;
			}
		}
	}

	public void setAllowInput(bool input) {
		allowInput = input;
	}
}
