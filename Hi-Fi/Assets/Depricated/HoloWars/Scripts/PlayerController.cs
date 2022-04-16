using UnityEngine;
using System.Collections;

public class PlayerController4 : MonoBehaviour {

	public float speed = 2.5f;
	bool isLeft;
	bool isRight;
	bool isDown;
	bool isUp;

	void FixedUpdate()
	{
		float inputX = Input.GetAxis ("Horizontal");
		float inputY = Input.GetAxis ("Vertical");
		//Rigidbody2D.velocity = new Vector2 (inputX * speed, inputY * speed); 

		//if(inputX > 0 &&!isRight)
		//else if(inputX < 0 && !isLeft)

	}
}
