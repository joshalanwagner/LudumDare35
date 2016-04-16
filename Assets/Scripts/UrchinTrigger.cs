using UnityEngine;
using System.Collections;

public class UrchinTrigger : MonoBehaviour {

	public Urchin urchin;


	void OnTriggerStay(Collider other)
	{
		
		if (other.tag != "Player") return;
		
		urchin.MoveToward(other.transform);
	}
}
