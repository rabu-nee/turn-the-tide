using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour {

    GameObject[] playersObjs;
    public Transform[] players;
    Selected[] selectedComponents;

    public Transform mainPlayer;

	// Use this for initialization
	void Start () {
        playersObjs = new GameObject[GameObject.FindGameObjectsWithTag("Player").Length];
        playersObjs = GameObject.FindGameObjectsWithTag("Player");
        players = new Transform[playersObjs.Length];
        selectedComponents = new Selected[playersObjs.Length];

        for (int i = 0; i < playersObjs.Length; i++)
        {
            players[i] = playersObjs[i].transform;
            selectedComponents[i] = playersObjs[i].GetComponent<Selected>();
            if (selectedComponents[i].selected)
            {
                mainPlayer = selectedComponents[i].gameObject.transform;
            }
        }

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.JoystickButton4) == true || Input.GetKey(KeyCode.Alpha1)) //e.g. XBox controller (L) or keyboard 1
        {
            Debug.Log("Pressed L or 1");
            ChangeCharacter(1);
        }
        if (Input.GetKey(KeyCode.JoystickButton5) == true || Input.GetKey(KeyCode.Alpha2)) //e.g. XBox controller (R) or keyboard 2
        {
            Debug.Log("Pressed R or 2");
            ChangeCharacter(0);
        }
    }

    void ChangeCharacter(int index)
    {
        for (int i = 0; i < selectedComponents.Length; i++)
        {
            if (i == index)
            {
                selectedComponents[i].selected = true;
                mainPlayer = selectedComponents[i].gameObject.transform;
            }
            else
            {
                selectedComponents[i].selected = false;
            }
        }
    }
}
