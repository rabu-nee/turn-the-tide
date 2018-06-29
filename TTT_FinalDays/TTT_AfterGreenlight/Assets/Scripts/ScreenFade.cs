using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ScreenFade : MonoBehaviour {

	private Material fadeMat;
	public float fadeInSpeed = 0.2f;
	public float fadeOutSpeed = 0.42f;
	public float fadeOutAtProgress = 0.79f;
	private int fadeDirection = -1;
	public Animator anim;

	void Start() {
		fadeMat = transform.GetChild (0).GetComponent<Renderer> ().material;
	}

	void Update() {
		//Check animation Time
		if (anim != null) {
			AnimatorStateInfo currentState = anim.GetCurrentAnimatorStateInfo (0);
			float animationProgress = currentState.normalizedTime;
			if (animationProgress >= fadeOutAtProgress) {
				SoundManager.instance.FadeSound ("intro", 0.009f);
				fadeDirection = 1;
			}
			if (animationProgress >= 0.98f) {
				SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
			}
		}

		fadeScreen (fadeDirection);
	}

	public void fadeScreen(int fadeDir) {
		Color nColor = fadeMat.color;
		nColor.a = Mathf.Lerp (nColor.a, Mathf.Clamp (fadeDir, 0, 1), Time.deltaTime * ((fadeDir < 0) ? fadeInSpeed : fadeOutSpeed));
		fadeMat.color = nColor;
	}
}
