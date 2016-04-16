using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public List<GameObject> levels;
	private int maxLevelCompleted = 0;

	void Awake ()
	{
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
			return;
		}

		levels [maxLevelCompleted].SetActive (true);
	}

	private void HideAllLevels()
	{
		foreach (GameObject level in levels)
		{
			level.SetActive(false);
		}

	}
}
