using UnityEngine;
using System.Collections;

public class UrchinTrigger : MonoBehaviour {

	public Urchin urchin;
	public bool urchinIsHome;
	public bool playerIsHome;
	public GameObject player;

	void OnTriggerStay(Collider other)
	{
		if (other.tag == "Enemy")
			urchinIsHome = true;

		if (other.tag != "Player") return;

		playerIsHome = true;
		player = other.transform.parent.gameObject;
//		urchin.MoveToward(other.transform);
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Enemy")
			urchinIsHome = false;

		if (other.tag == "Player")
			playerIsHome = false;
	}
}
