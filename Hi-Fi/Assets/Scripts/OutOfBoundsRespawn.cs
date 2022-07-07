using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class OutOfBoundsRespawn : MonoBehaviour
{

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Out of Bounds");
            other.GetComponent<ThirdPersonController>().Respawn();
        }
    }
    
}
