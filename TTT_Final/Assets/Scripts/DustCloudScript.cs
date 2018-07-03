using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustCloudScript : MonoBehaviour {
	private Vector2 velocity;
	private float friction;
	private float elapsedTime = 0;
	private float animTime;
	private Animator anim;

	private void Start () {
		anim = GetComponent<Animator> ();
	}

	private void Update () {
		applyVelocity ();
		if (checkAnimTime ()) {
			Destroy (this.gameObject);
		}
	}

	private void applyVelocity() {
		Vector3 curPos = transform.position;
		curPos.x += (velocity.x * Time.deltaTime);
		curPos.y += (velocity.y * Time.deltaTime);
		transform.position = curPos;

		velocity.x -= (friction * Time.deltaTime);
		velocity.y -= (friction * Time.deltaTime);
	}

	private bool checkAnimTime() {
		elapsedTime += Time.deltaTime;
		if (elapsedTime >= animTime) {
			return true;
		} else {
			return false;
		}
	}

	//###Setter###
	public void setStandardValues() {
		friction = 0;
		animTime = 0.62f;
		velocity = Vector2.zero;
	}

	public void setFriction(float f) {
		friction = f;
	}

	public void setAnimTiime(float at) {
		animTime = at;
	}

	public void setVelocity(Vector2 vel) {
		velocity = vel;
	}
}
