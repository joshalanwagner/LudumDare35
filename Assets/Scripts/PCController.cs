using UnityEngine;
using System.Collections;

public class PCController : MonoBehaviour {

	public float forceFactor = 15f;
	public float torqueFactor = 8f;
	public float maxFallingSpeed = 2f;
//	public float jumpFactor = 7f;
	public float floatFactor = 1.2f;
	private enum State {Normal, Flat, Tall};
	private State state = State.Normal;
	private Rigidbody rb;
	private Material playerMat;
	private GameManager gm;

	public GameObject cube;
	public GameObject icoso;
	public GameObject cone;
	
	void Awake () 
	{
		rb = GetComponent<Rigidbody>();
		ShiftToShape(icoso);
		playerMat = transform.Find("Icoso").GetComponent<MeshRenderer>().material;
		gm = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	void FixedUpdate () 
	{
		float vertical = Input.GetAxis("Vertical");
		float horizontal = Input.GetAxis("Horizontal");

		if (icoso.activeSelf)
			rb.AddTorque(vertical * torqueFactor, 0f, -horizontal * torqueFactor);

		rb.AddForce(horizontal * forceFactor, 0f, vertical * forceFactor);

		if (cone.activeSelf && IsFallingTooFast())
			rb.AddForce(Physics.gravity * rb.mass * -floatFactor);
	}

	void Update()
	{
		playerMat.SetColor( "_EmissionColor", new Color(gm.oscValue * 0.1f, 0f,  0f));
		transform.localScale = Vector3.one * 0.5f + (Vector3.one * gm.oscValue * 0.01f) ;
		

		if (Input.GetKeyDown(KeyCode.LeftShift) ||
		    Input.GetKeyDown(KeyCode.RightShift))
			Flatten();
		if (Input.GetKeyUp(KeyCode.LeftShift) ||
		    Input.GetKeyUp(KeyCode.RightShift))
			Reform();
	}

	bool IsFallingTooFast()
	{
		if (rb.velocity.y < -maxFallingSpeed)
		{
			Debug.Log ("IsFallingTooFast");
			
			return true;
		}
		return false;
	}



	void Reform()
	{
		ShiftToShape(icoso);
//		rb.constraints = RigidbodyConstraints.None;
	}

	void Flatten()
	{
		ShiftToShape(cone);
		Reorient();
//		rb.constraints = RigidbodyConstraints.FreezeRotation;
	}

	private void Reorient()
	{
		rb.angularVelocity = Vector3.zero;
		transform.rotation = Quaternion.identity;
	}

	public void StopVelocity()
	{
		rb.velocity = Vector3.zero;
	}

	public void ShiftToShape(GameObject newShape)
	{
		// set all shapes inactive
		icoso.SetActive(false);
		cube.SetActive(false);
		cone.SetActive(false);

		newShape.SetActive(true);
	}

	void OnTriggerEnter(Collider other)
	{

		if (other.tag == "Death")
		{
			gm.ResetPlayerCharacter();
			gm.ResetAllUrchin();
		}
	}


}
