﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWinState : MonoBehaviour {

	public float waitBeforeTransition = 2f;
	private int collectedCrystals = 0;

	public void addCrystal() {
		collectedCrystals++;
		GameObject[] totalCrystals = GameObject.FindGameObjectsWithTag ("Crystal");
		if (collectedCrystals == totalCrystals.Length) {
			//Win state reached!
			StartCoroutine(winExec());
		}
	}

	IEnumerator winExec() {
		bool t = true;
		while (t) {
			yield return new WaitForSeconds(waitBeforeTransition);

			GameObject levelContainer = GameObject.FindGameObjectWithTag("CurrentLevel");
			levelContainer.GetComponent<LevelRotation>().enabled = false;
			levelContainer.GetComponent<SceneTransition>().enabled = true;
			levelContainer.GetComponent<SceneTransition> ().setStandardVariables ();
			levelContainer.GetComponent<SceneTransition> ().setExitVariables ();


			t = false;
		}

	}
}