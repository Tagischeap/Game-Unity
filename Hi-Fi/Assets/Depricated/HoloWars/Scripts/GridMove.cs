using UnityEngine;
using System.Collections;

public class GridMove : MonoBehaviour
{
	public bool userInput = true;
	public bool canMove = true;

	public float wSpeed = 3f;					// Walk Speed
	public float sSpeed = 6f;					// Sprint Speed
	float cSpeed = 3f;							// Speed of movement
    Animator anim;

	Vector3 thisPosition;						//For Movement
	Vector3 targPosition;						//For RayCast
	Vector3 offsPosition;						//for Perspective
	float timePressed = 0f;
	bool axisDown = false;
	bool rayHit;

	bool faceingUp = false;
	bool faceingDown = false;
	bool faceingRight = false;
	bool facingLeft = false;

	Color rayColor;

	public int playerNumber = 1;

	public string[,] ControllerInput = new string[4, 6] {
		{"Horizontal", "Vertical", "Action", "Back", "Start", "Select"},
		{"p2_Horizontal", "p2_Vertical", "p2_Action", "p2_Back", "p2_Start", "p2_Select"},
		{"p3_Horizontal", "p3_Vertical", "p3_Action", "p3_Back", "p3_Start", "p3_Select"},
		{"p4_Horizontal", "p4_Vertical", "p4_Action", "p4_Back", "p4_Start", "p4_Select"}
	};

    void Start()
    {
		thisPosition = transform.position;
		anim = GetComponent<Animator> ();
	}

	void wake()
	{
	}

	public void LookUp (){
		anim.SetFloat ("LastMoveY", 1f);
		anim.SetFloat ("LastMoveX", 0f);

		faceingUp = true;
		faceingDown = false;
		faceingRight = false;
		facingLeft = false;
	}
	public void LookDown (){
		anim.SetFloat ("LastMoveY", -1f);
		anim.SetFloat ("LastMoveX", 0f);
		
		faceingUp = false;
		faceingDown = true;
		faceingRight = false;
		facingLeft = false;
	}
	public void LookRight(){
		anim.SetFloat ("LastMoveX", 1f);
		anim.SetFloat ("LastMoveY", 0f);
		
		faceingUp = false;
		faceingDown = false;
		faceingRight = true;
		facingLeft = false;
	}
	public void LookLeft (){
		anim.SetFloat ("LastMoveX", -1f);
		anim.SetFloat ("LastMoveY", 0f);
		
		faceingUp = false;
		faceingDown = false;
		faceingRight = false;
		facingLeft = true;
	}

	public void Move(Vector3 dir){
		transform.position = Vector3.MoveTowards(transform.position, dir, Time.deltaTime * cSpeed);
	}
	public void MoveUp(){
		thisPosition += Vector3.up;
	}
	public void MoveDown(){
		thisPosition += Vector3.down;
	}
	public void MoveRight(){
		thisPosition += Vector3.right;
	}
	public void MoveLeft(){
		thisPosition += Vector3.left;
	}

	/*public void WalkUp(int steps){
		LookUp();
			for (int i = 1; i <= steps; i++){ 
				if (transform.position == thisPosition){
					anim.SetBool ("walking", true);
					MoveUp ();
				}
		}anim.SetBool ("walking", false);
	}*/

	void UserInput()
	{
		float lastInputX = Input.GetAxisRaw(ControllerInput[playerNumber, 0]);
		float lastInputY = Input.GetAxisRaw(ControllerInput[playerNumber, 1]);

		if (lastInputX == 0 && lastInputY == 0) 
		{
				axisDown = false;
		}

		if ((lastInputX != 0  && lastInputY == 0) || (lastInputY != 0 && lastInputX == 0)) 
		{
			RayCaster(new Vector3(lastInputX,lastInputY,0));

			if(!axisDown){
				//down
				timePressed = Time.time + 0.05f;
				axisDown = true;
			}
				anim.SetBool ("walking", true);
			
			if (Input.GetButton ("Back")) {
				cSpeed = sSpeed;
				anim.SetBool ("sprinting", true);
			} else {
				cSpeed = wSpeed;
				anim.SetBool ("sprinting", false);
			}

			if (transform.position == thisPosition){
				if (lastInputY > 0) {
					LookUp();
					if (Time.time > timePressed && !rayHit){
						MoveUp();
					}
				} else if (lastInputY < 0) {
					LookDown();
					if (Time.time > timePressed && !rayHit){
						MoveDown();
					}
				} else if (lastInputX > 0) {
					LookRight();
					if (Time.time > timePressed && !rayHit){
						MoveRight();
					}
				} else if (lastInputX < 0) {
					LookLeft();
					if (Time.time > timePressed && !rayHit){
						MoveLeft();
					}
				}
			}

		} else if (lastInputX != 0 && lastInputY != 0) {
			anim.SetBool ("walking", false);
		} else {
			anim.SetBool ("walking", false);
		}
	}

	void RayCaster(Vector3 dir){
		offsPosition = transform.position - new Vector3(0, 0.25f , 0);
		targPosition = offsPosition + dir;

		rayHit = Physics2D.Linecast (offsPosition, targPosition, 1);
		// << LayerMask.NameToLayer("Blocking")
		Debug.DrawLine(offsPosition, targPosition, rayColor);

		if (!rayHit)
			rayColor = Color.cyan;
		else
			rayColor = Color.red;
	}

	void OnCollisionEnter2D(Collision2D col)
	{

	}

	void OnCollisionStay2D(Collision2D col)
	{

	}

    void FixedUpdate()
	{
		if (canMove == true) {
			Move(thisPosition);
			if (userInput == true){
				UserInput();
			}
		}
    }

    void Update()
	{

	}
}
