using UnityEngine;
using System.Collections;

public class Spinner : MonoBehaviour {

	private Rigidbody rb;
	public Vector3 torque;

	void Awake () 
	{
		rb = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate () 
	{
		rb.AddTorque(torque, ForceMode.Acceleration);
	}
}
