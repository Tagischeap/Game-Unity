using UnityEngine;
using System.Collections;

public class MultiPlayer : MonoBehaviour {

	GameObject gameCon;
	GameManager2 gameMan;

	public bool multiplayer = false;
	public GameObject[] player;

	Camera[] playerCamera;
	Vector3[] playerPosition;

	float[] screenX;
	float[] screenY;
	float[] screenH;
	float[] screenW;

	// Use this for initialization
	void Start () {
		gameCon = GameObject.FindGameObjectWithTag ("GameController");
		gameMan = gameCon.GetComponent<GameManager2> ();

		player = new GameObject[4];
		playerCamera = new Camera[4];
		playerPosition = new Vector3[4];

		screenX = new float[4];
		screenY = new float[4];
		screenW = new float[4];
		screenH = new float[4];

		screenX[0] = 0f;
		screenY[0] = 0f;
		screenW[0] = 1f;
		screenH[0] = 1f;


		player [0] = GameObject.FindWithTag ("Player");
		playerCamera[0] = player[0].GetComponentInChildren<Camera>();		
	}

	// Update is called once per frame
	void Update () {

	}

	public void SpawnPlayer (int plyN){
		playerPosition[0] = player[0].GetComponent<Transform>().position;

		player[plyN] =  Instantiate(player[0], playerPosition[0], Quaternion.identity) as GameObject;
		player[plyN].name = "Player_"+(plyN + 1);

		player [plyN].GetComponent<Controller_Logic> ().playerNumber = plyN + 1;

		playerCamera[plyN] = player[plyN].GetComponentInChildren<Camera>();
		playerCamera[plyN].GetComponent<AudioListener>().enabled = false;

		//Splits screen based on number of players
		/*
		if (gameMan.playerCount == 1){
			screenX [0] = 0f;
			screenY [0] = 0.5f;
			screenX [1] = 0f;
			screenY [1] = -0.5f;
		} else if (gameMan.playerCount == 2){
			screenX [0] = 0f;
			screenY [0] = 0.5f;
			screenX [1] = -0.5f;
			screenY [1] = -0.5f;
			screenX [2] = 0.5f;
			screenY [2] = -0.5f;
		} else if (gameMan.playerCount == 3){
			screenX [0] = -0.5f;
			screenY [0] = 0.5f;
			screenX [1] = 0.5f;
			screenY [1] = 0.5f;
			screenX [2] = 0.5f;
			screenY [2] = -0.5f;
			screenX [3] = -0.5f;
			screenY [3] = -0.5f;
		}
		for (int i = 0; i <= gameMan.playerCount; i++){
			playerCamera[i] = player[i].GetComponentInChildren<Camera>();
			playerCamera[i].rect = new Rect (screenX[i], screenY[i], 1f, 1f);
		}
		*/

		/*
		playerCamera[plyN] = player[plyN].GetComponentInChildren<Camera>();
		playerCamera[plyN].rect = new Rect (screenX[plyN], screenY[plyN], 1f, 1f);*/


		print ("Player_"+ (plyN + 1) + " Spawned");

	}

}
