using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearScript : MonoBehaviour {

	private GearScript childGS;
	private Transform gearSpriteTransform;
	private Vector3 desiredEuler;

	//BUILT-IN FUNCTIONS===================================================================================================================
	void Start () {
		if (transform.childCount > 1) {
			childGS = transform.GetChild (1).gameObject.GetComponent<GearScript> ();
		}
		gearSpriteTransform = transform.GetChild (0);
		desiredEuler = gearSpriteTransform.localRotation.eulerAngles;
	}

	//CUSTOM FUNCTIONS===================================================================================================================
	public void rotate(float angle) {
		desiredEuler.z += angle;
		//Reset desiredEuler
		if (desiredEuler.z > 360) {
			desiredEuler.z -= 360;
		}
		if (desiredEuler.z < -360) {
			desiredEuler.z += 360;
		}
		gearSpriteTransform.localRotation = Quaternion.Euler (desiredEuler);

		//Call rotate() on child Gear
		if (childGS != null) {
			childGS.rotate (-angle);
		}
	}
}
