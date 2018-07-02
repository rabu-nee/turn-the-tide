using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelection : MonoBehaviour
{
	public bool boyFirst = false;  //<-- Changes who is controlled first, screen orientation is seperate
    public GameObject player1, player2;

	private LevelRotation lr;
	private int lastScreen;

    // Use this for initialization
    void Start()
    {
        //player1 = GameObject.FindGameObjectWithTag("Player1");
		player1.GetComponent<Player>().selected = (boyFirst) ? true : false;

        //player2 = GameObject.FindGameObjectWithTag("Player2");
		player2.GetComponent<Player>().selected = (boyFirst) ? false : true;

		lr = GameObject.FindGameObjectWithTag ("CurrentLevel").GetComponent<LevelRotation> ();
		lastScreen = lr.getCurScreen ();
    }

    // Update is called once per frame
    void Update()
    {
		//Check if screen has changed
		if (lr.getCurScreen () != lastScreen) {
			ChangeCharacter ();
			lastScreen = lr.getCurScreen ();
		}
    }

    void ChangeCharacter()
    {
		player1.GetComponent<Player>().selected = !player1.GetComponent<Player>().selected;
		player2.GetComponent<Player>().selected = !player2.GetComponent<Player>().selected;
    }

	public int getCurSelected() {
		int ret = (player1.GetComponent<Player> ().selected) ? 1 : 0;
		return ret;
	}
}