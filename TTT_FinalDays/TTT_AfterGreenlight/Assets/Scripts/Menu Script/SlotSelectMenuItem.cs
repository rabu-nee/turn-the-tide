using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SlotSelectMenuItem : AbstractMenuItem {

	public int slotNumber = 0;
	public bool newGame = true;

	void Start() {
		fillText();
	}

	public override void onPress() {
		if (base.selected) {
			if (newGame) {
				//Start new game on this slot
				SaveLoadHandler.instance.createNewSave (slotNumber);
			}
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MainMenuLoadScene>().loadSlotScene(SaveLoadHandler.instance.getLevelOnSlot (slotNumber));
		}
	}

	void fillText() {
		TextMesh pText = transform.GetChild (1).GetComponent<TextMesh> ();
		TextMesh dText = transform.GetChild (2).GetComponent<TextMesh> ();
		int curSaveLevel = SaveLoadHandler.instance.getLevelOnSlot (slotNumber);
		string curLastDate = SaveLoadHandler.instance.getLastDateOnSlot (slotNumber);

		if (curSaveLevel < 0) {
			pText.text = "New";
		} else {
			float percentage = (float)curSaveLevel / (float)(SceneManager.sceneCountInBuildSettings - 2);
			Debug.Log ("Slot" + slotNumber.ToString() + ": " + curSaveLevel);
			percentage *= 100;
			int nPercentage = Mathf.RoundToInt (percentage);
			nPercentage = Mathf.Clamp (nPercentage, 0, 100);
			pText.text = nPercentage.ToString() + "%";
		}
		dText.text = curLastDate;
	}
}
