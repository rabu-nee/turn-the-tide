using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneTransition : MonoBehaviour {

	public Camera mainCam;
	public float positionShiftAmount = 6f;
	public float cameraZoomAmount = 2.3f;
	public float camSpeed = 2f;
	public float rotSpeed = 20f;

	private Vector3 standardPosition;
	private Quaternion standardRotation;
	private float standardCameraZoom;

	private Vector3 desiredPosition;
	private Quaternion desiredRotation;
	private float desiredCaneraZoom;

	private bool smoothTransition = true;
	private bool startAnimFinished = false;
	private AsyncOperation asyncLoad;


	//BUILT-IN FUNCTIONS===================================================================================================================
	void Start () {
		//Set Standards
		setStandardVariables();

		setStartVariables ();
	}

	void Update () {
		if ((hasReachedStandard ()) && (smoothTransition)) {
			activatePlayerControl ();
		} else {
			adjustPlate ();
		}
	}

	//CUSTOM FUNCTIONS===================================================================================================================
	private void adjustPlate() {
		transform.position = Vector3.Lerp (transform.position, desiredPosition, Time.deltaTime * camSpeed);
		mainCam.orthographicSize = Mathf.Lerp (mainCam.orthographicSize, desiredCaneraZoom, Time.deltaTime * camSpeed);

		if (smoothTransition) {
			transform.rotation = Quaternion.Lerp (transform.rotation, desiredRotation, Time.deltaTime * camSpeed);
		}
		if (!smoothTransition) {
			transform.rotation = Quaternion.RotateTowards (transform.rotation, desiredRotation, Time.deltaTime * rotSpeed);
		}
	}

	public void setStartVariables() {
		//Set Position
		Vector3 nPos = standardPosition;
		nPos.z -= positionShiftAmount;
		transform.position = nPos;
		desiredPosition = standardPosition;

		//Set Rotation
		Vector3 nRot = standardRotation.eulerAngles;
		nRot.x -= 86;
		transform.rotation = Quaternion.Euler (nRot);
		desiredRotation = standardRotation;

		//Set Camera Shift
		mainCam.orthographicSize = standardCameraZoom + cameraZoomAmount;
		desiredCaneraZoom = standardCameraZoom;

		smoothTransition = true;
	}

	public void setExitVariables() {
		//Set Position
		Vector3 nPos = standardPosition;
		nPos.z -= positionShiftAmount;
		desiredPosition = nPos;

		//Set Rotation
		Vector3 nRot = standardRotation.eulerAngles;
		nRot.x += 90;
		desiredRotation = Quaternion.Euler(nRot);

		//Set Camera Shift
		desiredCaneraZoom = standardCameraZoom + cameraZoomAmount;

		smoothTransition = false;

		//Start loading next Level
		StartCoroutine (LoadNextLevelAsync ());
	}
		
	public void setStandardVariables(){
		standardPosition = transform.position;
		standardRotation = transform.rotation;
		standardCameraZoom = mainCam.orthographicSize;
	}

	private void activatePlayerControl() {
		GetComponent<LevelRotation> ().enabled = true;
		enabled = false;
		//Plus player controls...
	}

	private bool hasReachedExit() {
		Debug.Log (transform.rotation.eulerAngles.x);
		if (Mathf.CeilToInt(transform.rotation.eulerAngles.x) == 90) {
			return true;
		} else {
			return false;
		}
	}

	private bool hasReachedStandard() {
		if (transform.rotation == standardRotation) {
			return true;
		} else {
			return false;
		}
	}

	IEnumerator LoadNextLevelAsync() {
		int nextSceneIndex = SceneManager.GetActiveScene ().buildIndex + 1;
		asyncLoad = SceneManager.LoadSceneAsync (nextSceneIndex);
		asyncLoad.allowSceneActivation = false;

		while (!asyncLoad.isDone) {
			if (asyncLoad.progress >= 0.9f) {
				if (hasReachedExit ()) {
					asyncLoad.allowSceneActivation = true;
				}
			}

			yield return null;
		}
			
	}
		
}
