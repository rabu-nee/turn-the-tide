using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotViewMenuItem : AbstractMenuItem {

	public override void onPress() {
		transform.parent.parent.GetComponent<UImenuHandler> ().setCurActiveMenu (1);

		LevelRotation lr = GameObject.FindGameObjectWithTag ("CurrentLevel").GetComponent<LevelRotation> ();
		lr.advanceScreen (-1);
	}
}