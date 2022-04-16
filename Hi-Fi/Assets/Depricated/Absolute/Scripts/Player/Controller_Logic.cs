using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class Controller_Logic : MonoBehaviour {
	
	Animator anim;
	Rigidbody rig;
	CapsuleCollider caps;
//	Camera cam;


	//Statistical Variables
	public bool isFocused = true;
	public int playerNumber = 1;
	float smooth = 2.0f;
	float distToGround = 0.1f;
	float jumpDist = 150.5f;

	int jumpIdleHash = Animator.StringToHash("Jump.IdleJump");
	int jumpRunHash = Animator.StringToHash("Jump.RunJump");

	//Input to string
	public string [,] controllerInput = new string [5,2] {
//----------------------------------------------|--
		{"Key Axis "   , "Key Button "},	//  | 0
		{"Joy 1 Axis " , "Joy 1 Button "},  //  | 1
		{"Joy 2 Axis " , "Joy 2 Button "},  //  | 2
		{"Joy 3 Axis " , "Joy 3 Button "},  //  | 3
		{"Joy 4 Axis " , "Joy 4 Button "}   //  | 4
//----------------------------------------------|--
//			0			|			1			|

	};

	float rightX;
	float rightY;

	// Use this for initialization
	void Start () {

	}

	void Awake()
	{
		anim = GetComponent<Animator> ();
		rig = GetComponent<Rigidbody> ();
		caps = GetComponent <CapsuleCollider> ();
//		cam = GetComponentInChildren<Camera> ();
	}

	// Update is called once per frame
	void Update () {
		rightX = Input.GetAxis (controllerInput [playerNumber, 0] + "1");
		rightY = Input.GetAxis (controllerInput [playerNumber, 0] + "2");
	}

	void FixedUpdate(){
		
		anim.SetBool ("Focused", isFocused);
		Move ();
		Turn ();
		if (IsGrounded()) {
			anim.SetBool ("Grounded", true);
			Jump ();
		}else{
			anim.SetBool ("Grounded", false);
		}
	}

	void Move(){

		if (playerNumber == 1 && (rightX == 0 && rightY == 0)) {
			anim.SetFloat ("Horizontal", Input.GetAxis (controllerInput [0, 0] + "1"));
			anim.SetFloat ("Vertical", Input.GetAxis (controllerInput [0, 0] + "2"));
		} else {
			anim.SetFloat ("Horizontal", rightX);
			anim.SetFloat ("Vertical", rightY);
		}
	}

	void Turn()
	{ 
		if ((Mathf.Abs(rightX) <= 0.15f && Mathf.Abs(rightY) <= 0.15f)&&
			IsGrounded()) {

			anim.SetFloat ("Rotation", Input.GetAxis (controllerInput [playerNumber, 0] + "4"));	

		} else {
			anim.SetFloat ("Rotation", 0f);
			if (isFocused) {
				transform.Rotate (0, Input.GetAxis (controllerInput [playerNumber, 0] + "4") * smooth, 0);
			}
		}
	}

	void Jump()
	{
		AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
		if ((playerNumber == 1 && Input.GetButtonDown (controllerInput[0,1] + "0")) ||
			Input.GetButtonDown (controllerInput [playerNumber, 1] + "0")) 
		{
			anim.SetBool ("Jump",true);	
			if (stateInfo.fullPathHash == jumpIdleHash||
				stateInfo.fullPathHash == jumpRunHash)
			{
				//print ("Jumping");
				//transform.Translate (Vector3.up * Time.deltaTime * jumpDist);
				rig.AddForce (0f,jumpDist,0f);
				/*caps.height = 1.0f;
				caps.center = new Vector3 (0f, 1.25f, 0f);*/
			}
		} 
		else 
		{
			anim.SetBool ("Jump", false);
			caps.height = 1.75f;
			caps.center = new Vector3 (0f, 0.9f, 0f);
		}
			
	}

	bool IsGrounded(){
		return true; //Physics.Raycast (transform.position, -Vector3.up, distToGround + 0.1f);

	}
}
