using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SlotSelectMenuItem : AbstractMenuItem {

	public int slotNumber = 0;
	public bool newGame = true;

	public override void onPress() {
		Debug.Log (gameObject.name + ": " + selected.ToString());
		if (base.selected) {
			if (newGame) {
				//Start new game on this slot
				PlayerPrefs.SetInt ("SaveSlot" + slotNumber.ToString (), 0);
				PlayerPrefs.SetString ("SaveSlot" + slotNumber.ToString () + "_lastDate", getCurDate ());
			}

			PlayerPrefs.SetInt ("curPlayingSlot", slotNumber);
			Debug.Log (GameObject.FindGameObjectWithTag("MainCamera").name);
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MainMenuLoadScene>().loadSlotScene ();
		}
	}

	void fillText() {
		TextMesh pText = transform.GetChild (1).GetComponent<TextMesh> ();
		TextMesh dText = transform.GetChild (2).GetComponent<TextMesh> ();
		int curSaveLevel = PlayerPrefs.GetInt ("SaveSlot" + slotNumber.ToString (), -1);
		string curLastDate = PlayerPrefs.GetString ("SaveSlot" + slotNumber.ToString () + "_lastDate", "");

		if (curSaveLevel < 0) {
			pText.text = "New";
		} else {
			float percentage = curSaveLevel / (SceneManager.sceneCountInBuildSettings - 2);
			percentage *= 100;
			int nPercentage = Mathf.RoundToInt (percentage);
			pText.text = nPercentage.ToString() + "%";
		}
		dText.text = curLastDate;
	}

	string getCurDate() {
		string curDate = DateTime.Today.Month + "/" + DateTime.Today.Day + "/" + DateTime.Now.Hour + ":" + DateTime.Now.Minute;
		return curDate;
	}

	void Start() {
		//Set UI percentage, etc...
		fillText();
	}
}
