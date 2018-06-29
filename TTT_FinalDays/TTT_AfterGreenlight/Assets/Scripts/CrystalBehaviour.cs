using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalBehaviour : MonoBehaviour {

	public bool noGlow = false;
	public GameObject winColoration;
	public float yOffset = 0.5f;
	public float yOffsetSpeed = 2f;
	public float alphaBlendSpeed = 1.3f;
	public int alphaRange = 40;
    public string collectSound;

	private Material winMaterial;
	private Material alphaMaterial;
	private Vector3 standardPosition;
	private SpriteRenderer glowRenderer;
	private Color standardGlowColor;
    private ParticleSystem particle;

	private float elapsedTime = 0;
	private bool isCollected = false;

	//BUILT-IN FUNCTIONS===================================================================================================================

	void Start () {
		standardPosition = transform.position;
		if (!noGlow) {
			glowRenderer = transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ();
			standardGlowColor = glowRenderer.color;
			particle = transform.GetChild (1).gameObject.GetComponent<ParticleSystem> ();
		}

		winMaterial = new Material(winColoration.GetComponent<MeshRenderer> ().material);
		alphaMaterial = new Material (winMaterial);
		Color nColor = alphaMaterial.color;
		nColor.a = 0;
		alphaMaterial.color = nColor;
		winColoration.GetComponent<MeshRenderer> ().material = alphaMaterial;
	}

	void Update () {
		elapsedTime += Time.deltaTime;

		//Preparing variables
		Vector3 addPos = new Vector3 (0, Mathf.Sin (elapsedTime * yOffsetSpeed) * yOffset, 0);
		Color nColor = standardGlowColor;
		nColor.a = (Mathf.Sin (elapsedTime * alphaBlendSpeed)) + alphaRange;

		//Adding offsets
		transform.position = standardPosition + addPos;
		if (!noGlow) {
			glowRenderer.color = nColor;
		}

		//Screen Color fade
		if (isCollected && !noGlow) {
			winColoration.GetComponent<Renderer> ().material.Lerp (winColoration.GetComponent<Renderer> ().material, winMaterial, Time.deltaTime * alphaBlendSpeed * 1.8f);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if ((isPlayerTag (other.gameObject.tag)) && (!isCollected) && (!noGlow)) {
            SoundManager.instance.PlaySound(collectSound);
			glowRenderer.enabled = false;
			GetComponent<Renderer> ().enabled = false;
            particle.enableEmission = false;
			GameObject.Find("Players").GetComponent<CheckWinState> ().addCrystal ();
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
