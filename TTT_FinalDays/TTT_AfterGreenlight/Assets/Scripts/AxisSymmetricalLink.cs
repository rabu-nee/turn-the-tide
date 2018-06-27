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
	private GameObject lr;

	//BUILT-IN FUNCTIONS===================================================================================================================
	void Start () {
		originalObj = transform.GetChild (0).gameObject;
		lr = GameObject.FindGameObjectWithTag ("CurrentLevel");
	}


	void FixedUpdate () {
		//Reset bools
		if (x && y) {
			x = false;
		}
			
		GameObject.Destroy (copyObj);
		if (x) {
			//Calculating Offset
			Vector3 newOffset = new Vector3(0, (midPoint.position.y - originalObj.transform.position.y), 0);
			Vector3 angles = new Vector3 (0, 0, lr.transform.rotation.eulerAngles.z);
			//Rotating Offset around specified angle
			newOffset = Quaternion.Euler (angles) * newOffset;
			Vector3 newPos = new Vector3(originalObj.transform.position.x, (midPoint.position.y), originalObj.transform.position.z) + newOffset;
			//Rotate newPos by rotation of Level
			Vector3 dir = newPos - midPoint.position;
			dir = Quaternion.Euler (lr.transform.rotation.eulerAngles) * dir;
			newPos = dir + midPoint.position;
			//Apply Position to copied Object
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
