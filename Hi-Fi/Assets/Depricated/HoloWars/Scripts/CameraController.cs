﻿using UnityEngine;
using System.Collections;

public class CameraController0 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    public Transform player;
    public Vector3 offset;

    // Update is called once per frame
    void Update () {
        // Camera follows the player with specified offset position
        //transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, offset.z); 
    }
}