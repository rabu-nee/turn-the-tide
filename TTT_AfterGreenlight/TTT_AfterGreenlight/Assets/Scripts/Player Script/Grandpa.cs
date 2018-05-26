using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grandpa : Player {

    public float throwDistance, speedWithoutStick;
    
    //public for testing
    public bool noStick;
    public Transform launchPos;
    public GameObject Stick;


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
            Aim();
            StartCoroutine(ThrowAnimation());
            noStick = true;
        }
    }

    private void Aim()
    {

    }


    private void PickStick()
    {
        if(Input.GetButtonDown("Throw")) //same button as throw
        {
            Debug.Log("Got stick");
            noStick = false;
        }
    }

    IEnumerator ThrowAnimation()
    {
        float aniLength = 1; //FILLER

        yield return new WaitForSeconds(aniLength);
    }
}
