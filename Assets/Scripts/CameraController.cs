using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public Transform target;
	public Vector3 offset;
	[System.NonSerialized]
	public float defaultFOV;
	public float zoomedOutFOV = 55f;
	private Camera cam;

	void Awake () 
	{
//		offset = target.position - transform.position;
		FollowTarget ();
		transform.LookAt(target);
		cam = GetComponent<Camera>();
		defaultFOV = cam.fieldOfView;
	}
	
	void LateUpdate () 
	{
		FollowTarget ();
	}


	void FollowTarget ()
	{
		transform.position = target.position - offset;
	}

	public void ZoomOut()
	{
		cam.fieldOfView = zoomedOutFOV;
	}

	public void ZoomIn()
	{
		cam.fieldOfView = defaultFOV;
	}
}
