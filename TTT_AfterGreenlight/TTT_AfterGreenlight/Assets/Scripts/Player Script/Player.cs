using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //some types are public for child classes for access

    public bool selected;
    public bool outsideForce;
    private bool hanging = false;
	private Vector3 standardPosition;

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
    public float scaleX, scaleY; //mostly used for sign of scale for gravity and control

    public bool canMove;


    public void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        scaleX = this.transform.localScale.x;
        scaleY = this.transform.localScale.y;

		standardPosition = transform.position;
    }

    public void Update()
    {
        if (selected && canMove)
        {
            Move();
            Jump();
        }
        else
        {
            anim.SetBool("Walking", false);
            anim.SetBool("IsWallSliding", false);
            anim.SetBool("Jumping", false);
        }
    }

    public void Move()
    {
        if (!grounded && Input.GetAxisRaw("Horizontal") != previousAxispos)
        {
            BaseSpeed = 0;
            outsideForce = false;
        }
        previousAxispos = Input.GetAxisRaw("Horizontal");

        //MOVING CODE

        //anim.SetFloat("velocityY", rb.velocity.y);
        if (!outsideForce && hanging == false)
        {

            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                anim.SetBool("Walking", true);
                rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed * Mathf.Sign(rb.gravityScale) + BaseSpeed, rb.velocity.y);
                transform.localScale = new Vector2(Mathf.Sign(Input.GetAxisRaw("Horizontal")) * Mathf.Sign(rb.gravityScale), scaleY);
            }
            else if((int)Input.GetAxisRaw("Horizontal") == 0)
            {
                anim.SetBool("Walking", false);
                rb.velocity = new Vector2(BaseSpeed, rb.velocity.y);
            }
        }
    }

    public void Jump()
    {
        //JUMPCODE
        grounded = Physics2D.OverlapArea(footPoint1.position, footPoint2.position, onlyGroundMask);

        if ((grounded || hanging) && Input.GetButtonDown("Jump"))
        {
            hanging = false;

            GetComponent<Rigidbody2D>().isKinematic = false;
            timer = 0;
            canJump = true;
            rb.AddForce(new Vector2(0, jumpForce * 3 * Mathf.Sign(rb.gravityScale)));
            anim.SetBool("Jumping", true);
        }
        else if (grounded)
        {
            anim.SetBool("Jumping", false);
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
                rb.AddForce(new Vector2(0, jumpForce * Mathf.Sign(rb.gravityScale)));
            }
            else
            {
                canJump = false;
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        outsideForce = false;
        if(other.gameObject.tag != "Stick")
        {
            canMove = true;
        }

        if (other.gameObject.tag == "Platform")
            BaseSpeed = other.gameObject.GetComponent<Rigidbody2D>().velocity.x;
    }

	public void reverseGravity() {
		GetComponent<Rigidbody2D> ().gravityScale *= -1;
	}
<<<<<<< HEAD

	public void resetPlayerPosition() {
		this.transform.position = standardPosition;
		rb.velocity = Vector3.zero;
	}
=======
>>>>>>> e7174d0a3fa3012130ce5352aaa97eb4bd0e3d2e
}
