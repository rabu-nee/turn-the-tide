using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearParent : MonoBehaviour {
	
	public float rotationMultiplier = 2f;
	public float rotationFriction = 1f;
	public float velocityAmount = 40f;

	private float velocity = 0f;
	private GearScript childGS;
	private LevelRotation lr;
	private int lastScreen;

	//BUILT-IN FUNCTIONS===================================================================================================================
	void Start () {
		childGS = transform.GetChild (0).gameObject.GetComponent<GearScript> ();
		lr = GameObject.FindGameObjectWithTag ("CurrentLevel").GetComponent<LevelRotation> ();
		lastScreen = lr.getCurScreen ();
	}

	void FixedUpdate () {
		//Apply rotation based on Turning
		if (lr.getCurScreen () != lastScreen) {
			lastScreen = lr.getCurScreen ();
			velocity += (velocityAmount * lr.getLastDir ());
		}

		//Apply friction to velocity
		velocity = Mathf.Lerp(velocity, 0f, Time.deltaTime * rotationFriction);

		//Rotate Gears based on velocity
		childGS.rotate (Time.deltaTime * (velocity * rotationMultiplier));
	}
}
