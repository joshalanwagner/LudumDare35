using UnityEngine;
using System.Collections;

public class Spinner : MonoBehaviour {

//	private GameManager gm;
	private Rigidbody rb;
	public Vector3 torque;
//	private Vector3 rootPos;
//	private float oscHeight = 0.01f;
	
	void Awake () 
	{
//		gm = GameObject.Find("GameManager").GetComponent<GameManager>();
		rb = GetComponent<Rigidbody>();
//		rootPos = transform.position;
		
	}
	
	void FixedUpdate () 
	{
		rb.AddRelativeTorque(torque, ForceMode.Acceleration);
	}

//	void Update ()
//	{
//		float yMod = gm.oscValue * oscHeight;
//		transform.position = new Vector3(rootPos.x, rootPos.y + yMod, rootPos.z);
//	}
}
