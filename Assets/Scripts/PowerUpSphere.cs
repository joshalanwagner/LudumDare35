using UnityEngine;
using System.Collections;

public class PowerUpSphere : MonoBehaviour 
{

	void OnTriggerEnter(Collider PC)
	{
		Debug.Log ("ShiftToSphere");
		PC.transform.parent.GetComponent<CubeController>().ShiftToSphere();

		Destroy(this.gameObject);
	}

}
