using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorArrowScript : MonoBehaviour {

	public float bounceAmount = 1f;
	public float bounceSpeed = 1f;
	public float bounceTime = 2f;
	public float fadeTime = 0.4f;
	[HideInInspector]
	public Transform follow;
	[HideInInspector]
	public float arrowOffset = 0;

	private Vector3 standardPosition;
	private float elapedTime =0;
	private SpriteRenderer sprite;


	//BUILT-IN FUNCTIONS===================================================================================================================
	void Start () {
		standardPosition = follow.position;
		standardPosition.z = -5;
		sprite = GetComponent<SpriteRenderer> ();
	}

	void Update () {
		elapedTime += Time.deltaTime;

		//Update Position
		standardPosition = follow.position;
		standardPosition.z = -5;

		//Bounce
		float nY = (standardPosition.y + arrowOffset) + ((Mathf.Sin (elapedTime * bounceSpeed) * bounceAmount));
		Vector3 nPos = new Vector3 (standardPosition.x, nY, standardPosition.z);
		transform.position = nPos;

		//Sprite Fading
		if (elapedTime <= fadeTime) {
			spriteFadeIn ();
		}
		if (elapedTime >= (bounceTime - fadeTime)) {
			spriteFadeOut ();
		}

		//Delete Object
		if (elapedTime > bounceTime) {
			GameObject.Destroy (this.gameObject);
		}
	}

	//CUSTOM FUNCTIONS===================================================================================================================

	void spriteFadeIn() {
		sprite.color = new Color(1,1,1,Mathf.SmoothStep(0,1,elapedTime*(2+fadeTime)));
	}

	void spriteFadeOut() {
		sprite.color = new Color(1,1,1,Mathf.SmoothStep(1,0,(elapedTime-(bounceTime-fadeTime))));
	}
}
