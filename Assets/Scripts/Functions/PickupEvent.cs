using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupEvent : MonoBehaviour
{
    public string itemName = "Item";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log(itemName + " Trigger Entered");
    }

    private void OnTriggerExit(Collider other) {
        Debug.Log(itemName + " Trigger Exited");
    }
}
