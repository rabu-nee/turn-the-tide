using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelection : MonoBehaviour {

	public GameObject[] Players;
	CameraPositioning camPos;
	int curPlayer;
	
	//BUILT-IN-FUNCTIONS===================================================================================================================

	void Start () {
		Players = GameObject.FindGameObjectsWithTag ("Player");
		camPos = GameObject.Find("RotatingPiece").GetComponent<CameraPositioning> ();
	}

	void FixedUpdate() {
		curPlayer = Mathf.Clamp(camPos.getCurScreen (), 0, 1);

		for (int i = 0; i < Players.Length; i++) {
			Players [i].GetComponent<Selected> ().selected = false;
		}

		Players [curPlayer].GetComponent<Selected> ().selected = true;
	}

	public int currentPlayer() {
		return Mathf.Clamp(camPos.getCurScreen (), 0, 1);
	}
}
