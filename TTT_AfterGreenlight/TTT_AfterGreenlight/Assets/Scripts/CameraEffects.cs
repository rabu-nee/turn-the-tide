using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffects : MonoBehaviour {

	public float movementFriction;
	public float movementSpeed;
	public float fovShift = 2;

	private Vector3 standardPosition;
	private Vector3 joltPosition;
	private Vector3 shakePosition;
	private Vector3 curVelocity = Vector3.zero;
	private Camera cam;

	private bool camShake = false;
	private float camShakeDuration;
	private float camShakeIntensity;
	private float camShakeMaxIntensity;
	private float camShakeDelayTime;
	private float elapsedTime = 0;
	private float standardFOV;

	//BUILT-IN-FUNCTIONS===================================================================================================================

	void Start () {
		setStandardPosition (transform.position);
		cam = GetComponent<Camera> ();
		standardFOV = cam.fieldOfView;
	}

	void Update () {
		applyJolt ();
		if (camShake) {
			applyCameraShake ();
		}
		applyPositions ();
	}

	//CUSTOM FUNCTIONS===================================================================================================================

	public void addCameraJolt(Vector3 amount) {
		curVelocity = amount;
	}

	public void addCameraShake(float duration, float maxIntensity, float delayTime) {
		camShake = true;
		camShakeDuration = duration;
		camShakeMaxIntensity = maxIntensity;
		camShakeDelayTime = delayTime;
	}

	private void applyCameraShake() {
		if (camShakeDelayTime > 0) {
			camShakeDelayTime -= Time.deltaTime;
		} else {
			if (elapsedTime < camShakeDuration) {
				elapsedTime += Time.deltaTime;
				float midTime = camShakeDuration / 2;

				if (elapsedTime <= midTime) {
					camShakeIntensity = (elapsedTime / midTime) * camShakeMaxIntensity;
				} else {
					camShakeIntensity = (1 - ((elapsedTime - midTime) / midTime)) * camShakeMaxIntensity;
				}
				cam.fieldOfView = standardFOV + (camShakeIntensity * fovShift);
				shakePosition = standardPosition + Random.insideUnitSphere * camShakeIntensity;

			} else {
				elapsedTime = 0;
				shakePosition = standardPosition;
				camShake = false;
			}
		}
	}

	private void applyPositions() {
		transform.position = standardPosition + (joltPosition - standardPosition) + (shakePosition - standardPosition);
	}

	private void applyJolt() {
		curVelocity = Vector3.Lerp (curVelocity, Vector3.zero, Time.deltaTime * movementFriction);
		joltPosition = Vector3.Lerp (joltPosition, (standardPosition + curVelocity), Time.deltaTime * movementSpeed);
	}

	//SETTER============================================================================================================================

	public void setStandardPosition(Vector3 pos) {
		standardPosition = pos;
		joltPosition = pos;
		shakePosition = pos;
	}
}
