using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Slot : MonoBehaviour, 
	IPointerDownHandler, IPointerUpHandler,
	IPointerEnterHandler, IPointerExitHandler, 
	IDragHandler, IDropHandler {

	public Piece piece;
	Image pieceImage;
	public int slotNumber;
	Inventory inventory;
	bool dropped = false;
	public int lastSlotNumber;

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
		if (Input.GetMouseButtonUp (0)) {
			if (inventory.Pieces [slotNumber].pieceName == null && inventory.draggingPiece) {
				inventory.Pieces [lastSlotNumber] = inventory.draggedPiece;
				inventory.hideDraggedPiece ();
			}
		}

	}

	public void OnPointerEnter(PointerEventData data){
		//Debug.Log (slotNumber);
		GetComponent<Image>().color = new Color (0.5f, 0.5f, 0.5f, 1f);
		lastSlotNumber = slotNumber;
		Debug.Log (lastSlotNumber);
	}

	public void OnPointerExit(PointerEventData data){
		GetComponent<Image>().color = new Color (1f, 1f, 1f, 1f);
		if (inventory.draggingPiece) {

		}
	}

	public void OnPointerUp(PointerEventData data){
		if (inventory.draggingPiece) {
			GetComponent<Image>().color = new Color (1f, 1f, 1f, 1f);
		}
	}

	public void OnPointerDown(PointerEventData data){
		//Debug.Log ("Clicked " + piece.pieceName);
		if (inventory.Pieces [slotNumber].pieceName == null && inventory.draggingPiece) {
			inventory.Pieces [slotNumber] = inventory.draggedPiece;
			inventory.hideDraggedPiece ();
		} else if (inventory.draggingPiece && inventory.Pieces [slotNumber].pieceName != null) {
			inventory.Pieces [inventory.indexDraggedPiece] = inventory.Pieces [slotNumber];
			inventory.Pieces [slotNumber] = inventory.draggedPiece;
			inventory.hideDraggedPiece ();
		}
	}

	public void OnDrag(PointerEventData data){
		if (inventory.Pieces [slotNumber].pieceName != null) {
			inventory.showDraggedPiece (inventory.Pieces [slotNumber], slotNumber);
			inventory.Pieces [slotNumber] = new Piece ();
			lastSlotNumber = slotNumber;
		}
	}

	public void OnDrop(PointerEventData data){
		//Debug.Log ("Dropped on " + slotNumber);

		if (inventory.Pieces [slotNumber].pieceName == null && inventory.draggingPiece) {
			inventory.Pieces [slotNumber] = inventory.draggedPiece;
			inventory.hideDraggedPiece ();
		} else if (inventory.draggingPiece && inventory.Pieces [slotNumber].pieceName != null) {
			inventory.Pieces [inventory.indexDraggedPiece] = inventory.Pieces [slotNumber];
			inventory.Pieces [slotNumber] = inventory.draggedPiece;
			inventory.hideDraggedPiece ();
		}
	}

}