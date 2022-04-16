using UnityEngine;
using System.Collections;

[System.Serializable]
public class PlayerController3 : MonoBehaviour {
	GameObject player;
	Rigidbody playerBody;
	public float speed = 10;
	public float jump = 10;

	void Start () {
		playerBody = GetComponent<Rigidbody>();
	}

	void Jump() {
		
	}
	
	void FixedUpdate () {

		float AxisHor = Input.GetAxis("Horizontal");
		float AxisVer = Input.GetAxis("Vertical");
		float AxisJum = Input.GetAxisRaw("Jump");

		RaycastHit jumpRayHit;
		Ray jumpRay = new Ray(transform.position, Vector3.down);
		Debug.DrawRay(transform.position, Vector3.down * 1f, Color.cyan);

		if(Physics.Raycast(jumpRay, out jumpRayHit, 1f) && jumpRayHit.collider.tag == "Envi"){
			Vector3 movement = new Vector3 (AxisHor, 0f, AxisVer);
			//playerBody.AddForce(movement * speed);

			if (Input.GetButtonDown("Jump")){
				playerBody.velocity = playerBody.velocity + new Vector3 (0f, jump, 0f);
			}
		}
	}
}
