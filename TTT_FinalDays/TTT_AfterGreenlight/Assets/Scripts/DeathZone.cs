using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		if (other.GetComponent<Player> () != null) {
			Player pl = other.GetComponent<Player> ();
			pl.resetPlayerPosition ();
		}
	}
}
