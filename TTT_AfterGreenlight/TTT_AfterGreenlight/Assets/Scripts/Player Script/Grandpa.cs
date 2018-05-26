using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grandpa : Player {

    public float throwPower, speedWithoutStick;
    public float rotationSpeed = 5f;
    public float maxThrowAngle, minThrowAngle;
    public GameObject StickPrefab;
    
    //public for testing
    public bool noStick;
    private bool canThrow, canPick, aimingMode;

    public Transform launchPos;
    private Transform initLaunchPos;

    public GameObject Stick;
    private Vector3 throwDirection;


	// Use this for initialization
	new void Start () {
        base.Start();
        launchPos = this.transform.Find("launchPos");
        initLaunchPos = launchPos;

	}

    new void Update()
    {
        Debug.DrawRay(this.transform.position, Vector3.forward * throwPower);
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
        //AIMING
        if (Input.GetButtonDown("Throw"))
        {
            canMove = false;
            Debug.Log("aiming");
            canThrow = true;
            aimingMode = true;
        }

        if (aimingMode)
        {
            float angle = Vector3.SignedAngle(launchPos.transform.position - this.transform.position, (Vector3.up * scaleY), Vector3.forward);
            if(this.transform.localScale.x < 0)
            {
                angle = -angle;
            }

            Debug.Log(angle);

            float rot = Input.GetAxisRaw("Vertical") * rotationSpeed * Mathf.Sign(this.transform.localScale.x);
            
            float newAngle = Mathf.Clamp(angle + rot, minThrowAngle, maxThrowAngle);

            float newRot = newAngle - angle;
            
            launchPos.RotateAround(this.transform.position, Vector3.forward, newRot);
            
            throwDirection = launchPos.transform.position - this.transform.position;
            

            
        }

        //THROWING
        if (Input.GetButtonUp("Throw") && canThrow)
        {
            aimingMode = false;
            noStick = true;
            canPick = false;
            Debug.Log("throwing");

            //animation
            StartCoroutine(Throw());
        }
    }

    IEnumerator Throw()
    {
        //ANIMATION CONTROLLER
        float aniLength = 0.1f; //FILLER

        yield return new WaitForSeconds(aniLength);


        //instantiate stick
        Stick = Instantiate(StickPrefab) as GameObject;
        Stick.transform.position = launchPos.position;
        Stick.transform.localScale = new Vector2(Stick.transform.localScale.x * -(Mathf.Sign(this.transform.localScale.x)), Stick.transform.localScale.y * Mathf.Sign(scaleY));
        Stick.GetComponent<Rigidbody2D>().velocity = throwDirection * throwPower;

        launchPos = initLaunchPos;

        canPick = true;
        canMove = true;
        canThrow = false;
    }

    private void PickStick()
    {
        if(Input.GetButtonDown("Throw") && canPick) //same button as throw
        {
            Debug.Log("Got stick");
            noStick = false;
            Destroy(Stick);
        }
    }
}
