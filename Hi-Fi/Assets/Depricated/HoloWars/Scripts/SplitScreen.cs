using UnityEngine;
using System.Collections;

public class SplitScreen : MonoBehaviour {
	public bool multiplayer = false;
	public	GameObject[] player;

	Camera[] playerCamera;
	Vector3[] playerPosition;
	float[] posX;
	float[] posY;

	float[] screenX;
	float[] screenY;
	float[] screenW;
	float[] screenH;

	GridMove[] gridMove;
	CameraController camcontrol;


	void Awake (){

	
	}
	
	void Start () {
		player = new GameObject[4];
		playerCamera = new Camera[4];
		playerPosition = new Vector3[4];
		posX = new float[4];
		posY = new float[4];

		screenX = new float[4];
		screenY = new float[4];
		screenW = new float[4];
		screenH = new float[4];

		screenX[0] = 0f;
		screenY[0] = 0f;
		screenW[0] = 1f;
		screenH[0] = 1f;

		gridMove = new GridMove[4];

		player[0] = GameObject.Find("Player");
		playerCamera[0] = player[0].GetComponentInChildren<Camera>();		
	}

	public void SpawnPlayer (int plyN){
		playerPosition[0] = player[0].GetComponent<Transform>().position;

		player[plyN] =  Instantiate(player[0], playerPosition[0], Quaternion.identity) as GameObject;
		player[plyN].name = "Player_"+(plyN);

		playerCamera[plyN] = player[plyN].GetComponentInChildren<Camera>();
		playerCamera[plyN].GetComponent<AudioListener>().enabled = false;

		gridMove[plyN] = player[plyN].GetComponent<GridMove>();
		gridMove[plyN].playerNumber = plyN;
	}
	
	float Distances (int first, int second){
		playerPosition[first] = player[first].GetComponent<Transform>().position;
		playerPosition[second] = player[second].GetComponent<Transform>().position;
		return Vector3.Distance(playerPosition[first], playerPosition[second]);
	}

	void Spliter (int first, int second){
		playerCamera[first] = player[first].GetComponentInChildren<Camera>();
		playerCamera[second] = player[second].GetComponentInChildren<Camera>();
		playerPosition[first] = player[first].GetComponent<Transform>().position;
		playerPosition[second] = player[second].GetComponent<Transform>().position;

/*		Debug.Log ("First: " + screenX[first] + ", " + screenY[first] + " | Second: " + 
		           screenX[second] + ", " + screenY[second]);
*/
		if (Distances(first, second) >= 5 || Distances(first, second) <= -5){
		
			playerCamera[first].rect = new Rect (screenX[first], screenY[first], screenH[first], screenW[first]);
			playerCamera[second].rect = new Rect (screenX[second], screenY[second], screenH[first], screenW[first]);
			
			playerCamera[first].GetComponent<Transform>().localPosition = new Vector3( 0, 0, -1);
			playerCamera[second].GetComponent<Transform>().localPosition = new Vector3( 0, 0, -1);

			if (playerPosition[first].x - playerPosition[second].x >= 5){
				screenX[first] = 0.5f;
				screenX[second] = 0f;
				screenH[first] = 0.5f;
				screenH[second] = 0.5f;
			} else if (playerPosition[first].x - playerPosition[second].x <= -5){
				screenX[first] = 0f;
				screenX[second] = 0.5f;
				screenH[first] = 0.5f;
				screenH[second] = 0.5f;
			} else {
				screenX[first] = 0f;
				screenX[second] = 0f;
				screenH[first] = 1f;
				screenH[second] = 1f;
			}
			if (playerPosition[first].y - playerPosition[second].y >= 5){
				screenY[first] = 0.5f;
				screenY[second] = 0f;
				screenW[first] = 0.5f;
				screenW[second] = 0.5f;
			} else if (playerPosition[first].y - playerPosition[second].y <= -5){
				screenY[first] = 0f;
				screenY[second] = 0.5f;
				screenW[first] = 0.5f;
				screenW[second] = 0.5f;
			} else {
				screenY[first] = 0f;
				screenY[second] = 0f;
				screenW[first] = 1f;
				screenW[second] = 1f;
			}
		} else {
			playerCamera[first].rect = new Rect (0,0,1,1);
			playerCamera[second].rect = new Rect (0,0,-1,-1);
		
			playerCamera[first].GetComponent<Transform>().position = new Vector3(
				(playerPosition[second].x + playerPosition[first].x) / 2, 
				(playerPosition[second].y + playerPosition[first].y) / 2, -1);
		}
	}
	
	void Update () {
		if (multiplayer){
			Spliter(0, 1);
		} else {
			//playerPosition[0] = player[0].GetComponent<Transform>().position;
			/*playerCamera[0].GetComponent<Transform>().position = new Vector3(
				playerPosition[0].x, playerPosition[0].y, -10);*/
		}
	}
}