using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grandpa : Player {

    public float throwDistance, speedWithoutStick;


	// Use this for initialization
	void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Move();
        Jump();
	}

    
}
