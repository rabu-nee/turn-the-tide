using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDelete : MonoBehaviour {
	public float timeTilDelete = 0.2f;
	float curTime = 0;

	void Update () {
		curTime += Time.deltaTime;
		if (curTime > timeTilDelete) {
			Destroy (this.gameObject);
		}
	}
}
