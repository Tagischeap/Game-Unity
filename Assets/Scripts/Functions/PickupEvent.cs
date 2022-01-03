using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupEvent : MonoBehaviour
{
    public string itemName = "Coin";
    public GameObject inventory;

    public float amount = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void pick()
    {
        if (inventory)
        {
            inventory.GetComponent<InventoryController>().addToInventory(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(itemName + " Trigger Entered");
        pick();
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(itemName + " Trigger Exited");
    }
}
