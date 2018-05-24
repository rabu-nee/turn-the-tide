using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelection : MonoBehaviour
{

    private GameObject player1, player2;

    // Use this for initialization
    void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player1");
        player1.GetComponent<Player>().selected = true;

        player2 = GameObject.FindGameObjectWithTag("Player2");
        player2.GetComponent<Player>().selected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton4) || Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.JoystickButton5) || Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeCharacter();
        }
    }

    void ChangeCharacter()
    {
        if (!player1.GetComponent<Player>().selected)
        {
            player1.GetComponent<Player>().selected = true;
            player2.GetComponent<Player>().selected = false;
        }
        else if (player1.GetComponent<Player>().selected)
        {
            player2.GetComponent<Player>().selected = true;
            player1.GetComponent<Player>().selected = false;
        }
    }
}