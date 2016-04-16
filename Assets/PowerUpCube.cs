using UnityEngine;
using System.Collections;

public class PowerUpCube : MonoBehaviour {

	void OnTriggerEnter(Collider PC)
	{
		Debug.Log ("ShiftToCube");
		PC.transform.parent.GetComponent<CubeController>().ShiftToCube();
		
		Destroy(this.gameObject);
	}
}
