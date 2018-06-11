using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalBehaviour : MonoBehaviour {

	public GameObject winColoration;
	public float yOffset = 0.5f;
	public float yOffsetSpeed = 2f;
	public float alphaBlendSpeed = 1.3f;
	public int alphaRange = 40;

	private Material winMaterial;
	private Material alphaMaterial;
	private Vector2 standardPosition;
	private SpriteRenderer glowRenderer;
	private Color standardGlowColor;

	private float elapsedTime = 0;
	private bool isCollected = false;

	//BUILT-IN FUNCTIONS===================================================================================================================

	void Start () {
		standardPosition = transform.position;
		glowRenderer = transform.GetChild (0).gameObject.GetComponent<SpriteRenderer>();
		standardGlowColor = glowRenderer.color;

		winMaterial = new Material(winColoration.GetComponent<MeshRenderer> ().material);
		alphaMaterial = new Material (winMaterial);
		Color nColor = alphaMaterial.color;
		nColor.a = 0;
		alphaMaterial.color = nColor;
		winColoration.GetComponent<MeshRenderer> ().material = alphaMaterial;
	}

	void FixedUpdate () {
		elapsedTime += Time.deltaTime;

		//Preparing variables
		Vector2 addPos = new Vector2 (0, Mathf.Sin (elapsedTime * yOffsetSpeed) * yOffset);
		Color nColor = standardGlowColor;
		nColor.a = (Mathf.Sin (elapsedTime * alphaBlendSpeed)) + alphaRange;

		//Adding offsets
		transform.position = standardPosition + addPos;
		glowRenderer.color = nColor;

		//Screen Color fade
		if (isCollected) {
			winColoration.GetComponent<Renderer> ().material.Lerp (winColoration.GetComponent<Renderer> ().material, winMaterial, Time.deltaTime * alphaBlendSpeed * 1.8f);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if ((isPlayerTag (other.gameObject.tag)) && (!isCollected)) {
			glowRenderer.enabled = false;
			GetComponent<Renderer> ().enabled = false;
			isCollected = true;
		}
	}	

	//CUSTOM FUNCTIONS===================================================================================================================

	private bool isPlayerTag(string tag) {
		if ((tag == "Player1") || (tag == "Player2")) {
			return true;
		} else {
			return false;
		}
	}



}
