using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

	GameManager gm;
	public Vector3 slowRotation = new Vector3(0f, 1f, 0f);
	private Vector3 rootPos;
	public float oscHeight = 0.12f;
	
	void Awake () 
	{
		gm = GameObject.Find("GameManager").GetComponent<GameManager>();
		rootPos = transform.position;
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.tag != "Player") return;

		gm.LevelCompleted(transform.parent.gameObject);
	}

	void Update ()
	{
		transform.localRotation *= Quaternion.Euler(slowRotation);

		float yMod = gm.oscValue * oscHeight;
		transform.position = new Vector3(rootPos.x, rootPos.y + yMod, rootPos.z);


	}
}
