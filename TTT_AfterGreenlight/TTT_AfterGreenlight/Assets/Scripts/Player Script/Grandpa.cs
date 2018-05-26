using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grandpa : Player {

    public float throwDistance, speedWithoutStick;
    public bool noStick; //public for testing
    public Transform launchPos;


	// Use this for initialization
	new void Start () {
        base.Start();
        launchPos = this.transform.Find("launchPos");
	}

    new void Update()
    {
        base.Update();
        if (selected)
        {
            if (!noStick)
            {
                ThrowStick();
            }
            else
            {
                PickStick();
            }
        }
    }
    
    private void ThrowStick()
    {
        if(Input.GetButtonDown("Throw"))
        {
            Debug.Log("Throw stick");
            noStick = true;
        }
    }

    private void PickStick()
    {
        if(Input.GetButtonDown("Throw"))
        {
            Debug.Log("Got stick");
            noStick = false;
        }
    }
}
