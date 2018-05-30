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
<<<<<<< HEAD
		Vector3 newPos = originalObj.transform.position + offset;
=======
		Vector3 newOffset = Quaternion.Euler (0, 0, GameObject.FindGameObjectWithTag("CurrentLevel").transform.rotation.eulerAngles.z) * offset;
		Vector3 newPos = originalObj.transform.position + newOffset;
>>>>>>> e7174d0a3fa3012130ce5352aaa97eb4bd0e3d2e
		copyObj = Instantiate (originalObj, newPos, originalObj.transform.rotation, this.gameObject.transform) as GameObject;
		copyObj.tag = "MovingPlatform";
	}
}
