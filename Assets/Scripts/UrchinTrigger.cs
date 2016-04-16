using UnityEngine;
using System.Collections;

public class UrchinTrigger : MonoBehaviour {

	public Urchin urchin;
	public bool urchinIsHome;

	void OnTriggerStay(Collider other)
	{
		if (other.tag == "Enemy")
			urchinIsHome = true;

		if (other.tag != "Player") return;
		
		urchin.MoveToward(other.transform);
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Enemy")
			urchinIsHome = false;

	}
}
