using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractMenuItem : MonoBehaviour, IMenuItem{
	public float highlightAlpha = 0.6f;
	[HideInInspector]
	public bool selected = false;
	[HideInInspector]
	public GameObject highlightObj;

	public virtual void onPress() {
		//Derivative classes override their functionality here
	}

	public virtual void setHighlight() {
		highlightObj = transform.GetChild (0).gameObject;
		SpriteRenderer highlightRenderer = highlightObj.GetComponent<SpriteRenderer> ();
		Color desiredColor = Color.white;
		desiredColor.a = (selected ? highlightAlpha : 0);
		highlightRenderer.color = desiredColor;
	}

	public virtual void setActivationState (bool input) {
		selected = input;
		setHighlight ();
	}
}
