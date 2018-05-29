using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelection : MonoBehaviour
{

    private GameObject player1, player2;
	private LevelRotation lr;
	private int lastScreen;

    // Use this for initialization
    void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player1");
        player1.GetComponent<Player>().selected = true;

        player2 = GameObject.FindGameObjectWithTag("Player2");
        player2.GetComponent<Player>().selected = false;

		lr = GameObject.FindGameObjectWithTag ("CurrentLevel").GetComponent<LevelRotation> ();
		lastScreen = lr.getCurScreen ();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeCharacter();
		if (lr.getCurScreen () != lastScreen) {
			lastScreen = lr.getCurScreen ();
			reversePlayerGravity ();
		}
    }

    void ChangeCharacter()
    {
		player1.GetComponent<Player>().selected = screenToBool(lr.getCurScreen());
		player2.GetComponent<Player>().selected = !screenToBool(lr.getCurScreen());

    }

	void reversePlayerGravity() {
		player1.GetComponent<Player> ().reverseGravity ();
		player2.GetComponent<Player> ().reverseGravity ();
	}

	private bool screenToBool(int input) {
		if (input == 1) {
			return true;
		} else {
			return false;
		}
	}
}