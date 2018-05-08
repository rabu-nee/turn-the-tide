using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Vector2 position;

    bool selected;
    public float speed;
    public float maxSpeed;
    public float jumpSpeed;
	public float groundedDistance;
	public float gravity;
	public LayerMask IgnoreLayers;
	public Vector2 CastSize;
	public float castOffset = 0.7f;
	public int InvertControls = 1;

    private Animator anim;
    private Rigidbody2D rb2d;
    private SpriteRenderer[] sp;

	private bool disabledUntilContact = false;


    Selected sel;

    // Use this for initialization
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        sel = gameObject.GetComponent<Selected>();
        sp = GetComponentsInChildren<SpriteRenderer>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
		if (!disabledUntilContact) {
			Move ();
		} else {
			if (isHuggingWall ((int) Mathf.Clamp(rb2d.velocity.x,-1,1)) == true) {
				Move ();
			} else {
				Vector2 addVel = new Vector2 (0, -gravity);
				rb2d.AddForce (addVel, ForceMode2D.Force);
				if (isGrounded () == true) {
					disabledUntilContact = false;
				}
			}
		}
    }

	void Update()
	{
		if (selected) {
			checkJump ();
		}
	}

    private void Move()
    {
        selected = sel.selected;

		if (selected) {
			Vector2 addVel = new Vector2 (0, -gravity);

			if (Input.GetAxis ("Horizontal") > 0f) { //move right
				anim.SetFloat ("State", 0);
				anim.SetBool ("IsWalking", true);
				addVel.x = 1 * InvertControls * speed;

                foreach (SpriteRenderer s in sp) {
                    s.flipX = false;
                }

				if (isHuggingWall (1 * InvertControls)) {
					addVel.x = 0;
				}
			} else if (Input.GetAxis ("Horizontal") < 0f) { //move left
				anim.SetFloat ("State", 1);
				anim.SetBool ("IsWalking", true);
				addVel.x = -1 * InvertControls * speed;

                foreach (SpriteRenderer s in sp)
                {
                    s.flipX = true;
                }

                if (isHuggingWall (-1 * InvertControls)) {
					addVel.x = 0;
				}
			} else {
				anim.SetBool ("IsWalking", false);
			}

			//Apply Motion
			rb2d.AddForce (addVel, ForceMode2D.Force);

			//speed limit
			if (rb2d.velocity.x > maxSpeed) {
				rb2d.velocity = new Vector2 (maxSpeed, rb2d.velocity.y);
			}
			if (rb2d.velocity.x < -maxSpeed) {
				rb2d.velocity = new Vector2 (-maxSpeed, rb2d.velocity.y);
			}
		} else {
			anim.SetBool ("IsWalking", false);
			Vector2 addVel = new Vector2 (0, -gravity);
			rb2d.AddForce (addVel, ForceMode2D.Force);
		}
    }

	private void checkJump(){
		if (Input.GetButtonDown ("Jump") && isGrounded ()) { //jump
			if (rb2d.gravityScale >= 0) {
				rb2d.AddForce (Vector2.up * jumpSpeed, ForceMode2D.Force);
			} else if (rb2d.gravityScale < 0) {
				rb2d.AddForce (-Vector2.up * jumpSpeed, ForceMode2D.Force);
			}
		}
	}

	public bool isGrounded(){
		Vector3 nPos = transform.position;
		nPos.y -= castOffset;
		RaycastHit2D r = Physics2D.BoxCast(nPos, CastSize, 0, Vector2.down, groundedDistance, IgnoreLayers);

		if (r.collider != null) {
            anim.SetBool("Grounded", true);
			return true;
		} else {
            anim.SetBool("Grounded", false);
            return false;
		}
	}

	public bool isHuggingWall(int dir){
		Vector3 nPos = transform.position;
		nPos.x += dir * castOffset;
		RaycastHit2D r = Physics2D.BoxCast(nPos, flipV2(CastSize), 0, Vector2.right * dir, groundedDistance, IgnoreLayers);

		if (r.collider != null) {
			anim.SetBool("IsWalking", false);
			return true;
		} else {
			return false;
		}
	}

	private Vector2 flipV2(Vector2 input) {
		Vector2 res = new Vector2 (input.y, input.x);
		return res;
	}

	public Vector2[] calculateTrajectory(int stepAmount, int dir, float grav, float fric){
		//Calculate direction
		Vector2 nDirection = Vector2.up - (dir * InvertControls * Vector2.right);

		//Setup Array and calculate trajectory
		Vector2[] nTrajectory = new Vector2[stepAmount];
		nTrajectory [0] = nDirection;
		for (int i = 1; i < stepAmount; i++) {
			nTrajectory [i] = applyFriction (nTrajectory [i - 1]*1.62f, grav, fric);
		}

		return nTrajectory;
	}

	private Vector2 applyFriction(Vector2 input, float grav, float fric) {
		input.x -= fric;
		input.y += grav;

		return input;
	}

	public void disableUntilContact() {
		disabledUntilContact = true;
	}

	public bool getDisableUntilContact() {
		return disabledUntilContact;
	}
}
