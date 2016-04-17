using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public List<GameObject> levels;
	public GameObject pc;
	private PCController pCController;
	public int maxLevelCompleted = 0;
	private CameraController camControl;
	private Light mainLight;

	private float oscSpeed = 6.777f; //6.8
	public float oscValue;
	private List<GameObject> urchinList = new List<GameObject>();

//	public Skybox skybox;
//	public Material playerMat;

	void Awake ()
	{
//		if (PlayerPrefs.HasKey("maxLevelCompleted"))
//		{
//			maxLevelCompleted = PlayerPrefs.GetInt("maxLevelCompleted");
//		}
//		Debug.Log ("maxLevelCompleted " + maxLevelCompleted);
		camControl = GameObject.Find ("Main Camera").GetComponent<CameraController>();
		pCController = pc.GetComponent<PCController>();
		mainLight = GameObject.Find("Directional Light").GetComponent<Light>();

		GetAllUrchinRefs();
	}

	void Start()
	{
		HideAllLevels();
		ShowCurrentLevels ();
	}

	void Update ()
	{
		oscValue = Mathf.Sin(Time.time * oscSpeed);

		// might not use main light?
		mainLight.intensity = 0.5f + (oscValue * 0.25f);

//		skybox.SetColor( "_Tint", new Color(oscValue * 0.2f + 0.5f, 0.5f, -oscValue * 0.15f + 0.5f, 0.5f ) );
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
		ShowCurrentLevels ();
	}

	private void ShowCurrentLevels ()
	{
		if (maxLevelCompleted >= levels.Count)
		{
			Debug.Log ("Game Completed");
			maxLevelCompleted = 0;
			HideAllLevels();
			ShowCurrentLevels ();
			return;
		}

		camControl.ZoomIn();
		
		ActivateLevel (maxLevelCompleted);
		SwitchSpotlight(maxLevelCompleted, true);

		// level after
		if (maxLevelCompleted < levels.Count - 1)
		{
			ActivateLevel (maxLevelCompleted + 1);
			SwitchSpotlight(maxLevelCompleted + 1, false);
		}

		// need to set player at last checkpoint location.
		if (maxLevelCompleted > 0)
		{
			ResetPlayerCharacter ();
		}

		if (maxLevelCompleted == 10 || maxLevelCompleted == 11)
		{
			camControl.ZoomOut();
		}
	}

	void ActivateLevel (int levelToActivate)
	{
		levels [levelToActivate].SetActive (true);
	}

	private void SwitchSpotlight(int level, bool on)
	{
		levels[level].transform.Find("Spotlight").gameObject.SetActive(on);

	}

	public void ResetPlayerCharacter ()
	{
		pCController.StopVelocity();

		Vector3 playerPos = levels [maxLevelCompleted - 1].transform.Find ("Checkpoint").transform.position;
		pc.transform.position = playerPos;
	}

	public void ResetAllUrchin()
	{
		foreach(GameObject urchin in urchinList)
		{
			urchin.SetActive(true);
			urchin.GetComponent<Urchin>().ResetToStart();
		}
	}

	private void GetAllUrchinRefs()
	{
		foreach(GameObject go in GameObject.FindObjectsOfType(typeof(GameObject)))
		{
			if(go.name == "Urchin")
				urchinList.Add (go);
		}
	}

	private void HideAllLevels()
	{
		foreach (GameObject level in levels)
		{
			level.SetActive(false);
		}

	}
}
