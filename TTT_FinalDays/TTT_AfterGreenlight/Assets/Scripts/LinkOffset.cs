using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkOffset : MonoBehaviour {

	public Vector3 offset = Vector3.zero;
	public Sprite linkSprite;
	[HideInInspector]
	private GameObject originalObj;
	private GameObject copyObj;

	//BUILT-IN FUNCTIONS===================================================================================================================
	void Start () {
		originalObj = transform.GetChild (0).gameObject;

		//das ist ein test
		copyObj = Instantiate (originalObj);
		copyObj.transform.parent = originalObj.transform.parent;
		Destroy (copyObj.GetComponent<Rigidbody2D>());
		copyObj.tag = "MovingPlatform";
		if (linkSprite != null) {
			copyObj.GetComponent<SpriteRenderer> ().sprite = linkSprite;
		}
	}
	

	void FixedUpdate () {
		Vector3 newPos = originalObj.transform.position + offset;
		copyObj.transform.position = newPos;
		copyObj.transform.rotation = originalObj.transform.rotation;
		copyObj.transform.localScale = originalObj.transform.localScale;
	}
}
