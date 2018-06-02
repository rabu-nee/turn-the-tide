using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grandpa : Player
{

    [Header("Throw", order = 2)]
    public float throwPower, speedWithoutStick;
    //public float rotationSpeed = 5f;
    public GameObject StickPrefab;
    public SpriteRenderer stickSprite;

    private float initSpeed;
    private bool noStick;
    private bool canThrow, canPick, aimingMode;
    private float rot, angle;

    private Transform aim;
    private Transform launchPos;

    private GameObject Stick;
    private Vector3 throwDirection;


    // Use this for initialization
    new void Start()
    {
        base.Start();
        aim = this.transform.parent.Find("grandpa_aim");
        launchPos = aim.transform.Find("launchPos");
        initSpeed = speed;
        aim.gameObject.SetActive(false);
    }

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
            canThrow = true;
            aimingMode = true;
        }

        if (aimingMode)
        {
            //NEW CODE
            aim.gameObject.SetActive(true);
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            if (x != 0.0f || y != 0.0f)
            {
                angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;

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

                rot = Input.GetAxisRaw("Vertical") * rotationSpeed * Mathf.Sign(this.transform.localScale.x);


                float newAngle = Mathf.Clamp(angle + rot, minThrowAngle, maxThrowAngle);

                float newRot = newAngle - angle;


                launchPos.RotateAround(this.transform.position, Vector3.forward, rot);
                
                throwDirection = launchPos.transform.position - this.transform.position;
                */
        }

        //THROWING
        if (Input.GetButtonUp("Throw") && canThrow)
        {
            aimingMode = false;
            noStick = true;
            canPick = false;

            //animation
            StartCoroutine(Throw());
        }
    }

    IEnumerator Throw()
    {
        //ANIMATION CONTROLLER
        anim.SetBool("Throwing", true);

        AnimatorClipInfo[] info = anim.GetCurrentAnimatorClipInfo(0);

        yield return new WaitForSeconds(info.Length);

        aim.gameObject.SetActive(false);
        anim.SetBool("Throwing", false);


        //instantiate stick
        SoundManager.instance.PlaySound(ThrowOrWallJumpSound);
        stickSprite.enabled = false;
        Stick = Instantiate(StickPrefab) as GameObject;
        Stick st = Stick.GetComponent<Stick>();
        Stick.transform.position = launchPos.position;

        Stick.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
        Stick.GetComponent<Rigidbody2D>().velocity = throwDirection * throwPower;
        st.hitAngle = angle + 90;

        speed = speedWithoutStick;

        canPick = true;
        canMove = true;
        canThrow = false;
    }

    private void PickStick()
    {
        if (Input.GetButtonDown("Throw") && canPick) //same button as throw
        {
            noStick = false;
            Destroy(Stick);
            stickSprite.enabled = true;
            speed = initSpeed;
        }
    }
}