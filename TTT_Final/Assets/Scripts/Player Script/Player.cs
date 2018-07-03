using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    [Header("Player parameters", order = 0)]
    public float timeToReset;
    public float speed = 5f; //move speed
    public float jumpForce;
    public float footCoolDown = 0.3f;

    //some data types are public for child classes for access
    [Header("Do not touch", order = 1)]
    public bool selected;
    public bool outsideForce;
    private bool hanging = false;
    private Vector3 standardPosition;
    public bool grounded;
	public bool spawnDustCloud = false;
	public bool isHuggingWall = false;
    public Animator anim;
    public Rigidbody2D rb;
	public GameObject DustCloud;
    public float scaleX, scaleY; //mostly used for sign of scale for gravity and control
    public bool canMove;
	public bool canJump;
    private float timer;
    private float maxTime = 0.1f;
    private float previousAxispos;
    private float BaseSpeed;
    private CheckWinState cws;
    private float footTimer;

    public float raycastYOffset;
    public float distance;


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

        cws = GetComponentInParent<CheckWinState>();

		standardPosition = transform.position;
    }

    public void Update()
    {
        if (selected && canMove)
        {
            Move();
            Jump();
            Push();
			spawnDC ();
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
		if ((!outsideForce && hanging == false) && (canMove))
		{

			if (Input.GetAxisRaw("Horizontal") != 0)
			{
				anim.SetBool("Walking", true);

				if (grounded)
				{
					//FOOTSTEPS
					footSound();
					if (footTimer > 0)
					{
						footTimer -= Time.deltaTime;
					}
					if (footTimer < 0)
					{
						footTimer = 0;
					}
				}


				//FOOTSTEPS END

				//MOVE
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

	private void footSound()
	{
		if(footTimer == 0)
		{
			int random = Random.Range(1, 6);
			SoundManager.instance.PlaySound(MoveSound + random);
			footTimer = footCoolDown;
		}
	}

	private void spawnDC() {
		if (spawnDustCloud) {
			spawnDustCloud = false;
			Vector3 cloudPos = transform.position;
			if (!isHuggingWall) {
				cloudPos.y += raycastYOffset;
			}
			GameObject dc = Instantiate (DustCloud, cloudPos, transform.rotation);
			dc.GetComponent<DustCloudScript> ().setStandardValues ();
			if (isHuggingWall) {
				dc.GetComponent<DustCloudScript> ().setVelocity(new Vector2(0, rb.velocity.y/2));
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

    public void Push()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + raycastYOffset), Vector2.right * transform.localScale.x, distance);
        if(Input.GetAxisRaw("Horizontal") != 0 && grounded && hit.collider != null && hit.collider.CompareTag("Platform"))
        {
            anim.SetBool("IsPushing", true);
            if (!hasPlayed)
            {
                hasPlayed = true;
                SoundManager.instance.PlaySound(PushingSound);
            }
        }
        else
        {
			if (hasPlayed) {
				rb.velocity = new Vector2 (0, rb.velocity.y);
			}
            hasPlayed = false;
            anim.SetBool("IsPushing", false);
            SoundManager.instance.StopSound(PushingSound);
        }
    }


    public void OnCollisionEnter2D(Collision2D other)
    {
        outsideForce = false;
        if(other.gameObject.tag != "Stick")
        {
			canMove = !(anim.GetBool ("Dead"));
        }

        if (other.gameObject.tag == "Platform")
        { BaseSpeed = other.gameObject.GetComponent<Rigidbody2D>().velocity.x; }
    }

    public void reverseGravity() {
		GetComponent<Rigidbody2D> ().gravityScale *= -1;
	}

	public IEnumerator resetDeath() {

        anim.SetBool("Dead", true);
        AnimatorClipInfo[] info = anim.GetCurrentAnimatorClipInfo(0);
        yield return new WaitForSeconds(info.Length);
		anim.SetBool("Dead", false);
		canMove = true;
		canJump = true;

		this.transform.position = standardPosition;
	}

    public void resetPlayerPosition()
    {
		canMove = false;
		canJump = false;
		rb.velocity = Vector3.zero;
        StartCoroutine(resetDeath());
    }

    public void ToTitle()
    {
        
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector2(transform.position.x, transform.position.y + raycastYOffset), new Vector3(transform.position.x, transform.position.y + raycastYOffset) + Vector3.right * transform.localScale.x * distance);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Crystal"))
        {
            anim.SetBool("Dance", true);
        }
    }
}
