using UnityEngine;
using System.Collections;

public class GameManager2 : MonoBehaviour {

	public int playerCount = 1;
	[SerializeField]
	private GameObject player;
	[SerializeField]
//	private Controller_Logic controlLogic;
	MultiPlayer multi;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("Player");
		multi = this.GetComponent<MultiPlayer> ();

//		controlLogic = player.GetComponent<Controller_Logic> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (playerCount < 4 && (Input.GetButtonDown ("Joy " + (playerCount + 1) + " Button 7"))) {		
			print (Input.GetButtonDown ("Joy " + (playerCount + 1) + " Button 7"));
			multi.SpawnPlayer (playerCount);
			playerCount = playerCount + 1;	
		}
	}
}
