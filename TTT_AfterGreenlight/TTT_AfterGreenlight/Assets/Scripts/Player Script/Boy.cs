using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boy : Player {

	// Use this for initialization
	void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	void Update () {
        Move();
        Jump();
	}
}
