using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubMenuHandler : MonoBehaviour {

	public bool verticalInput = false;
	private bool processInput = true;
	private IMenuItem[] items;
	private int selected = 0;

	private void handleInput() {
		//Get directional Input
		float axisInput = 0;
		if (processInput) {
			axisInput = (verticalInput ? Input.GetAxis ("Vertical") : Input.GetAxis ("Horizontal"));
			processInput = false;
		}

		int dirInput = 0;
		if (axisInput != 0) {
			dirInput = ((axisInput > 0) ? 1 : -1);
		}

		//Check for key release
		if ((Input.GetAxis ("Vertical") == 0) && (Input.GetAxis ("Horizontal") == 0)) {
			processInput = true;
		}

		//Set selected item
		selected += dirInput;
		selected = Mathf.Clamp (selected, 0, transform.childCount - 1);
		setMenuItemActivationState (selected);

	}

	private void setMenuItemActivationState(int index) {
		for (int i = 0; i < transform.childCount; i++) {
			bool state = ((i == index) ? true : false);
			items [i].setActivationState (state);
		}
	}

	void Start () {
		//Initialize Items
		items = new IMenuItem[transform.childCount];
		for (int i = 0; i < transform.childCount; i++) {
			items [i] = transform.GetChild (i).GetComponent<AbstractMenuItem> ();
		}
	}

	void Update () {
		handleInput ();
	}
}
