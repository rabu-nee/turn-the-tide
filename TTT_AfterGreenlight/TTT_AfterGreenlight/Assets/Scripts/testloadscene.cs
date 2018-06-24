using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class testloadscene : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown) {
			int nextSceneIndex = SceneManager.GetActiveScene ().buildIndex + 1;
			//SceneManager.LoadScene(nextSceneIndex);
		}
	}
}
