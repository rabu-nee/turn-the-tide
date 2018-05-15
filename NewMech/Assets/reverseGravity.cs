using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reverseGravity : MonoBehaviour {

	void Start () {
		this.GetComponent<Rigidbody2D> ().gravityScale = -this.GetComponent<Rigidbody2D> ().gravityScale;
	}

}
