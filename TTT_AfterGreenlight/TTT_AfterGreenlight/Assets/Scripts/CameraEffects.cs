using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffects : MonoBehaviour {

	public float movementFriction;
	public float movementSpeed;

	private Vector3 standardPosition;
	private Vector3 curVelocity = Vector3.zero;

	//BUILT-IN-FUNCTIONS===================================================================================================================

	void Start () {
		setStandardPosition (transform.position);
	}

	void Update () {
		applyMovement ();
	}

	//CUSTOM FUNCTIONS===================================================================================================================

	public void addCameraJolt(Vector3 amount) {
		curVelocity = amount;
	}

	public void addCameraShake(float duration, float intensity) {

	}

	private void applyMovement() {
		curVelocity = Vector3.Lerp (curVelocity, Vector3.zero, Time.deltaTime * movementFriction);
		transform.position = Vector3.Lerp (transform.position, (standardPosition + curVelocity), Time.deltaTime * movementSpeed);
	}

	//SETTER============================================================================================================================

	public void setStandardPosition(Vector3 pos) {
		standardPosition = pos;
	}
}
