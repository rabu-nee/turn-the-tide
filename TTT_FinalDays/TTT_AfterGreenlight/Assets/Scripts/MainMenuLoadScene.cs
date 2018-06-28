using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuLoadScene : MonoBehaviour {

	public float timeTilLoad = 3f;

	private Animator anim;
	private int sceneToLoad = 0;
	private bool transitionNow = false;
	private float elapsedTime = 0;

	public void loadSlotScene() {
		int nScene = PlayerPrefs.GetInt ("SaveSlot" + PlayerPrefs.GetInt ("curPlayingSlot").ToString(), -1);
		if (nScene < 1) {
			sceneToLoad = SceneManager.GetActiveScene ().buildIndex + 1;
		} else {
			sceneToLoad = nScene + 1;
		}
		anim.SetBool ("GameStart", true);
		transitionNow = true;
	}

	public bool startedTransition() {
		return transitionNow;
	}

	void Start() {
		anim = GetComponent<Animator> ();
	}

	void Update () {
		if (transitionNow) {
			elapsedTime += Time.deltaTime;
			if (elapsedTime >= timeTilLoad) {
				SceneManager.LoadScene (sceneToLoad);
			}
		}

		//MENU DEBUG
		if (Input.GetKeyDown (KeyCode.P)) {
			PlayerPrefs.DeleteAll ();
		}
	}
}
