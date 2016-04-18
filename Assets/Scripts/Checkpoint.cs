using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

	private GameManager gm;
	public Vector3 slowRotation = new Vector3(0f, 1f, 0f);
	private Vector3 rootPos;
	public float oscHeight = 0.12f;
	private float floatDist = 0.5f;
	private Light myLight;

	void Awake () 
	{
		gm = GameObject.Find("GameManager").GetComponent<GameManager>();

		float newY = transform.position.y - DistanceToGround() + floatDist;
		transform.position = new Vector3(transform.position.x,  newY, transform.position.z);
		rootPos = transform.position;
		myLight = transform.Find("Point light").GetComponent<Light>();

		
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.tag != "Player") return;

		gm.LevelCompleted(transform.parent.gameObject);

		gameObject.SetActive(false);


		Instantiate(Resources.Load("CheckpointParticles"), transform.position, transform.rotation);
	}

	void Update ()
	{
		transform.localRotation *= Quaternion.Euler(slowRotation);

		myLight.intensity = (gm.oscValue * 0.35f) + 0.65f;

		float yMod = gm.oscValue * oscHeight;
		transform.position = new Vector3(rootPos.x, rootPos.y + yMod, rootPos.z);
	}

	private float DistanceToGround()
	{
		RaycastHit hit;
		Ray downRay = new Ray(transform.position, Vector3.down);
		if (Physics.Raycast(downRay, out hit)) 
		{
			return hit.distance;
		}
		else return 0f;
	}
}
