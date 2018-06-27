using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorArrowSpawner : MonoBehaviour {

	public GameObject IndicatorArrow;
	public float arrowOffset = 1;
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
		int dir = pl;
		pl = Mathf.Clamp(pl, 0, 1);

		//Instantiate new Arrow
		Quaternion newRot = Quaternion.Euler(new Vector3(0,0,180*pl));
		Vector3 newScale = new Vector3 (1, -1, 1);
		GameObject newArrow = Instantiate (IndicatorArrow, new Vector3(999,999,999), newRot) as GameObject;
		newArrow.GetComponent<IndicatorArrowScript> ().follow = Players [pl].transform;
		newArrow.transform.localScale = newScale;
		//Set arrow offset
		newArrow.GetComponent<IndicatorArrowScript>().arrowOffset = dir * arrowOffset;
	}
}
