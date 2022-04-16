using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public Vector2 gridPosition = Vector2.zero;

	public Vector3 moveDestination;
	public float moveSpeed = 10.0f;

	public string playerName = "Joe";
	public Sprite sprite;

	public bool moving = false;
	public bool attacking = false;

	public int hitPoint = 12;
	public int damage = 5;
	public int actionPoints = 2;


	void Awake () {
		moveDestination = transform.position;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
		
	public virtual void TurnUpdate() {
		if (actionPoints <= 0) {
			//actionPoints = 2;
			moving = false;
			attacking = false;
			//GameManager3.instance.nextTurn();
		}
	}

	public virtual void TurnOnGUI() {

	}
}
