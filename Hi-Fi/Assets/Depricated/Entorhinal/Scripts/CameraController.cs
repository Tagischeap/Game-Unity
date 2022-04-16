using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController4 : MonoBehaviour
{


	//Private variable to store the offset distance between the player and camera
	public GameObject follower;
	private Vector3 currentPosition;
	private Vector3 offset;

	// Use this for initialization
	void Start ()
	{
		//Calculate and store the offset value by getting the distance between the player's position and camera's position.
		offset = transform.position - follower.transform.position;
	}

	// LateUpdate is called after Update each frame
	void LateUpdate ()
	{
		transform.LookAt (follower.transform);
		currentPosition = follower.transform.position + offset;
	//	transform.position = currentPosition;

		float moveHorizontal = Input.GetAxis ("P1_Axis4");
		float moveVertical = Input.GetAxis ("P1_Axis5");

		transform.RotateAround (Vector3.zero, follower.transform.position, moveHorizontal * 100 * Time.deltaTime);

	}
}