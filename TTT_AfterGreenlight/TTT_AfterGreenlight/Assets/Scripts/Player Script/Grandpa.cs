using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grandpa : Player {

    public float throwPower, speedWithoutStick;
    public float rotationSpeed = 5f;
    public float maxThrowAngle, minThrowAngle;
    public GameObject StickPrefab;
<<<<<<< HEAD

    public float test;



    private float initSpeed;
    private bool noStick;
=======
    
    //public for testing
    public bool noStick;
>>>>>>> d698f11947f3f1f9edf01d9f0209a10bef2006a8
    private bool canThrow, canPick, aimingMode;

<<<<<<< HEAD
    public Transform aim;
    public Transform launchPos;

    private GameObject Stick;
    public SpriteRenderer stickSprite;
=======
    public Transform launchPos;
    private Transform initLaunchPos;

    public GameObject Stick;
>>>>>>> d698f11947f3f1f9edf01d9f0209a10bef2006a8
    private Vector3 throwDirection;


	// Use this for initialization
	new void Start () {
        base.Start();
<<<<<<< HEAD
        aim = this.transform.parent.Find("grandpa_aim");
        launchPos = aim.transform.Find("launchPos");
        initSpeed = speed;
        aim.gameObject.SetActive(false);
    }
=======
        launchPos = this.transform.Find("launchPos");
        initLaunchPos = launchPos;

	}
>>>>>>> d698f11947f3f1f9edf01d9f0209a10bef2006a8

    new void Update()
    {
        base.Update();
        aim.transform.position = this.transform.position;
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
            anim.SetBool("Walking", false);

            canMove = false;
            Debug.Log("aiming");
            canThrow = true;
            aimingMode = true;
        }

        if (aimingMode)
        {
<<<<<<< HEAD
            //NEW CODE
            aim.gameObject.SetActive(true);
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            if (x != 0.0f || y != 0.0f)
            {
                angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
                Debug.Log(angle);

                aim.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));

                throwDirection = launchPos.transform.position - this.transform.position;
            }

                /* PREVIOUS CODE
                angle = Vector3.SignedAngle(launchPos.transform.position - this.transform.position, (Vector3.up * scaleY), Vector3.forward);
                if(this.transform.localScale.x < 0)
                {
                   angle = -angle;
                }

                Debug.Log(angle);
=======
            float angle = Vector3.SignedAngle(launchPos.transform.position - this.transform.position, (Vector3.up * scaleY), Vector3.forward);
            if(this.transform.localScale.x < 0)
            {
                angle = -angle;
            }
>>>>>>> d698f11947f3f1f9edf01d9f0209a10bef2006a8

                rot = Input.GetAxisRaw("Vertical") * rotationSpeed * Mathf.Sign(this.transform.localScale.x);

<<<<<<< HEAD

                float newAngle = Mathf.Clamp(angle + rot, minThrowAngle, maxThrowAngle);

                float newRot = newAngle - angle;


                launchPos.RotateAround(this.transform.position, Vector3.forward, rot);
                
                throwDirection = launchPos.transform.position - this.transform.position;
                */
=======
            float rot = Input.GetAxisRaw("Vertical") * rotationSpeed * Mathf.Sign(this.transform.localScale.x);
            
            float newAngle = Mathf.Clamp(angle + rot, minThrowAngle, maxThrowAngle);

            float newRot = newAngle - angle;
            
            launchPos.RotateAround(this.transform.position, Vector3.forward, newRot);
            
            throwDirection = launchPos.transform.position - this.transform.position;
            

            
>>>>>>> d698f11947f3f1f9edf01d9f0209a10bef2006a8
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

<<<<<<< HEAD
        yield return new WaitForSeconds(info.Length);

        aim.gameObject.SetActive(false);
        anim.SetBool("Throwing", false);
=======
        yield return new WaitForSeconds(aniLength);
>>>>>>> d698f11947f3f1f9edf01d9f0209a10bef2006a8


        //instantiate stick
        Stick = Instantiate(StickPrefab) as GameObject;
<<<<<<< HEAD
        Stick st = Stick.GetComponent<Stick>();
        //st.hitAngle *= -Mathf.Sign(angle);
        Stick.transform.position = launchPos.position;

        Stick.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
=======
        Stick.transform.position = launchPos.position;
        Stick.transform.localScale = new Vector2(Stick.transform.localScale.x * -(Mathf.Sign(this.transform.localScale.x)), Stick.transform.localScale.y * Mathf.Sign(scaleY));
>>>>>>> d698f11947f3f1f9edf01d9f0209a10bef2006a8
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
