using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

	private Quaternion rotationIncrement;
	private GameObject lightbulb;
	private float lightOnTime;
	private float lightOffTime;
	private float timer;

	void Awake ()
	{
		rotationIncrement = Quaternion.Euler(0f, 1f, 0f);
		lightbulb = transform.GetChild(0).gameObject;

		lightOnTime = Random.Range(0.2f, .35f);
		lightOffTime = Random.Range(3f, 4.5f);
	}
	
	void Update()
	{
		transform.rotation *= rotationIncrement;

		timer += Time.deltaTime;

		if (lightbulb.activeSelf && timer > lightOnTime)
		{
			TurnLightOff ();
		}
		if (!lightbulb.activeSelf && timer > lightOffTime)
		{
			TurnLightOn ();
		}
	}

	void TurnLightOff ()
	{
		timer = 0f;
		lightbulb.SetActive (false);
	}

	void TurnLightOn ()
	{
		timer = 0f;
		lightbulb.SetActive (true);
	}
}
