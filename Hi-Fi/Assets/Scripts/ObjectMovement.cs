using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    [SerializeField] private Vector3 rotationDirection;
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private bool swaggerRotation = false;
    [SerializeField] private bool repeatRotation = true;
    [SerializeField] private Vector3 movementDirection;
    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private bool swaggerMovement = false;
    [SerializeField] private bool repeatMovement = true;

    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(rotationDirection * rotationSpeed, Space.Self);
        //transform.MoveTowards(movementDirection * movementSpeed, Space.Self);
    }
}
