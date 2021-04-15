using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEvent : MonoBehaviour
{
    public Transform targetTransform;
    public Vector3 targetDirection;
    public float targetDistance;
    public float movementSpeed;
    public bool repeat;
    public bool swagger;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool movingToStart;

    //private bool movingToPosition;    
    void Start()
    {
        startPosition = transform.position;
        targetPosition = startPosition + (targetDirection * targetDistance);
    }

    void Update()
    {
        targetPosition = startPosition + (targetDirection * targetDistance);
        if (movingToStart)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, movementSpeed * Time.deltaTime);
            if (transform.position == startPosition)
            {
                movingToStart = false;
                //movingToPosition = true;
            }
        }
        else
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
