using UnityEngine;
using System.Collections;

public class TimedDestroy : MonoBehaviour {


	void Update () 
	{
	if (Time.timeSinceLevelLoad > 5f)
			Destroy(gameObject);
	}
}
