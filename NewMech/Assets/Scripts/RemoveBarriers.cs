using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBarriers : MonoBehaviour {

	//BUILT-IN-FUNCTIONS===================================================================================================================

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.CompareTag ("Player")) {
			GameObject[] del = GameObject.FindGameObjectsWithTag (this.tag);
			foreach (GameObject g in del) {
				if (g != this.gameObject) {
					GameObject.Destroy (g);
				}
			}
			GameObject.Destroy (this.gameObject);
		}
	}
}
