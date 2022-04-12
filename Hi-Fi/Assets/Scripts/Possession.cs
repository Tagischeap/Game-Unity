using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Possession : MonoBehaviour
{

    public GameObject player;
    public GameObject posses;

    public GameObject cam;
    public GameObject camRoot;
    GameObject previousRoot;

    public bool boarded = false;
    public bool near = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (near)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (boarded)
                {
                    Debug.Log("Exit");
                    boarded = false;
                }
                else
                {
                    Debug.Log("Entered");
                    boarded = true;
                }
                activate();
            }
        }
        
    }

    private void activate()
    {
        if (boarded)
        {
            previousRoot = cam.GetComponent<CinemachineVirtualCamera>().Follow.gameObject;
            player.transform.parent = posses.transform;
            player.transform.localPosition = Vector3.zero;
            player.transform.rotation = Quaternion.Euler(0,-180,0);
            cam.GetComponent<CinemachineVirtualCamera>().Follow = camRoot.transform;
        }
        else
        {
            cam.GetComponent<CinemachineVirtualCamera>().Follow = previousRoot.transform;
            near = false;
        }
        posses.GetComponent<MarbleController>().enabled = boarded;
        player.SetActive(!boarded);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player")
        {
            near = true;
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
