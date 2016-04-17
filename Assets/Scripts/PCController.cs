using UnityEngine;
using System.Collections;

public class PCController : MonoBehaviour {

	public float forceFactor = 15f;
	public float torqueFactor = 8f;
	public float maxFallingSpeed = 2f;
	public float floatFactor = 1.2f;
	private Rigidbody rb;
	public Material playerMat;
	private GameManager gm;
	private Color rainbowColor;
	private bool beatLastFrame;
	public GameObject icoso;
	public GameObject cone;
	
	void Awake () 
	{
		rb = GetComponent<Rigidbody>();
		ShiftToShape(icoso);
//		playerMat = transform.Find("Icoso").GetComponent<MeshRenderer>().material;
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
		// choose a random color to change to each cycle. 
		if (gm.oscValue >= 0.98f && beatLastFrame == false)
		{
			rainbowColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
			rb.AddForce(Vector3.up * 0.6f, ForceMode.Impulse);
			beatLastFrame = true;
		} 
		else
			beatLastFrame = false;

		float colorMult = (gm.oscValue + 1f) * 0.5f;

		playerMat.SetColor( "_EmissionColor", rainbowColor * colorMult);


//		transform.localScale = Vector3.one * 0.5f + (Vector3.one * gm.oscValue * 0.005f) ;

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
			return true;
		}
		return false;
	}

	void Reform()
	{
		ShiftToShape(icoso);
	}

	void Flatten()
	{
		ShiftToShape(cone);
		Reorient();
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
		icoso.SetActive(false);
		cone.SetActive(false);

		newShape.SetActive(true);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Death")
		{
			StopVelocity();
			Reorient();
			gm.ResetPlayerCharacter();
			gm.ResetAllUrchin();
		}
	}


}
