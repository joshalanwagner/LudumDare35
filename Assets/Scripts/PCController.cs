using UnityEngine;
using System.Collections;

public class PCController : MonoBehaviour {

	public float forceFactor = 15f;
	public float torqueFactor = 8f;
//	public float jumpFactor = 7f;
	public float flatGravFactor = 0.5f;
	private enum State {Normal, Flat, Tall};
	private State state = State.Normal;
	private Rigidbody rb;

	public GameObject cube;
	public GameObject icoso;
	public GameObject cone;
	
	void Awake () 
	{
		rb = GetComponent<Rigidbody>();
		ShiftToShape(icoso);
	}
	
	void FixedUpdate () 
	{
		float vertical = Input.GetAxis("Vertical");
		float horizontal = Input.GetAxis("Horizontal");

		if (icoso.activeSelf)
			rb.AddTorque(vertical * torqueFactor, 0f, -horizontal * torqueFactor);

		rb.AddForce(horizontal * forceFactor, 0f, vertical * forceFactor);

		if (cone.activeSelf) // && IsFallingTooFast();
			rb.AddForce(Physics.gravity * rb.mass * -flatGravFactor);
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftShift) ||
		    Input.GetKeyDown(KeyCode.RightShift))
			Flatten();
		if (Input.GetKeyUp(KeyCode.LeftShift) ||
		    Input.GetKeyUp(KeyCode.RightShift))
			Reform();
	}

	void Reform()
	{
		Time.timeScale = 1.0f;
		ShiftToShape(icoso);
//		rb.constraints = RigidbodyConstraints.None;
	}

	void Flatten()
	{
		Time.timeScale = 0.5f;
		ShiftToShape(cone);
		Reorient();
//		rb.constraints = RigidbodyConstraints.FreezeRotation;
	}

	void Reorient()
	{
		rb.angularVelocity = Vector3.zero;
		transform.rotation = Quaternion.identity;
	}

	public void ShiftToShape(GameObject newShape)
	{
		// set all shapes inactive
		icoso.SetActive(false);
		cube.SetActive(false);
		cone.SetActive(false);

		newShape.SetActive(true);
	}

	//	void JumpUp()
	//	{
	//		rb.AddForce(Vector3.up * jumpFactor, ForceMode.Impulse);
	//	}


}
