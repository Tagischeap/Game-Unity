using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEvent : MonoBehaviour
{
    public Transform targetTransform;
    public Vector3 targetDirection;
    public Vector3 targetRotation;
    float targetDistance = 1f;
    public float movementSpeed = 1f;
    public bool repeat;
    public bool swagger;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private Vector3 startRotation;
    private bool movingToStart;
    private bool directionSet;
    private bool rotationSet;

    //private bool movingToPosition;    
    void Start()
    {
        startPosition = transform.position;
        targetPosition = startPosition + (targetDirection * targetDistance);

    }

    void Update()
    {
        
        //directionSet = (targetDirection == Vector3.zero);
        if (targetDirection != Vector3.zero)
            directionSet = true;
            else
            directionSet = false;
        //rotationSet = (targetRotation == Vector3.zero);
        if (targetRotation != Vector3.zero)
            rotationSet = true;
            else
            rotationSet = false;

        if (movingToStart)
        {
            if (rotationSet)
            {
                transform.Rotate(new Vector3(0f, Time.deltaTime * -(targetRotation.x), 0f), Space.World);
            }
            if (directionSet)
            {
                targetPosition = startPosition + (targetDirection * targetDistance);
                transform.position = Vector3.MoveTowards(transform.position, startPosition, movementSpeed * Time.deltaTime);
                if (transform.position == startPosition)
                {
                    if (repeat)
                    {
                        movingToStart = false;
                    }
                    //movingToPosition = true;
                }
            }
        }
        else
        {
            if (rotationSet){
                transform.Rotate(new Vector3(0f, Time.deltaTime * (targetRotation.x), 0f), Space.World);
            }
            if (directionSet)
            {
                transform.position = Vector3.MoveTowards(transform.position, startPosition + (targetDirection * targetDistance), movementSpeed * Time.deltaTime);
                if (transform.position == targetPosition)
                {
                    if (swagger)
                    {
                        movingToStart = true;
                        //movingToPosition = false;
                    }
                }
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying)
        {
            startPosition = transform.position;
            targetPosition = startPosition + (targetDirection * targetDistance);
        }
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(startPosition, targetPosition);
    }

    void OnSceneGUI() 
    {
    }

}
