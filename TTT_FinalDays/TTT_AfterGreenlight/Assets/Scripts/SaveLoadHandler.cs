using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SaveLoadHandler : MonoBehaviour {

	public static SaveLoadHandler instance;

	private void Awake()
	{
		if (instance != null)
		{
			if (instance != this)
			{
				Destroy(this.gameObject);
			}
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(this);
		}
	}

	public void createNewSave(int slot) {
		PlayerPrefs.SetInt ("SaveSlot" + slot.ToString (), 0);
		PlayerPrefs.SetString ("SaveSlot" + slot.ToString () + "_lastDate", getCurDate ());
		PlayerPrefs.SetInt ("curPlayingSlot", slot);
	}

	public void selectSlot(int slot) {
		PlayerPrefs.SetInt ("curPlayingSlot", slot);
	}

	public void addLevelToCurrentSlot(int addLevel) {
		int curSlot = PlayerPrefs.GetInt ("curPlayingSlot");
		int levelOnSlot = PlayerPrefs.GetInt ("SaveSlot" + curSlot.ToString ());
		levelOnSlot += addLevel;
		PlayerPrefs.SetInt ("SaveSlot" + curSlot.ToString (), levelOnSlot);
	}

	public int getLevelOnSlot(int slot) {
		return PlayerPrefs.GetInt ("SaveSlot" + slot.ToString (), -1);
	}

	public string getLastDateOnSlot(int slot) {
		return PlayerPrefs.GetString ("SaveSlot" + slot.ToString () + "_lastDate", "");
	}

	string getCurDate() {
		string curDate = DateTime.Today.Month + "/" + DateTime.Today.Day + "/" + DateTime.Now.Hour + ":" + DateTime.Now.Minute;
		return curDate;
	}
}
