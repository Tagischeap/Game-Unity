using UnityEngine;
using System.Collections;

[System.Serializable]
public class Piece {

	public string pieceName;
	public int pieceID;
	public string pieceDesc;
	public Sprite pieceIcon;
	public GameObject pieceObject;
	public int pieceValue;

	public int pieceWght;
	public int pieceHP;
	public int pieceDmg;
	public int pieceRng;
	public int pieceAtkCost;
	public int pieceMovCost;
	public string pieceSpecial;

	public Piece(
		string name, int id, string desc, int value, 
		int weight, int hitPoints, int damage, int range, int atkCost, int movCost, string special){

		pieceName = name;
		pieceID = id;
		pieceDesc = desc;
		pieceValue = value;

		pieceWght = weight;
		pieceHP = hitPoints;
		pieceDmg = damage;
		pieceRng = range;
		pieceAtkCost = atkCost;
		pieceMovCost = movCost;
		pieceSpecial = special;

		pieceIcon = Resources.Load<Sprite> ("" + name);
	}

	public Piece()
	{
	}
}
