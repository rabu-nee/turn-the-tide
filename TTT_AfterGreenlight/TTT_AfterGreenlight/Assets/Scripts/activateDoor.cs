using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateDoor : MonoBehaviour {

	public GameObject door;

	void OnCollisionEnter2D(Collision2D other) {
		//if (other.gameObject.CompareTag ("Stick")) {
			if (door != null) {
				Destroy (door);
				//Destroy (other.gameObject);
			}
		//}
	}
}
