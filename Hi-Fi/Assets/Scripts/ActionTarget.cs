using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTarget : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<ObjectProjectile>() != null)
        {
            Debug.Log("Hit " + transform.name);
            if (transform.GetComponent<Renderer>() != null)
            {
                transform.GetComponent<Renderer>().material.color = Color.red;
            }
        }
    }
}
