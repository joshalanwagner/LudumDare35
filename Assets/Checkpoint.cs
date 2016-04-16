using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

	GameManager gm;

	void Awake () 
	{
		gm = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.tag != "Player") return;

		gm.LevelCompleted(transform.parent.gameObject);
	}
}
