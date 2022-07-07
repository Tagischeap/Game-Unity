using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterActivate : MonoBehaviour
{
    public bool isActive = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) {
        isActive = true;
    }
    private void OnTriggerExit(Collider other) {
        isActive = false;
    }
    private void OnTriggerStay(Collider other) {
        
    }
}
