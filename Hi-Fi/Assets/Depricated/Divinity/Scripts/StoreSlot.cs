using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class StoreSlot : MonoBehaviour {

	public Piece piece;
	Image pieceImage;
	public int slotNumber;
	Inventory inventory;
	bool dropped = false;

	// Use this for initialization
	void Start () {
		inventory = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory> ();
		pieceImage = gameObject.transform.GetChild (0).GetComponent<Image> ();
	}

	// Update is called once per frame
	void Update () {
		if (inventory.Pieces[slotNumber].pieceName != null) {
			piece = inventory.Pieces [slotNumber];
			pieceImage.enabled = true;
			pieceImage.sprite = inventory.Pieces [slotNumber].pieceIcon;
		} else {
			pieceImage.enabled = false;
		}

	}

}
