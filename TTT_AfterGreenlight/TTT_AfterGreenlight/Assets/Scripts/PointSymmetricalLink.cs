using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSymmetricalLink : MonoBehaviour {

	public Transform midPoint;
	[HideInInspector]
	public GameObject copyObj;
	private GameObject originalObj;

	//BUILT-IN FUNCTIONS===================================================================================================================
	void Start () {
		originalObj = transform.GetChild (0).gameObject;
	}


	void FixedUpdate () {
		GameObject.Destroy (copyObj);
		Vector3 newOffset = (midPoint.position - originalObj.transform.position);
		Vector3 newPos = midPoint.position + newOffset;
		copyObj = Instantiate (originalObj, newPos, originalObj.transform.rotation, this.gameObject.transform) as GameObject;
		copyObj.transform.localScale = -copyObj.transform.localScale;
	}
}
