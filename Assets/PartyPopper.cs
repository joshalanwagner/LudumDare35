using UnityEngine;
using System.Collections;

public class PartyPopper : MonoBehaviour {

	private GameManager gm;
	private Vector3 rootPos;
	public float moveLength = 0.65f;
	public float downSpeed = 0.01f;

	void Start () 
	{
		rootPos = transform.position;
		gm = GameObject.Find("GameManager").GetComponent<GameManager>();
	
	}
	
	void Update () 
	{
		float absOsc = Mathf.Abs(gm.oscValue);
		// need it to pop up quickly (1 frame) and move down slowly.
		if (absOsc > 0.9f)
		{
			transform.position = Vector3.up * absOsc * moveLength + rootPos;
		} else
		{
			transform.position += Vector3.down * downSpeed;
		}

			   
	}
}
