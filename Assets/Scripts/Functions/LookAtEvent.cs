﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtEvent : MonoBehaviour
{
    public GameObject targetObject;
    public Vector3 offset = new Vector3(0, 3, -3);
    void Start()
    {
        
    }
    
    void Update()
    {
        if (targetObject != null)
        {
            transform.LookAt(targetObject.transform);
        }
    }
}
