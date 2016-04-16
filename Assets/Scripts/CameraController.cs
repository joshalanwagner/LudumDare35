using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public Transform target;
	private Vector3 offset;

	void Awake () 
	{
		offset = target.position - transform.position;
	}
	
	void LateUpdate () 
	{
		transform.position = target.position - offset;
	}
}
