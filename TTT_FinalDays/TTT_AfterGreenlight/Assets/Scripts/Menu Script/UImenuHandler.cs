using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UImenuHandler : MonoBehaviour {

	private SubMenuHandler[] subMenus;
	private int curActiveMenu = 0;

	public void setCurActiveMenu(int input) {
		curActiveMenu = input;

		for (int i = 0; i < subMenus.Length; i++) {
			subMenus [i].setActivationState ((i == curActiveMenu) ? true : false);
		}

		if (curActiveMenu == 1) {
			subMenus [1].gameObject.SetActive (true);
			subMenus [2].gameObject.SetActive (false);
		}
		if (curActiveMenu == 2) {
			subMenus [1].gameObject.SetActive (false);
			subMenus [2].gameObject.SetActive (true);
		}
	}

	public int getActiveMenu() {
		return curActiveMenu;
	}

	void Start() {
		subMenus = new SubMenuHandler[transform.childCount - 1];
		for (int i = 0; i < subMenus.Length; i++) {
			subMenus [i] = transform.GetChild (i).GetComponent<SubMenuHandler> ();
			if (i > 0) {
				subMenus [i].gameObject.SetActive (false);
			}
		}

		setCurActiveMenu (0);
	}

	void Update() {
		LevelRotation lr = GameObject.FindGameObjectWithTag ("CurrentLevel").GetComponent<LevelRotation> ();

		//Press Back
		if ((Input.GetKeyDown (KeyCode.Escape)) || (Input.GetButtonDown ("B-Button"))) {
			if ((lr.isActive()) && (curActiveMenu > 0)) {
				setCurActiveMenu (0);
				//subMenus [1].gameObject.SetActive (false);
				lr.advanceScreen (1);
			}
		}
	}
}
