using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory0 : MonoBehaviour {
	public List <Item> inventory = new List <Item> ();
	private ItemDatabase database;


	// Use this for initialization
	void Start () {
		database = GameObject.FindGameObjectWithTag ("Item Database").GetComponent <ItemDatabase>();
		inventory.Add (database.itemList[0]);
		inventory.Add (database.itemList[0]);
		inventory.Add (database.itemList[0]);
		inventory.Add (database.itemList[0]);
		inventory.Add (database.itemList[0]);
		inventory.Add (database.itemList[0]);
		inventory.Add (database.itemList[0]);
		inventory.Add (database.itemList[0]);
		inventory.Add (database.itemList[0]);
		inventory.Add (database.itemList[0]);
		inventory.Add (database.itemList[0]);
		inventory.Add (database.itemList[0]);
		inventory.Add (database.itemList[0]);
		inventory.Add (database.itemList[0]);

	}

	void OnGUI()
	{
		for (int i = 0; i < inventory.Count; i++)
		{
			GUI.Label (new Rect (12,i*12,200,50), inventory[i].itemName);
		}
	}
}
