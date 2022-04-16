using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapGen : MonoBehaviour {

	public GameObject TilePrefab;
	int mapX = 8;
	int mapY = 8;


	List <GameObject> tile = new List<GameObject>();
	List <List<GameObject>> map = new List<List<GameObject>>();

	// Use this for initialization
	void Start () {
		GenerateMap ();
	}

	void GenerateMap () {
		for (int i = 0; i < mapX; i++) {
			for (int j = 0; j < mapY; j++) {
				GameObject tile = Instantiate (TilePrefab,transform.position = 
					new Vector3(i,0,j), Quaternion.Euler(Vector3.zero))as GameObject;
				tile.gameObject.name = "Tile " + i + " " + j;
			}
		}
		

	}

	// Update is called once per frame
	void Update () {
	
	}
}
