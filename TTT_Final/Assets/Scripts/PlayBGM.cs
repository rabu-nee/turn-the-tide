using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayBGM : MonoBehaviour {

	public string birdAmbience;

	void Start () {
		SoundManager.instance.PlaySound(birdAmbience);
		SceneManager.sceneLoaded += stopSoundOnLevelLoad;
	}

	void stopSoundOnLevelLoad(Scene scene, LoadSceneMode mode) {
		SoundManager.instance.StopSound (birdAmbience);
		SceneManager.sceneLoaded -= stopSoundOnLevelLoad;
	}
}
