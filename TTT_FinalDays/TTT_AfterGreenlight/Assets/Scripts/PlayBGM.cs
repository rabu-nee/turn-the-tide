using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayBGM : MonoBehaviour {

	public string bgmToPlay;

	void Start () {
		SoundManager.instance.PlaySound(bgmToPlay);
		SceneManager.sceneLoaded += stopSoundOnLevelLoad;
	}

	void stopSoundOnLevelLoad(Scene scene, LoadSceneMode mode) {
		SoundManager.instance.StopSound (bgmToPlay);
		SceneManager.sceneLoaded -= stopSoundOnLevelLoad;
	}
}
