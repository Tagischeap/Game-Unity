using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class Store : MonoBehaviour {

    public GameObject slots;
    public List<GameObject> sSlots = new List<GameObject>();
    public List<Piece> sPieces = new List<Piece>();
    PieceDatabase database;

    int xPos = 0;
    int yPos = 0;


	// Use this for initialization
	void Start () {
        database = GameObject.FindGameObjectWithTag("PieceDatabase").GetComponent<PieceDatabase>();
        
        //Instantiate Store Slots
        int slotamount=0;

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                GameObject slot = (GameObject)Instantiate(slots);

                slot.GetComponent<Slot>().slotNumber = slotamount;

                slot.transform.SetParent(
                    GameObject.FindGameObjectWithTag("Store").gameObject.transform, false);
                slot.GetComponent<RectTransform>().localPosition = new Vector3(xPos, yPos, 0);
                slot.name = ("Slot: " + i + ", " + j);
                xPos = xPos + 48;
                if (j == 4)
                {
                    xPos = 0;
                    yPos = yPos - 100;
                }
                sPieces.Add(new Piece());
                sSlots.Add(slot);

                slotamount++;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {

    }
}
