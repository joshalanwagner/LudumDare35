using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public Transform target;
	public Vector3 offset;

	void Awake () 
	{
//		offset = target.position - transform.position;
		FollowTarget ();
		transform.LookAt(target);
	}
	
	void LateUpdate () 
	{
		FollowTarget ();
	}


	void FollowTarget ()
	{
		transform.position = target.position - offset;
	}
}
