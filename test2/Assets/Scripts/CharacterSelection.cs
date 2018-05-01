using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour {

    private GameObject player1, player2;

	// Use this for initialization
	void Start () {
        player1 = GameObject.FindGameObjectWithTag("Player1");
        player1.GetComponent<PlayerMovement>().selected = true;

        CameraFollow.S.poi = player1;

        player2 = GameObject.FindGameObjectWithTag("Player2");
        player2.GetComponent<PlayerMovement>().selected = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.JoystickButton4) == true || Input.GetKeyDown(KeyCode.Alpha1)) //e.g. XBox controller (L) or keyboard 1
        {
            Debug.Log("Pressed L or 1");
            ChangeCharacter();
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton5) == true || Input.GetKeyDown(KeyCode.Alpha2)) //e.g. XBox controller (R) or keyboard 2
        {
            Debug.Log("Pressed R or 2");
            ChangeCharacter();
        }
    }

    void ChangeCharacter()
    {
        if(!player1.GetComponent<PlayerMovement>().selected)
        {
            player1.GetComponent<PlayerMovement>().selected = true;
            player2.GetComponent<PlayerMovement>().selected = false;
            CameraFollow.S.poi = player1;
        }
        else if (player1.GetComponent<PlayerMovement>().selected)
        {
            player2.GetComponent<PlayerMovement>().selected = true;
            player1.GetComponent<PlayerMovement>().selected = false;
            CameraFollow.S.poi = player2;
        }
    }
}
