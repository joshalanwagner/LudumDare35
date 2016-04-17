using UnityEngine;
using System.Collections;

public class Urchin : MonoBehaviour {

	public UrchinTrigger ut;
//	private SphereCollider sc;
	public float forceFactor = 1f;
	public float tooFarDistance = 0f;
	public float tooCloseDistance = 0f; 
	public float randomForce = 1f;

	[System.NonSerialized]
	public Vector3 startingPos;

	private Rigidbody rb;
	private float distToGround;

	// urchin needs to be reset if it falls.

	void Awake()
	{
		startingPos = transform.position;
		rb = GetComponent<Rigidbody>();
//		ut = GameObject.Find ("UrchinTrigger").GetComponent<UrchinTrigger>();
		distToGround = GetComponent<SphereCollider>().radius;
//		sc = ut.GetComponent<SphereCollider>();
	}

	void Start()
	{
		InvokeRepeating("RandomMovement", 0.25f, 0.25f);
	}

	private void RandomMovement()
	{
		rb.AddForce(Random.Range(-randomForce, randomForce), 0f, Random.Range(-randomForce, randomForce)); 
	}

	void FixedUpdate()
	{
		if (ut.playerIsHome && Grounded())
		{
			MoveToward(ut.player.transform);
		}
		else if (TooFarFromHome() && Grounded())
		{
			MoveToward(ut.transform);
		}
//		else if (TooCloseToHome() && Grounded())
		else if (TooCloseToHome())
		{
			Debug.Log ("Too Close!");
			MoveAwayFrom(ut.transform);
		}
	}

// if anyone besides the ground is hitting my collider, I move towards them.

	public void MoveToward(Transform otherTrans)
	{
		Vector3 moveVector = Vector3.Normalize(otherTrans.position - transform.position);

		if (Grounded())
			rb.AddForce(moveVector * forceFactor);
	}

	public void MoveAwayFrom(Transform otherTrans)
	{
		Vector3 moveVector = Vector3.Normalize(transform.position - otherTrans.position);
		
		if (Grounded())
			rb.AddForce(moveVector * forceFactor);
	}

	private bool Grounded()
	{
		return Physics.Raycast(transform.position, Vector3.down, distToGround + 0.25f);
	}

	bool TooFarFromHome()
	{
		if (tooFarDistance == 0f) return false;

		if (Vector3.Distance(transform.position, ut.transform.position) > tooFarDistance)
		{
			return(true);
		}
		return false;
	}

	bool TooCloseToHome()
	{
		if (tooCloseDistance == 0f) return false;

		if (Vector3.Distance(transform.position, ut.transform.position) < tooCloseDistance)
		{
			return(true);
		}
		return false;
	}

	public void ResetToStart()
	{
		rb.isKinematic = false;
		transform.position = startingPos;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Death")
		{
//			Invoke("ResetToStart", 5f);
//			rb.velocity = Vector3.zero;
//			rb.angularVelocity = Vector3.zero;
			rb.isKinematic = true;
			gameObject.SetActive(false);
		}
	}

}
