﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public List<GameObject> levels;
	public GameObject pc;
	private int maxLevelCompleted = 0;

	void Awake ()
	{
		if (PlayerPrefs.HasKey("maxLevelCompleted"))
		{
			maxLevelCompleted = PlayerPrefs.GetInt("maxLevelCompleted");
		}
		Debug.Log ("maxLevelCompleted " + maxLevelCompleted);
		HideAllLevels();
		ShowCurrentLevel ();
	}
	
	public void LevelCompleted(GameObject level)
	{
		for (int i = 0; i < levels.Count; i++)
		{
			if (level == levels[i])
			{
				maxLevelCompleted = i + 1;
				PlayerPrefs.SetInt("maxLevelCompleted", maxLevelCompleted);
			}
		}

		HideAllLevels();
		ShowCurrentLevel ();

	}

	void ShowCurrentLevel ()
	{
		if (maxLevelCompleted >= levels.Count)
		{
			Debug.Log ("Game Completed");
			maxLevelCompleted = 0;
			HideAllLevels();
			ShowCurrentLevel ();
			return;
		}

		levels [maxLevelCompleted].SetActive (true);
		// need to set player at last checkpoint location.
		Vector3 playerPos = levels[maxLevelCompleted - 1].transform.Find("Checkpoint").transform.position;
		pc.transform.position = playerPos;

	}

	private void HideAllLevels()
	{
		foreach (GameObject level in levels)
		{
			level.SetActive(false);
		}

	}
}
