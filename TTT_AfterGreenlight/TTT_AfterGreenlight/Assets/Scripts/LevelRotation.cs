using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRotation : MonoBehaviour {
	
	public float rotationSpeed;
	public float movementSpeed = 2f;
	public float velocityIntensity = 2f;

	private Quaternion standardRotation;
	private Vector3 curEuler;
	private Vector3 desiredEuler;
	private Vector3 standardPosition;
	private int curScreen = 1;
	private bool buttonHit = false;
	private bool allowInput = true;
	private float elapsedTurnTime = 0;
	private CameraEffects CamFX;

	//BUILT-IN FUNCTIONS===================================================================================================================

	void Start() {
		CamFX = Camera.main.GetComponent<CameraEffects> ();

		standardRotation = transform.rotation;
		desiredEuler = standardRotation.eulerAngles;
		curEuler = standardRotation.eulerAngles;
		standardPosition = transform.position;
	}

	void Update () {
		controllerInput ();

		turnScreen ();
		resetOvershootRotation ();
	}

	//CUSTOM FUNCTIONS===================================================================================================================

	public void advanceScreen(int dir) {
		//Check if last turn animation completed
		if (isDivBy (curEuler.z, 180)) {
			//Initiate screen turning
			elapsedTurnTime = 0;
			desiredEuler = addEulerRotation (desiredEuler, dir);
			curScreen = -curScreen;
			turnVelocity (dir);
		}
	}

	private void turnScreen() {
		//Control rotation speed with a square function;
		elapsedTurnTime += Time.deltaTime;
		float newRotSpeed = (elapsedTurnTime * elapsedTurnTime * elapsedTurnTime) * rotationSpeed;

		//Change camera rotation to frame current screen
		curEuler = Vector3.Lerp(curEuler, desiredEuler, Time.deltaTime * newRotSpeed);
		transform.rotation = Quaternion.Euler (curEuler);
	}

	private void resetOvershootRotation() {
		//Keep curEuler & desiredEuler as low as possible to avoid potential overflow
		while (Mathf.Abs (curEuler.y) >= 360) {
			curEuler.y -= (360 * getSign (curEuler.z));
			desiredEuler.y -= (360 * getSign (desiredEuler.y));
		}
	}

	private void turnVelocity(int dir) {
		//Calculating Velocity direction
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
	}

	private Vector3 addEulerRotation(Vector3 euler, int dir) {
		euler.z += 180 * (dir);

		return euler;
	}

	private int getSign(float num) {
		if (num >= 0) {
			return 1;
		} else {
			return -1;
		}
	}

	private bool isDivBy(float input, float div) {
		if ((input % div) == 0) {
			return true;
		} else {
			return false;
		}
	}


	//GETTER===================================================================================================================

	public int getCurScreen() {
		return curScreen;
	}

	//DEBUG===================================================================================================================

	void controllerInput() {
		if (allowInput) { 
			/*
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
			*/


			if ((Input.GetKeyDown(KeyCode.Q)) && (buttonHit == false)) {
				advanceScreen (-1);
				buttonHit = true;
			}

			//Turn Screen to the right
			if ((Input.GetKeyDown(KeyCode.W)) && (buttonHit == false)) {
				advanceScreen (1);
				buttonHit = true;
			}

			if (!(Input.GetKeyDown(KeyCode.Q)) && !(Input.GetKeyDown(KeyCode.Q))) {
				buttonHit = false;
			}

		}
	}

	public void setAllowInput(bool input) {
		allowInput = input;
	}
}