using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public bool selected;
    public bool outsideForce;
    private bool hanging = false;

    public Animator anim;
    public Rigidbody2D rb;

    public float speed = 5f; //move speed
    private float BaseSpeed;

    //JumpVariables
    public bool grounded;
    public Transform footPoint1;
    public Transform footPoint2;
    public LayerMask onlyGroundMask;
    public float jumpForce;

    private float timer;
    private bool canJump;
    private float maxTime = 0.1f;


    private float previousAxispos;
    public float scaleX, scaleY;

    public bool canMove;


    public void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        scaleX = this.transform.localScale.x;
        scaleY = this.transform.localScale.y;
        // anim.SetBool("Grounded", true);
    }

    public void Update()
    {
        if (selected && canMove)
        {
            Move();
            Jump();
        }
    }

    public void Move()
    {
        if (!grounded && Input.GetAxis("Horizontal") != previousAxispos)
        {
            BaseSpeed = 0;
            outsideForce = false;
        }
        previousAxispos = Input.GetAxis("Horizontal");

        //MOVING CODE

        //anim.SetFloat("velocityY", rb.velocity.y);
        if (!outsideForce && hanging == false)
        {

            if (Input.GetAxis("Horizontal") != 0)
            {
<<<<<<< HEAD
                anim.SetBool("Walking", true);
                rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed * Mathf.Sign(scaleY) + BaseSpeed, rb.velocity.y);
                transform.localScale = new Vector2(Mathf.Sign(Input.GetAxis("Horizontal")) * scaleX * Mathf.Sign(scaleY), scaleY);
=======
                //anim.SetBool("Moving", true);
                rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed * Mathf.Sign(scaleY) + BaseSpeed, rb.velocity.y);
                transform.localScale = new Vector2(Input.GetAxisRaw("Horizontal") * scaleX * Mathf.Sign(scaleY), scaleY);
>>>>>>> d698f11947f3f1f9edf01d9f0209a10bef2006a8
            }
            else
            {
                //anim.SetBool("Moving", false);
                rb.velocity = new Vector2(BaseSpeed, rb.velocity.y);
            }
        }

    }

    public void Jump()
    {
        //JUMPCODE
        grounded = Physics2D.OverlapArea(footPoint1.position, footPoint2.position, onlyGroundMask);


        //anim.SetBool("Grounded", grounded);

        if ((grounded || hanging) && Input.GetButtonDown("Jump"))
        {
            hanging = false;

            GetComponent<Rigidbody2D>().isKinematic = false;
            timer = 0;
            canJump = true;
            rb.AddForce(new Vector2(0, jumpForce * 3 * Mathf.Sign(scaleY)));
        }
    }


    public void FixedUpdate()
    {
        if (selected && canMove)
        {
            if (grounded && Input.GetButtonDown("Jump"))
            {

            }
            else if (Input.GetButtonDown("Jump") && canJump && timer < maxTime)
            {
                timer += Time.deltaTime;
                rb.AddForce(new Vector2(0, jumpForce * Mathf.Sign(scaleY)));
            }
            else
            {
                canJump = false;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        outsideForce = false;
        if(other.gameObject.tag != "Stick")
        {
            canMove = true;
        }

        if (other.gameObject.tag == "Platform")
            BaseSpeed = other.gameObject.GetComponent<Rigidbody2D>().velocity.x;
    }
}
