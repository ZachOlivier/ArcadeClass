using UnityEngine;
using System.Collections;

public class cameraScript : MonoBehaviour {

	public GUISkin mySkin;
	
	public int totalScore;

	public Transform target;
	public float distance = 1f;
	public float offsetx = 3f;
	public float offsety = 0f;
	public float damping = 5f;

	// Update is called once per frame
	void Update ()
	{
		Vector3 wantedPosition = target.TransformPoint(offsetx, offsety, -distance);

		transform.position = Vector3.Lerp(transform.position, wantedPosition, Time.deltaTime * damping);
	}
}