using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenuItem : AbstractMenuItem {

	public override void onPress() {
		transform.parent.parent.GetComponent<UImenuHandler> ().setCurActiveMenu (2);

		LevelRotation lr = GameObject.FindGameObjectWithTag ("CurrentLevel").GetComponent<LevelRotation> ();
		lr.advanceScreen (-1);
	}
}
