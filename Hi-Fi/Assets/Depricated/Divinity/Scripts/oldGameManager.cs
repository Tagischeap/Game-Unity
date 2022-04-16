using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager3 : MonoBehaviour {
	public static GameManager2 instance;

	public GameObject TilePrefab;
	public GameObject UserPlayerPrefab;
	public GameObject OpponetPlayerPrefab;

	public int mapSize = 8;

	List <List<Tile>> map = new List<List<Tile>>();
	public List <Player> players = new List<Player> ();
	public int currentPlayerIndex = 0;

	void Awake() {
		//instance = this;
	}

	// Use this for initialization
	void Start () {
		generateMap ();
		generatePlayers ();
	}
	
	// Update is called once per frame
	void Update () {
		if (players [currentPlayerIndex].hitPoint > 0) {
			players [currentPlayerIndex].TurnUpdate ();
		} else {
			nextTurn ();
		}
	}

	void OnGUI () {
		if (players [currentPlayerIndex].hitPoint > 0) {
			players [currentPlayerIndex].TurnOnGUI ();
		}
	}

	public void nextTurn() {
		if (currentPlayerIndex + 1 < players.Count) {
			currentPlayerIndex++;
		} else {
			currentPlayerIndex = 0;
		}
	}

	public void moveCurrentPlayer(Tile destTile){
		//players [currentPlayerIndex].actionPoints--;
		//players [currentPlayerIndex].gridPosition = destTile.gridPosition;
		players [currentPlayerIndex].moveDestination = destTile.transform.position + 1f * Vector3.up;
	}
	public void attackCurrentPlayer(Tile destTile){
		//players[currentPlayerIndex].gridPosition = destTile.gridPosition;
		Player target = null;
		foreach (Player p in players) {
			/*if (p.gridPosition == destTile.gridPosition) {
				target = p;
			}*/
		}

		if (target != null) {
			/*
			Debug.Log ("Player: " + players [currentPlayerIndex].gridPosition.x + ", " +
				players [currentPlayerIndex].gridPosition.y + "Target: " +
				target.gridPosition.x + ", " + target.gridPosition.y
			);
			*/
			if (players [currentPlayerIndex].gridPosition.x >= target.gridPosition.x - 1 &&
			    players [currentPlayerIndex].gridPosition.x <= target.gridPosition.x + 1 &&
			    players [currentPlayerIndex].gridPosition.y >= target.gridPosition.y - 1 &&
			    players [currentPlayerIndex].gridPosition.y <= target.gridPosition.y + 1) {
				//attack logic
				int amountOfDamage = players [currentPlayerIndex].damage;
				target.hitPoint -= amountOfDamage;
				Debug.Log (players [currentPlayerIndex].playerName + " Hit: " + 
					amountOfDamage + " at " + target.playerName);
			} else {
				Debug.Log ("Target not adjacent.");
			}
		}
	}

	void generateMap () {
		map = new List<List<Tile>>();
		for (int i = 0; i < mapSize; i++) {
			List <Tile> row = new List<Tile> ();
			for (int j = 0; j < mapSize; j++) {
				Tile tile = ((GameObject)Instantiate (
					TilePrefab, new Vector3 (i - Mathf.Floor(mapSize/2), -j + Mathf.Floor(mapSize/2), 0),
					Quaternion.Euler(new Vector3 ()))).GetComponent<Tile>();
				/*tile.gridPosition = new Vector2 (i, j);*/
				row.Add (tile);
				tile.transform.parent = this.gameObject.transform;
				tile.name = (i + ", " + j);
			}
			map.Add (row);
		}
	}
	void generatePlayers (){
		UserPlayer player;
		OpponetPlayer opponetPlayer;

		player = ((GameObject)Instantiate (
			UserPlayerPrefab, new Vector3 (
				0 - Mathf.Floor (mapSize / 2),
				1 + Mathf.Floor (mapSize / 2),
				0
			),Quaternion.Euler (new Vector3 ()))).GetComponent<UserPlayer> ();
		player.gridPosition = new Vector2(0,0);
		player.playerName = " Bitch Lord";
		players.Add (player);

		player = ((GameObject)Instantiate (
			UserPlayerPrefab, new Vector3 (
				1 - Mathf.Floor (mapSize / 2),
				1 + Mathf.Floor (mapSize / 2),
				0
			),Quaternion.Euler (new Vector3 ()))).GetComponent<UserPlayer> ();
		player.gridPosition = new Vector2(mapSize-1,mapSize-1);
		player.playerName = " Bitch Lord 2";
		players.Add (player);

		player = ((GameObject)Instantiate(
			UserPlayerPrefab, new Vector3(
				2 - Mathf.Floor (mapSize / 2),
				1 + Mathf.Floor (mapSize / 2),
				0
			),Quaternion.Euler(new Vector3()))).GetComponent<UserPlayer>();
		player.gridPosition = new Vector2(4,4);
		player.playerName = " Bitch Lord 3";
		players.Add (player);

		/*
		opponetPlayer = ((GameObject)Instantiate (
			OpponetPlayerPrefab, new Vector3 (6 - Mathf.Floor (mapSize / 2), -4 + Mathf.Floor (mapSize / 2), 0),
			Quaternion.Euler (new Vector3 ()))).GetComponent<OpponetPlayer> ();

		players.Add (opponetPlayer);
		*/
	}

}
