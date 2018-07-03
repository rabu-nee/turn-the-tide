using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SlotSelectMenuItem : AbstractMenuItem {

	public int slotNumber = 0;

	void Start() {
		fillText();
		Debug.Log ("Slot" + slotNumber.ToString() + ": " + SaveLoadHandler.instance.getLevelOnSlot(slotNumber));
	}

	public override void onPress() {
		if (base.selected) {
			//Start new game on this slot
			if (PlayerPrefs.GetInt ("MenuMode_NewGame") == 1) {
				SaveLoadHandler.instance.createNewSave (slotNumber);
				GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<MainMenuLoadScene> ().loadSlotScene (SaveLoadHandler.instance.getLevelOnSlot (slotNumber));
			}

			//Load game on slot
			if ((PlayerPrefs.GetInt ("MenuMode_NewGame") == 0) && (SaveLoadHandler.instance.getLevelOnSlot (slotNumber) >= 0)) {
				SaveLoadHandler.instance.selectSlot (slotNumber);
				GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<MainMenuLoadScene> ().loadSlotScene (SaveLoadHandler.instance.getLevelOnSlot (slotNumber));
			}
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
			float percentage = (float)curSaveLevel / (float)(SceneManager.sceneCountInBuildSettings - 3);
			Debug.Log ("Slot" + slotNumber.ToString() + ": " + curSaveLevel);
			percentage *= 100;
			int nPercentage = Mathf.RoundToInt (percentage);
			nPercentage = Mathf.Clamp (nPercentage, 0, 100);
			pText.text = nPercentage.ToString() + "%";
			//Gold Text
			if (nPercentage == 100) {
				Vector3 nScale = pText.transform.localScale;
				nScale *= 0.81f;
				pText.transform.localScale = nScale;
				pText.color = Color.yellow;
			}
		}
		dText.text = curLastDate;
	}
}
