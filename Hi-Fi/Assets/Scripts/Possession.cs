using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;

public class Possession : MonoBehaviour
{

    public GameObject player;
    public GameObject posses;

    public GameObject cam;
    public GameObject camRoot;
    GameObject previousRoot;

    public bool boarded = false;
    public bool near = false;

    private StarterAssetsInputs starterAssetsInputs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (near)
        {
            if (starterAssetsInputs.interact) 
            {
                Debug.Log("Button pressed");
                starterAssetsInputs.interact = false;
                if (boarded)
                {
                    Debug.Log("Exit");
                    boarded = false;
                    player.GetComponent<ThirdPersonController>().LockPlayerMovement = false;
                }
                else
                {
                    Debug.Log("Entered");
                    boarded = true;
                    player.GetComponent<ThirdPersonController>().LockPlayerMovement = true;
                }
                activate();
            }
        }
    }

    private void activate()
    {
        if (boarded)
        {
            Debug.Log("Fart");
            previousRoot = cam.GetComponent<CinemachineVirtualCamera>().Follow.gameObject;
            player.transform.parent = posses.transform;
            player.transform.localPosition = Vector3.zero + new Vector3(-0.5f, 1f, 0f);
            player.transform.rotation = Quaternion.Euler(0,-180,0);
            cam.GetComponent<CinemachineVirtualCamera>().Follow = camRoot.transform;
            posses.GetComponent<MarbleController>().Parked = false;
        }
        else
        {
            cam.GetComponent<CinemachineVirtualCamera>().Follow = previousRoot.transform;
            posses.GetComponent<MarbleController>().Parked = true;
            near = false;
        }
        posses.GetComponent<MarbleController>().enabled = boarded;
        //player.SetActive(!boarded);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player")
        {
            near = true;
            starterAssetsInputs = other.GetComponent<StarterAssetsInputs>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            near = false;
        }
    }
}
