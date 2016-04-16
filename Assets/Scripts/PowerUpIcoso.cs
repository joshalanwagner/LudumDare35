using UnityEngine;
using System.Collections;

public class PowerUpIcoso : MonoBehaviour 
{

	void OnTriggerEnter(Collider PC)
	{
		Debug.Log ("ShiftToSphere");
		PC.transform.parent.GetComponent<CubeController>().ShiftToIcoso();

		Destroy(this.gameObject);
	}

}
