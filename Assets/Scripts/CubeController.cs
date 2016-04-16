using UnityEngine;
using System.Collections;

public class CubeController : MonoBehaviour {

	public float forceFactor = 15f;
	public float torqueFactor = 8f;
	public float jumpFactor = 7f;
	public float flatGravFactor = 0.5f;
	private enum State {Normal, Flat, Tall};
	private State state = State.Normal;
	private Rigidbody rb;
	private Vector3 normalScale = new Vector3(0.5f, 0.5f, 0.5f); 
	private Vector3 tallScale = new Vector3(0.25f, 1.0f, 0.25f); 
	private Vector3 flatScale = new Vector3(1.0f, 0.25f, 1.0f); 

	public GameObject cube;
	public GameObject sphere;
	

	// Try adjusting gravity instead? except that would affect the gravity of everything.
	// Gravity scale feels too big. Trying a smaller size...

	void Awake () 
	{
		rb = GetComponent<Rigidbody>();
		ShiftToCube();
	}
	
	void FixedUpdate () 
	{
		float vertical = Input.GetAxis("Vertical");
		float horizontal = Input.GetAxis("Horizontal");

		if (state == State.Normal)
			rb.AddTorque(vertical * torqueFactor, 0f, -horizontal * torqueFactor);

		rb.AddForce(horizontal * forceFactor, 0f, vertical * forceFactor);

		if (state == State.Flat)
			rb.AddForce(Physics.gravity * rb.mass * -flatGravFactor);
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Mouse0) ||
		    Input.GetKeyDown(KeyCode.Tab))
			GrowTaller();
		if (Input.GetKeyDown(KeyCode.Mouse1) ||
		    Input.GetKeyDown(KeyCode.LeftShift))
			GrowFlatter();
	}

	void JumpUp()
	{
		rb.AddForce(Vector3.up * jumpFactor, ForceMode.Impulse);
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
		transform.localScale = normalScale;
		state = State.Normal;
	}

	void GrowTall()
	{
		JumpUp(); // check if grounded?
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

	public void ShiftToSphere()
	{
		cube.SetActive(false);
		sphere.SetActive(true);
		
	}

	public void ShiftToCube()
	{
		sphere.SetActive(false);
		cube.SetActive(true);
	}


}
