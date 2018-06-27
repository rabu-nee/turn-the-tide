using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderMenuItem : AbstractMenuItem {

	public string prefName = "...Volume";
	public Transform pivot;
	public GameObject sliderNub;
	public float angleMultiplier = 0.9f;
	public int sliderIncrement = 5;
	public Vector2 valueRange;

	public int sliderValue = 0;
	private int rotatedAmount = 0;
	private bool processInput = true;

	void setNubPosition() {
		float dif = rotatedAmount - sliderValue;
		sliderNub.transform.RotateAround (pivot.position, transform.forward, dif * angleMultiplier);
		rotatedAmount = sliderValue;
	}

	public void saveValue() {
		PlayerPrefs.SetInt (prefName, sliderValue);
	}

	void Start () {
		sliderValue = PlayerPrefs.GetInt (prefName);
		highlightObj = transform.GetChild (0).gameObject;
	}

	void Update () {
		//Input new slider Value
		if (selected && processInput) {
			float input = Input.GetAxis ("Horizontal");
			if (input != 0) {
				input = ((input > 0) ? 1 : -1);
			}
			sliderValue += (sliderIncrement * (int)input);
			sliderValue = Mathf.Clamp (sliderValue, (int)valueRange.x, (int)valueRange.y);
			processInput = false;
		}

		//Save new Setting
		if (sliderValue != rotatedAmount) {
			saveValue ();
		}

		//Check for key release
		if (Input.GetAxis ("Horizontal") == 0) {
			processInput = true;
		}
			
		setNubPosition ();
	}
}
