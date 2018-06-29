using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotViewMenuItem : AbstractMenuItem {

	public bool newGame = false;

	public override void onPress() {
		//Activate Slot Menu
		transform.parent.parent.GetComponent<UImenuHandler> ().setCurActiveMenu (1);

		//Set slot selection type
		PlayerPrefs.SetInt("MenuMode_NewGame", (newGame) ? 1 : 0);

		//Turn Stone to show Menu
		LevelRotation lr = GameObject.FindGameObjectWithTag ("CurrentLevel").GetComponent<LevelRotation> ();
		lr.advanceScreen (-1);
	}
}