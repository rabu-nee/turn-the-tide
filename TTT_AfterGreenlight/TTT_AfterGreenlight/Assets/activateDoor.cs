using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateDoor : MonoBehaviour {

	public GameObject door;

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.CompareTag ("Stick")) {
			if (door != null) {
				Destroy (door);
<<<<<<< HEAD
				Destroy (other.gameObject);
=======
>>>>>>> e7174d0a3fa3012130ce5352aaa97eb4bd0e3d2e
			}
		}
	}
}
