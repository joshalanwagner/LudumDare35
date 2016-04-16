using UnityEngine;
using System.Collections;

public class PowerUpCube : MonoBehaviour {



	void OnTriggerEnter(Collider PC)
	{
		Debug.Log ("ShiftToCube");
		PCController pCC = PC.transform.parent.GetComponent<PCController>();
		pCC.ShiftToShape(pCC.cube);
		
		Destroy(this.gameObject);
	}


}
