using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public List<GameObject> levels;
	public GameObject pc;
	public Material groundMat;

	private PCController pCController;
	public int maxLevelCompleted = 0;
	private CameraController camControl;
	private Light mainLight;

	private float oscSpeed = 6.777f; //6.8
	public float oscValue;
	private List<GameObject> urchinList = new List<GameObject>();

	private AudioSource audioSource;
	private Text bigText;
	private Text smallText;
	private Text subText;
	
	
	public AudioClip audioClip1;
	public AudioClip audioClip2;

	void Awake ()
	{
//		if (PlayerPrefs.HasKey("maxLevelCompleted"))
//		{
//			maxLevelCompleted = PlayerPrefs.GetInt("maxLevelCompleted");
//		}
//		Debug.Log ("maxLevelCompleted " + maxLevelCompleted);
		bigText = GameObject.Find("BigText").GetComponent<Text>();
		smallText = GameObject.Find("SmallText").GetComponent<Text>();
		subText = GameObject.Find("SubText").GetComponent<Text>();
		
		audioSource = GetComponent<AudioSource>();
		camControl = GameObject.Find ("Main Camera").GetComponent<CameraController>();
		pCController = pc.GetComponent<PCController>();
		mainLight = GameObject.Find("Directional Light").GetComponent<Light>();

		GetAllUrchinRefs();
		Time.timeScale = 1.0f;
	}
	
	void Start()
	{
		ClearText();
		HideAllLevels();
		SetActiveLevels ();
		SetLevelMusic();
		ResetPlayerCharacter ();
		ShowTutorialText();
	}

	void Update ()
	{
		oscValue = Mathf.Sin(Time.time * oscSpeed);

		float colorVal = oscValue * 0.03f + 0.75f;

		groundMat.color = new Color(colorVal, 0.75f, 0.8f);
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


		if (maxLevelCompleted >= levels.Count)
		{
			GameWon ();
			return;
		}

		SetActiveLevels ();
		SetLevelMusic();
		ShowTutorialText();

		// need to set player at last checkpoint location.
		if (maxLevelCompleted > 0)
		{
			ResetPlayerCharacter ();
		}
		
		if (maxLevelCompleted >= 10f)
		{
			camControl.ZoomOut();
		}
	}

	private void ShowTutorialText()
	{
		if (maxLevelCompleted == 1 ) 
		{
			bigText.text = "Shape Shifter";
			smallText.text = "Disco Labyrinth Inferno";
			subText.text = "[Arrow Keys to Move It.]";
			
		}
		else if (maxLevelCompleted == 2 ) 
			smallText.text = "Hold 'Shift' to Get Down.";
		 
		else if (maxLevelCompleted == 3 ) 
			smallText.text = "Hold 'Shift' to Float Down.";

		Invoke("ClearText", 6f);
	}

	private void SetActiveLevels ()
	{
		camControl.ZoomIn();
		
		ActivateLevel (maxLevelCompleted);
		SwitchSpotlight(maxLevelCompleted, true);

		// turn on level after
		if (maxLevelCompleted < levels.Count - 1)
		{
			ActivateLevel (maxLevelCompleted + 1);
			SwitchSpotlight(maxLevelCompleted + 1, false);
		}

		// turn on one level back 
		if (maxLevelCompleted > 0)
		{
			ActivateLevel(maxLevelCompleted - 1);
			SwitchSpotlight(maxLevelCompleted - 1, false);
		}

		// turn off two levels back, if exists
		if (maxLevelCompleted > 1)
			DeactivateLevel(maxLevelCompleted - 2);
	}

	void SetLevelMusic()
	{
		if (maxLevelCompleted == 1)
			PlayMainTheme();
		if (maxLevelCompleted == 3)
			PlayFallingTheme();
		if (maxLevelCompleted == 4)
			PlayMainTheme();
		if (maxLevelCompleted == 10)
			PlayFallingTheme();
		if (maxLevelCompleted == 11)
			PlayMainTheme();
		if (maxLevelCompleted == 15)
			PlayFallingTheme();
		if (maxLevelCompleted == 16)
			PlayFallingTheme();
		if (maxLevelCompleted == 17)
			PlayMainTheme();
		if (maxLevelCompleted == 18)
			PlayFallingTheme();
	}

	void ActivateLevel (int levelToActivate)
	{
		levels [levelToActivate].SetActive (true);
	}

	void DeactivateLevel (int levelToActivate)
	{
		levels [levelToActivate].SetActive (false);
	}

	private void SwitchSpotlight(int level, bool on)
	{
		levels[level].transform.Find("Spotlight").gameObject.SetActive(on);

	}

	public void ResetPlayerCharacter ()
	{
		// only do this if the player is too far away.
		if (maxLevelCompleted > 0)
		{
			Vector3 lastCheckpointPos = levels [maxLevelCompleted - 1].transform.Find ("Checkpoint").transform.position;

			if (Vector3.Distance(pc.transform.position, lastCheckpointPos) > 1f)
			{
				pc.transform.position = lastCheckpointPos;
			}
		}

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

	void PlayMainTheme()
	{
		audioSource.clip = audioClip1;
		audioSource.Play();
	}
	
	void PlayFallingTheme()
	{
		audioSource.clip = audioClip2;
		audioSource.Play();
	}

	void GameWon ()
	{
		Debug.Log ("Game Completed");
		maxLevelCompleted = 0;
		HideAllLevels ();

		bigText.text = "Dream Complete.";
		smallText.text = "You wake up feeling refreshed.";

		pCController.rb.useGravity = false;
		pCController.rb.velocity = Vector3.zero;
		pCController.rb.constraints = RigidbodyConstraints.FreezePosition;
		camControl.ZoomInForEnd();

		Time.timeScale = 0.5f;
//		SetActiveLevels ();
	}

	private void ClearText()
	{
		bigText.text = "";
		smallText.text = "";
		subText.text = "";
	}
}
