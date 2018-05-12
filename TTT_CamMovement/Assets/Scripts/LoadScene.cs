using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

	public void LoadCamMovScene(){
		SceneManager.LoadScene ("CamMovementScene");
	}

	public void Update() {
		if (Input.GetButton ("Jump")) {
			SceneManager.LoadScene ("CamMovementScene");
		}
	}
}
