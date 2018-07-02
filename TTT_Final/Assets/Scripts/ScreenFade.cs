using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFade : MonoBehaviour {

	private Material fadeMat;
	public float fadeSpeed = 0.2f;
	public int fadeDirection = -1;

	void Start() {
		fadeMat = transform.GetChild (0).GetComponent<Renderer> ().material;
	}

	void Update() {
		fadeScreen (fadeDirection);
	}

	public void fadeScreen(int fadeDir) {
		Color nColor = fadeMat.color;
		nColor.a = Mathf.Lerp (nColor.a, Mathf.Clamp (fadeDir, 0, 1), Time.deltaTime * fadeSpeed);
		fadeMat.color = nColor;
	}
}
