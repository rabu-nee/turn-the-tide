using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ResetScript : MonoBehaviour {

	public static float timeToReset = 3f;
	public static float timeToDenie = 0.35f;
	public float rotationSpeed = 1f;

	private float resetTimer = 0;
	private float denieTimer = timeToDenie;
	private CheckWinState cws;
	private TextMesh tm;
	private GameObject resetSymbol;
	private LevelRotation lr;
	private bool isResetting = false;
	private bool wasActive = false;

	void Start () {
		cws = GameObject.FindGameObjectWithTag ("Player").GetComponent<CheckWinState>();
		lr = GameObject.FindGameObjectWithTag ("CurrentLevel").GetComponent<LevelRotation> ();
		tm = transform.GetChild (0).GetComponent<TextMesh> ();
		resetSymbol = transform.GetChild (1).gameObject;

		//Disable Gui Reset
		resetSymbol.transform.rotation = Quaternion.Euler (Vector3.zero);
		tm.text = "";
		resetSymbol.GetComponent<Renderer>().enabled = false;
		GetComponent<Renderer> ().enabled = false;
	}

	void Update () {
		//Reset Logic
		reset();

		//Graphical Reset
		if (isResetting) {
			tm.color = Color.black;
			resetSymbol.GetComponent<Renderer>().enabled = true;
			GetComponent<Renderer> ().enabled = true;
			Vector3 eulerRot = resetSymbol.transform.rotation.eulerAngles;
			eulerRot.z += rotationSpeed * Time.deltaTime;
			resetSymbol.transform.rotation = Quaternion.Euler (eulerRot);
			tm.text = (Mathf.RoundToInt (timeToReset) - Mathf.RoundToInt (resetTimer)).ToString ();
			wasActive = true;
		} else {
			if ((denieTimer <= timeToDenie) && (wasActive)) {
				denieTimer += Time.deltaTime;
				tm.color = Color.red;
				tm.text = "X";
			} else {
				resetSymbol.transform.rotation = Quaternion.Euler (Vector3.zero);
				tm.text = "";
				resetSymbol.GetComponent<Renderer>().enabled = false;
				GetComponent<Renderer> ().enabled = false;
			}
		}
	}

	public void reset()
	{
		if (!cws.crystalCollected)
		{
			if (Input.GetButton("Reset") && lr.enabled)
			{
				isResetting = true;
				resetTimer += Time.deltaTime;
				denieTimer = 0;

				if (resetTimer >= timeToReset)
				{
					//RESET LEVEL
					GameObject levelContainer = GameObject.FindGameObjectWithTag("CurrentLevel");
					levelContainer.GetComponent<LevelRotation>().enabled = false;
					levelContainer.GetComponent<SceneTransition>().enabled = true;
					levelContainer.GetComponent<SceneTransition> ().setStandardVariables ();
					levelContainer.GetComponent<SceneTransition> ().setExitVariables (SceneManager.GetActiveScene().buildIndex);
				}
			}
			else
			{
				isResetting = false;
				resetTimer = 0;
			}
		}
		else
		{
			resetTimer = 0;
		}
	}
}
