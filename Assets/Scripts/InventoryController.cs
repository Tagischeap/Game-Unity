using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public List<GameObject> inventory;
    
    // Start is called before the first frame update
    void Start()
    {
        inventory = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addToInventory(GameObject item)
    {
        inventory.Add(item);
    }
}
