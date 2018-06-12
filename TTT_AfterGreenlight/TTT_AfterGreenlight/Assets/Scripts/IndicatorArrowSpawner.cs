using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorArrowSpawner : MonoBehaviour {

	public GameObject IndicatorArrow;
	private GameObject[] Players = new GameObject[2];

	//BUILT-IN FUNCTIONS===================================================================================================================
	void Start () {
		Players[0] = transform.GetChild(0).gameObject;
		Players[1] = transform.GetChild(1).gameObject;
	}

	//CUSTOM FUNCTIONS===================================================================================================================
	public void spawnIndicatorArrow(int pl) {
		//Delete previous arrows
		GameObject[] indicators = GameObject.FindGameObjectsWithTag ("Indicator");
		foreach (GameObject g in indicators) {
			GameObject.Destroy (g);
		}

		//Prevent Array overflow
		pl = Mathf.Clamp(pl, 0, 1);

		//Instantiate new Arrow
		Quaternion newRot = Quaternion.Euler(new Vector3(0,0,180*pl));
		GameObject newArrow = Instantiate (IndicatorArrow, new Vector3(999,999,999), newRot) as GameObject;
		newArrow.GetComponent<IndicatorArrowScript> ().follow = Players [pl].transform;
	}
}
