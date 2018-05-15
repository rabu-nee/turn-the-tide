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
	public Vector2 castOffset;
	public int InvertControls = 1;

	private Vector2 startPos;
    private Animator anim;
    private Rigidbody2D rb2d;
	private bool disabledUntilContact = false;
	private int turnVelocityAdded = 0;
    private Transform tf;


    Selected sel;

    // Use this for initialization
    void Start()
    {
		startPos = transform.position;
        anim = GetComponentInChildren<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        sel = gameObject.GetComponent<Selected>();
        tf = GetComponent<Transform>();
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
                tf.localScale = new Vector3(-1, tf.localScale.y, tf.localScale.z);
				anim.SetBool ("IsWalking", true);
				addVel.x = 1 * InvertControls * speed;
				if (isHuggingWall (1 * InvertControls)) {
					addVel.x = 0;
				}
			} else if (Input.GetAxis ("Horizontal") < 0f) { //move left
                tf.localScale = new Vector3(1, tf.localScale.y, tf.localScale.z);
				anim.SetBool ("IsWalking", true);
				addVel.x = -1 * InvertControls * speed;
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
			//anim.SetBool ("IsWalking", false);
			Vector2 addVel = new Vector2 (0, -gravity);
			rb2d.AddForce (addVel, ForceMode2D.Force);
		}
    }

	private void checkJump(){
		if (Input.GetButtonDown ("Jump") && isGrounded()) { //jump
			rb2d.AddForce (Vector2.up * jumpSpeed * getGravityWeight(), ForceMode2D.Force);
            anim.SetBool("IsJumping", true);
		}
        else if (rb2d.velocity.y == 0)
        {
            anim.SetBool("IsJumping", false);
        }
	}
		
     public bool isGrounded(){
		Vector3 nPos = transform.position;
		nPos.y -= castOffset.y * getGravityWeight();
		RaycastHit2D r = Physics2D.BoxCast(nPos, CastSize, 0, Vector2.down * getGravityWeight(), groundedDistance, IgnoreLayers);

		if (r.collider != null) {
			turnVelocityAdded = 0;
            anim.SetBool("IsJumping", false);
            return true;
		} else {
			Debug.Log ("nix");
			return false;
		}
	}
   


	public bool isHuggingWall(int dir){
		Vector3 nPos = transform.position;
		nPos.x += dir * castOffset.x;
		RaycastHit2D r = Physics2D.BoxCast(nPos, flipV2(CastSize), 0, Vector2.right * dir, groundedDistance, IgnoreLayers);

		if (r.collider != null) {
			anim.SetBool("IsWalking", false);
			Debug.Log (r.collider.gameObject.name);
			return true;
		} else {
			return false;
		}
	}

	public void setToStartPosition() {
		rb2d.velocity = Vector2.zero;
		transform.position = startPos;
	}

	public int getGravityWeight(){
		return (int)Mathf.Clamp (gravity, -1, 1);
	}

	private Vector2 flipV2(Vector2 input) {
		Vector2 res = new Vector2 (input.y, input.x);
		return res;
	}

	public void disableUntilContact() {
		disabledUntilContact = true;
	}

	public bool getDisableUntilContact() {
		return disabledUntilContact;
	}

	public void addTurn() {
		turnVelocityAdded++;
	}

	public int getTurns() {
		return turnVelocityAdded;
	}
}
