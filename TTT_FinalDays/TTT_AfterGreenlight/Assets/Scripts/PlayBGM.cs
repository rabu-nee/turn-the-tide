using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBGM : MonoBehaviour {

	public string bgmToPlay;

	void Start () {
		SoundManager.instance.PlaySound(bgmToPlay);
	}
}
