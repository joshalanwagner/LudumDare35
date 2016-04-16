using UnityEngine;
using System.Collections;

public class Urchin : MonoBehaviour {

	public SphereCollider sc;
	public float forceFactor = 1f;
	private Rigidbody rb;
	private float distToGround;
	private UrchinTrigger ut;

	void Awake()
	{
		rb = GetComponent<Rigidbody>();
		ut = GameObject.Find ("UrchinTrigger").GetComponent<UrchinTrigger>();
		distToGround = GetComponent<SphereCollider>().radius;
	}

// if anyone besides the ground is hitting my collider, I move towards them.

	public void MoveToward(Transform otherTrans)
	{
		Vector3 moveVector = Vector3.Normalize( otherTrans.position - transform.position);

		if (Grounded())
			rb.AddForce(moveVector * forceFactor);
	}

	void FixedUpdate()
	{
		// if I'm not within urchin collider
		// and I'm grounded 
		// move towards urchin collider.
		if (!ut.urchinIsHome && Grounded())
		{
			MoveToward(ut.transform);
		}

	}

	private bool Grounded()
	{
		return Physics.Raycast(transform.position, Vector3.down, distToGround + 0.1f);
	}


}
