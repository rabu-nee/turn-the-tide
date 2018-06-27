using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateDoor : MonoBehaviour {

	public GameObject door;
    public string switchSound = "switch hit";
    public string doorSound = "door";

	void OnCollisionEnter2D(Collision2D other) {
		if ((other.gameObject.CompareTag ("Stick")) || (other.gameObject.CompareTag ("Player1")) || (other.gameObject.CompareTag ("Player2"))) {
            SoundManager.instance.PlaySound(switchSound);
			if (door != null) {
                SoundManager.instance.PlaySound(doorSound);
				Destroy (door);
			}
		}
	}
}
