using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformObj : MonoBehaviour {

	public float movementSpeed = 1f;
	public Transform[] desiredTransforms = new Transform[2];

	private GameObject steadyObj;
	private LevelRotation lr;
	private int lastDir;

	//BUILT-IN FUNCTIONS===================================================================================================================
	void Start () {
		steadyObj = transform.GetChild (0).gameObject;
		lr = GameObject.FindGameObjectWithTag ("CurrentLevel").GetComponent<LevelRotation> ();
	}

	void FixedUpdate () {
		lastDir = lr.getLastDir ();
		lastDir = Mathf.Clamp (lastDir, 0, 1);

		Transform curDesirdedTransform = desiredTransforms [lastDir];
		steadyObj.transform.position = Vector3.Lerp (steadyObj.transform.position, curDesirdedTransform.position, Time.deltaTime * movementSpeed);
		steadyObj.transform.rotation = Quaternion.Lerp (steadyObj.transform.rotation, curDesirdedTransform.rotation, Time.deltaTime * movementSpeed);
	}
}
