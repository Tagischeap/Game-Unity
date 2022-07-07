using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraController : MonoBehaviour
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
            transform.position = offset + targetObject.transform.position;
        }
    }
}
