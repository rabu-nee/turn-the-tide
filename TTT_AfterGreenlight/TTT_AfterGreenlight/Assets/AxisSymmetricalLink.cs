using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisSymmetricalLink : MonoBehaviour {

	public Transform midPoint;
	public float angleOffset = 0f;
	public bool x = false;
	public bool y = true;
	[HideInInspector]
	public GameObject copyObj;
	private GameObject originalObj;

	//BUILT-IN FUNCTIONS===================================================================================================================
	void Start () {
		originalObj = transform.GetChild (0).gameObject;
	}


	void FixedUpdate () {
		//Reset bools
		if (x && y) {
			x = false;
		}

		GameObject.Destroy (copyObj);
		if (x) {
			Vector3 newOffset = new Vector3(0, (midPoint.position.y - originalObj.transform.position.y), 0);
			Vector3 angles = new Vector3 (0, 0, angleOffset);
			newOffset = Quaternion.Euler (angles) * newOffset;
			Vector3 newPos = new Vector3(originalObj.transform.position.x, (midPoint.position.y), originalObj.transform.position.z) + newOffset;
			copyObj = Instantiate (originalObj, newPos, originalObj.transform.rotation, this.gameObject.transform) as GameObject;
			copyObj.transform.localScale = new Vector3(copyObj.transform.localScale.x, -copyObj.transform.localScale.y, copyObj.transform.localScale.z);
		}
		if (y) {
			Vector3 newOffset = new Vector3((midPoint.position.x - originalObj.transform.position.x), 0, 0);
			Vector3 angles = new Vector3 (0, 0, angleOffset);
			newOffset = Quaternion.Euler (angles) * newOffset;
			Vector3 newPos = new Vector3((midPoint.position.x), originalObj.transform.position.y, originalObj.transform.position.z) + newOffset;
			copyObj = Instantiate (originalObj, newPos, originalObj.transform.rotation, this.gameObject.transform) as GameObject;
			copyObj.transform.localScale = new Vector3(-copyObj.transform.localScale.x, copyObj.transform.localScale.y, copyObj.transform.localScale.z);
		}
	}
}
