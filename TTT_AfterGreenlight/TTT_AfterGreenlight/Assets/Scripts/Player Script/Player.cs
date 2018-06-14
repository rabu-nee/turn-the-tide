using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    [Header("Player parameters", order = 0)]
    public float speed = 5f; //move speed
    public float jumpForce;

    //some data types are public for child classes for access
    [Header("Do not touch", order = 1)]
    public bool selected;
    public bool outsideForce;
    private bool hanging = false;
    private Vector3 standardPosition;
    public bool grounded;
    public Animator anim;
    public Rigidbody2D rb;
    public float scaleX, scaleY; //mostly used for sign of scale for gravity and control
    public bool canMove;
    private float timer;
    private bool canJump;
    private float maxTime = 0.1f;
    private float previousAxispos;
    private float BaseSpeed;

    [Header("Sound names", order = 3)]
    bool hasPlayed = false;
    public string MoveSound;
    public string JumpSound;
    public string ThrowOrWallJumpSound;
    public string PushingSound;


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
        else if (!selected)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
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

                int random = (int)Random.Range(1, 6);
                if (!anim.GetBool("Walking"))
                {
                    for (int i = 1; i < 6; i++)
                    {
                        SoundManager.instance.StopSound(MoveSound + i);
                    }
                    
                }
                else if (anim.GetBool("Walking") && grounded)
                {

                    SoundManager.instance.PlaySoundDelayed(MoveSound + random, 0.3f);
                }

                rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed * Mathf.Sign(rb.gravityScale) + BaseSpeed, rb.velocity.y);
                transform.localScale = new Vector2(Mathf.Sign(Input.GetAxisRaw("Horizontal")) * Mathf.Sign(rb.gravityScale) * scaleX, scaleY);
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

        if ((grounded || hanging) && Input.GetButtonDown("Jump"))
        {
            hanging = false;

            GetComponent<Rigidbody2D>().isKinematic = false;
            timer = 0;
            canJump = true;
            SoundManager.instance.PlaySound(JumpSound);
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
        { BaseSpeed = other.gameObject.GetComponent<Rigidbody2D>().velocity.x; }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform") && anim.GetBool("Walking"))
        {
            anim.SetBool("IsPushing", true);
            if (!hasPlayed)
            {
                hasPlayed = true;
                SoundManager.instance.PlaySound(PushingSound);
            }
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        hasPlayed = false;
        anim.SetBool("IsPushing", false);
        SoundManager.instance.StopSound(PushingSound);
    }

    public void reverseGravity() {
		GetComponent<Rigidbody2D> ().gravityScale *= -1;
	}

	public void resetPlayerPosition() {
		this.transform.position = standardPosition;
		rb.velocity = Vector3.zero;
	}
}
