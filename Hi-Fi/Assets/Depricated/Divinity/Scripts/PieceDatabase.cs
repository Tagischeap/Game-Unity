using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PieceDatabase : MonoBehaviour {

	public List<Piece> pieces = new List<Piece> ();

	// Use this for initialization
	void Start () {
		pieces.Add (new Piece ("Queenpx", 0, "Big Tits", 40, 40, 12, 5, 2, 5, 1, "Double Damage area of effect with range 1"));
		pieces.Add (new Piece ("Kingpx", 1, "Big Dong", 40, 40, 12, -5, 2, 5, 1, "Half Damage area of effect with range 1"));
		pieces.Add (new Piece ("BishopPx", 2, "Big Dong", 40, 40, 12, -5, 2, 5, 1, "Half Damage area of effect with range 1"));
		pieces.Add (new Piece ("KnightPx", 3, "Big Dong", 40, 40, 12, -5, 2, 5, 1, "Half Damage area of effect with range 1"));

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
