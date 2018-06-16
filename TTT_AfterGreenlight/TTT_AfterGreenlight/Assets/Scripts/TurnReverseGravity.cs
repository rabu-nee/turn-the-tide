﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnReverseGravity : MonoBehaviour {

	private int curScreen;
	private int lastScreen;
	private float standardGravityScale;
	private LevelRotation lrot;
	private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
		lrot = GameObject.Find ("LevelPlate").GetComponent<LevelRotation>();
		rb2d = GetComponent<Rigidbody2D> ();
		standardGravityScale = rb2d.gravityScale;
		lastScreen = lrot.getCurScreen ();
	}
	
	// Update is called once per frame
	void Update () {
		curScreen = lrot.getCurScreen ();
		if (curScreen != lastScreen) {
			rb2d.gravityScale = standardGravityScale * curScreen;
			lastScreen = curScreen;
		}
	}
}
