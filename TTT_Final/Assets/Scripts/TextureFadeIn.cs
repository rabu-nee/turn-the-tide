using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureFadeIn : MonoBehaviour {

	public Material fadeMat;
	public float fadeInSpeed = 0.2f;
	public float waitUntilFade = 5.3f;

	private float elapsedTime = 0;

	void Start () {
		Color sColor = fadeMat.color;
		sColor.a = 0;
		fadeMat.color = sColor;
	}

	void Update () {
		elapsedTime += Time.deltaTime;
		if (elapsedTime >= waitUntilFade) {
			Color nColor = fadeMat.color;
			nColor.a = Mathf.Lerp (nColor.a, 1, Time.deltaTime * fadeInSpeed);
			fadeMat.color = nColor;
		}
	}
}
