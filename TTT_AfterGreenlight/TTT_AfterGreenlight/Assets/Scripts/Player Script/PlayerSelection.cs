using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelection : MonoBehaviour
{

    private GameObject player1, player2;
	private LevelRotation lr;
<<<<<<< HEAD
=======
	private int lastScreen;
>>>>>>> e7174d0a3fa3012130ce5352aaa97eb4bd0e3d2e

    // Use this for initialization
    void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player1");
        player1.GetComponent<Player>().selected = true;

        player2 = GameObject.FindGameObjectWithTag("Player2");
        player2.GetComponent<Player>().selected = false;

		lr = GameObject.FindGameObjectWithTag ("CurrentLevel").GetComponent<LevelRotation> ();
<<<<<<< HEAD
=======
		lastScreen = lr.getCurScreen ();
>>>>>>> e7174d0a3fa3012130ce5352aaa97eb4bd0e3d2e
    }

    // Update is called once per frame
    void Update()
    {
        ChangeCharacter();
<<<<<<< HEAD
=======
		if (lr.getCurScreen () != lastScreen) {
			lastScreen = lr.getCurScreen ();
			reversePlayerGravity ();
		}
>>>>>>> e7174d0a3fa3012130ce5352aaa97eb4bd0e3d2e
    }

    void ChangeCharacter()
    {
<<<<<<< HEAD
		player1.GetComponent<Player>().selected = !screenToBool(lr.getCurScreen());
		player2.GetComponent<Player>().selected = screenToBool(lr.getCurScreen());
    }

=======
		player1.GetComponent<Player>().selected = screenToBool(lr.getCurScreen());
		player2.GetComponent<Player>().selected = !screenToBool(lr.getCurScreen());

    }

	void reversePlayerGravity() {
		player1.GetComponent<Player> ().reverseGravity ();
		player2.GetComponent<Player> ().reverseGravity ();
	}

>>>>>>> e7174d0a3fa3012130ce5352aaa97eb4bd0e3d2e
	private bool screenToBool(int input) {
		if (input == 1) {
			return true;
		} else {
			return false;
		}
	}
}