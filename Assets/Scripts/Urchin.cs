﻿using UnityEngine;
using System.Collections;

public class Urchin : MonoBehaviour {

	public SphereCollider sc;
	public float forceFactor = 1f;
	public float tooFarDistance = 0f;
	public float tooCloseDistance = 0f; 
	public float randomForce = 1f;

	private Rigidbody rb;
	private float distToGround;
	private UrchinTrigger ut;

	void Awake()
	{
		rb = GetComponent<Rigidbody>();
		ut = GameObject.Find ("UrchinTrigger").GetComponent<UrchinTrigger>();
		distToGround = GetComponent<SphereCollider>().radius;
	}

	void Start()
	{
		InvokeRepeating("RandomMovement", 0.25f, 0.25f);
	}

	private void RandomMovement()
	{
		rb.AddForce(Random.Range(-randomForce, randomForce), 0f, Random.Range(-randomForce, randomForce)); 
	}

// if anyone besides the ground is hitting my collider, I move towards them.

	public void MoveToward(Transform otherTrans)
	{
		Vector3 moveVector = Vector3.Normalize( otherTrans.position - transform.position);

		if (Grounded())
			rb.AddForce(moveVector * forceFactor);
	}

	public void MoveAwayFrom(Transform otherTrans)
	{
		Vector3 moveVector = Vector3.Normalize(transform.position -  otherTrans.position);
		
		if (Grounded())
			rb.AddForce(moveVector * forceFactor);
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
		else if (TooCloseToHome() && Grounded())
		{
			MoveAwayFrom(ut.transform);
		}
	}

	private bool Grounded()
	{
		return Physics.Raycast(transform.position, Vector3.down, distToGround + 0.1f);
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
}
