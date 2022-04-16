using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

	public List<GameObject> Slots = new List<GameObject> ();
	public List<Piece> Pieces = new List<Piece> ();
	public GameObject slots;
	PieceDatabase database;
	int xPos = 48;
	int yPos = 40;
	int xFPos = 48;
	int yFPos = 16;

	public GameObject draggedPieceGameObject;
	public bool draggingPiece = false;
	public Piece draggedPiece;
	public int indexDraggedPiece;

	// Use this for initialization
	void Start () {
		int slotamount = 0;
		database = GameObject.FindGameObjectWithTag ("PieceDatabase").GetComponent<PieceDatabase> ();

        slotamount = 0;

		//Instantiate Formation Slots

		for (int i = 0; i < 2; i++) {
			for (int j = 0; j < 8; j++) {
				GameObject slot = (GameObject)Instantiate (slots);
                Slots.Add (slot);
				Pieces.Add (new Piece ());
                slot.GetComponent<Slot> ().slotNumber = slotamount;
                slot.transform.SetParent (
					GameObject.FindGameObjectWithTag("Formation").gameObject.transform, false);
				slot.GetComponent<RectTransform> ().localPosition = new Vector3 (xFPos, yFPos, 0);
                slot.name = ("Slot: " + i + ", " + j);
                xFPos = xFPos +32;
				if (j == 7) {
					xFPos = 48;
					yFPos = yFPos-32;
				}
				slotamount++;
			}
		}

		//Instantiate Inventory Slots
		for (int i = 0; i < 4; i++) {
			for (int j = 0; j < 5; j++) {
				GameObject slot = (GameObject) Instantiate (slots);
                slot.GetComponent<Slot> ().slotNumber = slotamount;
                Slots.Add (slot);
                Pieces.Add (new Piece ());
                slot.transform.SetParent (
					GameObject.FindObjectOfType<Inventory> ().gameObject.transform, false);
				slot.GetComponent<RectTransform> ().localPosition = new Vector3 (xPos, yPos, 0);
				slot.name = ("Slot:" + i + ", " + j);
				xPos = xPos +48;

				if (j == 4) {
					xPos = 48;
					yPos = yPos-100;
				}
                slotamount++;
			}
		}

		addPiece (0);
		addPiece (1);
		addPiece (2);
		addPiece (3);

	}

	void Update(){
		if (draggingPiece) {
			//Vector3 pos = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, -1);
			//Vector3 posi = Camera.main.ScreenToWorldPoint(pos);
			//draggedPieceGameObject.GetComponent<RectTransform> ().localPosition = posi;
			draggedPieceGameObject.GetComponent<RectTransform> ().position = new Vector3(
				Input.mousePosition.x, Input.mousePosition.y , -10);
		}
	}

	public void showDraggedPiece(Piece piece, int slotNumber){

		indexDraggedPiece = slotNumber;
		draggingPiece = true;
		draggedPieceGameObject.SetActive (true);

		draggedPieceGameObject.GetComponent<Image> ().sprite = piece.pieceIcon;
		draggedPiece = piece;
	}

	public void hideDraggedPiece(){
		draggingPiece = false;
		draggedPieceGameObject.SetActive (false);
	}

	void addPiece(int id){
		for (int i = 0; i < database.pieces.Count; i++) {
			if (database.pieces [i].pieceID == id) {
				Piece piece = database.pieces [i];
				addtoEmptySlot (piece);
				break;
			}
		}
	}

	void addtoEmptySlot(Piece piece){
		for (int i = 0; i < Pieces.Count; i++) {
			if (Pieces [i+16].pieceName == null) {
				Pieces [i+16] = piece;

				Debug.Log (Pieces [i+16].pieceName + " in slot " + (i+16));
				break;
			}
		}
	}
}