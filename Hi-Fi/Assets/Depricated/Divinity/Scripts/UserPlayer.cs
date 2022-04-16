using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UserPlayer : Player {

	//public float moveSpeed = 10.0f;

	// Use this for initialization
	void Start () {
		/*GameObject.Find("Attack Button").GetComponent<Button>().onClick.AddListener(() => {
			onAttackButton();
		});
		GameObject.Find("Move Button").GetComponent<Button>().onClick.AddListener(() => {
			onMoveButton();
		});
		GameObject.Find("End Turn Button").GetComponent<Button>().onClick.AddListener(() => {
			onEndTurnButton();
		});*/
	}
	
	// Update is called once per frame
	void Update () {
		/*if (GameManager3.instance.players [GameManager3.instance.currentPlayerIndex] == this) {
			GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 1f);
		} else {
			GetComponent<SpriteRenderer> ().color = new Color (0.75f, 0.75f, 0.75f, 0.75f);
		}*/
		if (hitPoint <= 0) {
			GetComponent<SpriteRenderer> ().color = new Color (0f, 0f, 0f, 0.25f);
		}
		if (actionPoints <= 0) {
			GetComponent<SpriteRenderer> ().color = new Color (0.5f, 0.5f, 0.5f, 0.50f);
		}
	}

	public override void TurnUpdate() {
		if (Vector3.Distance (moveDestination, transform.position) > 0.1f) {
			transform.position += (moveDestination - transform.position).normalized * moveSpeed * Time.deltaTime;
			if (Vector3.Distance (moveDestination, transform.position) <= 0.1f) {
				transform.position = moveDestination;
				//GameManager.instance.nextTurn ();
			}
		}
		base.TurnUpdate ();
	}

	public void onMoveButton(){
		if (!moving) {
			moving = true;
			attacking = false;
		} else {
			moving = false;
			attacking = false;
		}
	}
	public void onAttackButton(){
		if (!attacking) {
			moving = false;
			attacking = true;
		} else {
			moving = false;
			attacking = false;
		}
	}
	public void onEndTurnButton(){
			moving = false;
			attacking = false;
			//GameManager3.instance.nextTurn();
	}

	public override void TurnOnGUI () {
		float buttonHeight = Screen.height / 10;
		float buttonWidth = Screen.width / 2;

		//buttons
		Rect buttonRect = new Rect(0, Screen.height - buttonHeight *3, buttonWidth, buttonHeight);
		if (GUI.Button (buttonRect, "Move")) {
			onMoveButton ();
		}
		buttonRect = new Rect(0, Screen.height - buttonHeight *2, buttonWidth, buttonHeight);
		if (GUI.Button (buttonRect, "Attack")) {
			onAttackButton ();
		}
		buttonRect = new Rect(0, Screen.height - buttonHeight *1, buttonWidth, buttonHeight);
		if (GUI.Button (buttonRect, "End Turn")) {
			onEndTurnButton ();
		}
		base.TurnOnGUI ();

	}

}
