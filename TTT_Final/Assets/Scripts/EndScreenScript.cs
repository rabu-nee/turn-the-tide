using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EndScreenScript : MonoBehaviour {

	void Update () {
		if (Input.GetButtonDown ("Reset")) {
			SoundManager.instance.stopAllSounds ();
			SceneManager.LoadScene (0);
		}
	}
}
