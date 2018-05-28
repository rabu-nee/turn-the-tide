using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkOffset : MonoBehaviour {

	public Vector3 offset = Vector3.zero;
	[HideInInspector]
	public GameObject copyObj;
	private GameObject originalObj;

	//BUILT-IN FUNCTIONS===================================================================================================================
	void Start () {
		originalObj = transform.GetChild (0).gameObject;
	}
	

	void FixedUpdate () {
		GameObject.Destroy (copyObj);
		Vector3 newPos = originalObj.transform.position + offset;
		copyObj = Instantiate (originalObj, newPos, originalObj.transform.rotation, this.gameObject.transform) as GameObject;
	}
}
