using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenuScript : MonoBehaviour {

	public Sprite TEX_buttonNotSelected;
	public Sprite TEX_buttonSelected;

	private bool active;
	private float standardTimeScale;
	private bool processInput = true;
	private Canvas can;

	private int curSelected = 0;
	private Image[] buttonImages;

	void Start () {
		standardTimeScale = Time.timeScale;
		can = GetComponent<Canvas> ();

		buttonImages = new Image [3];
		GameObject btnContainer = transform.Find ("PauseBTNs").gameObject;
		for (int i=0; i<btnContainer.transform.childCount; i++) {
			buttonImages [i] = btnContainer.transform.GetChild (i).GetComponent<Image>();
		}

		setMenu (false);
	}

	void Update () {
		checkPauseButton ();
		setCharacterMovement (!active);

		if (active) {
			handleInput ();
			setSelectedButton ();
		}
	}

	//###CustomFunctions###

	public void setMenu(bool state) {
		active = state;

		Time.timeScale = (!active) ? standardTimeScale : 0f;
		can.enabled = (active) ? true : false;
	}

	private void checkPauseButton() {
		if (Input.GetButtonDown ("Start")) {
			curSelected = 0;
			setMenu (!active);
		}
	}

	private void handleInput() {
		int input = -Mathf.RoundToInt(Input.GetAxisRaw ("Vertical"));
		input = Mathf.Clamp (input, -1, 1);

		if ((input != 0) && (processInput)) {
			curSelected += input;
			curSelected = Mathf.Clamp (curSelected, 0, 2);
			processInput = false;
		}

		if (input == 0) {
			processInput = true;
		}

		//Press A
		if (Input.GetButtonDown ("Jump")) {
			acceptPressed ();
		}

		//Press B
		if (Input.GetButtonDown ("B-Button")) {
			setMenu(false);
		}
	}

	private void setSelectedButton() {
		for (int i = 0; i < buttonImages.Length; i++) {
			buttonImages [i].sprite = (i == curSelected) ? TEX_buttonSelected : TEX_buttonNotSelected;
		}
	}

	private void setCharacterMovement (bool m){
		foreach (Player p in GameObject.FindObjectsOfType<Player>()) {
			p.enabled = m;
		}
	}

	private void acceptPressed() {
		switch (curSelected) {
		case 0:
			setCharacterMovement (true);
			setMenu (false);
			break;
		case 1:
			Time.timeScale = standardTimeScale;
			SoundManager.instance.stopAllSounds ();
			SoundManager.instance.PlaySound ("ambience bird");
			SceneManager.LoadScene (0);
			break;
		case 2:
			Application.Quit ();
			break;
		}
	}
}
