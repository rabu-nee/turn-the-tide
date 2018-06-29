using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CheckWinState : MonoBehaviour {

	public int waitBeforeTransition = 2;
	private int collectedCrystals = 0;
    public bool crystalCollected;

	public void addCrystal() {
		collectedCrystals++;
		GameObject[] totalCrystals = GameObject.FindGameObjectsWithTag ("Crystal");
		Debug.Log(totalCrystals.Length);
		Debug.Log (collectedCrystals);
		if (collectedCrystals == totalCrystals.Length) {
			//Win state reached!
			SoundManager.instance.PlaySound("victory");
            crystalCollected = true;
			//Set save slot to next level
			SaveLoadHandler.instance.addLevelToCurrentSlot(1);
            StartCoroutine(winExec(1));
		}
	}

    public bool hasReachedWinState()
    {
        return crystalCollected;
    }

	public IEnumerator winExec(int relNextSceneIndex) {
		bool t = true;
		while (t) {
			yield return new WaitForSeconds(waitBeforeTransition);

			GameObject levelContainer = GameObject.FindGameObjectWithTag("CurrentLevel");
			levelContainer.GetComponent<LevelRotation>().enabled = false;
			levelContainer.GetComponent<SceneTransition>().enabled = true;
			levelContainer.GetComponent<SceneTransition> ().setStandardVariables ();
			levelContainer.GetComponent<SceneTransition> ().setExitVariables (SceneManager.GetActiveScene().buildIndex + 1);


			t = false;
		}

	}
}
