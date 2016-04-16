using UnityEngine;
using System.Collections;

public class CubeController : MonoBehaviour {


	public float torqueFactor;
	private enum State {Normal, Flat, Tall};
	private State state = State.Normal;
	private Rigidbody rb;
	private Vector3 tallScale = new Vector3(0.5f, 2.0f, 0.5f); 
	private Vector3 flatScale = new Vector3(2.0f, 0.25f, 2.0f); 
	
	

	void Awake () 
	{
		rb = GetComponent<Rigidbody>();

	}
	
	void FixedUpdate () 
	{
		float vertical = Input.GetAxis("Vertical");
		float horizontal = -Input.GetAxis("Horizontal");

		rb.AddTorque(vertical * torqueFactor, 0f, horizontal * torqueFactor);
		
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Mouse0))
			GrowTaller();
		if (Input.GetKeyDown(KeyCode.Mouse1))
			GrowFlatter();
	}

	void GrowTaller()
	{
		if (state == State.Flat)
			GrowNormal();
		else if (state == State.Normal)
			GrowTall();
	}

	void GrowFlatter()
	{
		if (state == State.Tall)
			GrowNormal();
		else if (state == State.Normal)
			GrowFlat();
	}

	void GrowNormal()
	{
		transform.localScale = Vector3.one;
		state = State.Normal;
	}

	void GrowTall()
	{
		Reorient();
		transform.localScale = tallScale;
		state = State.Tall;
	}

	void GrowFlat()
	{
		Reorient();
		transform.localScale = flatScale;
		state = State.Flat;
	}

	void Reorient()
	{
		rb.angularVelocity = Vector3.zero;
		transform.rotation = Quaternion.identity;
	}
}
