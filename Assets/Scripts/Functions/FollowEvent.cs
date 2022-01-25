using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEvent : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float translateSpeed;
    [SerializeField] private float rotationSpeed;
    private void FixedUpdate() 
    {
        HandleTranslation();
        HandleRotation();
    }
    
    private void HandleTranslation() 
    {
        var targetPosition = targetObject.transform.TransformPoint(offset);
        transform.position = Vector3.Lerp(transform.position, targetPosition, translateSpeed * Time.deltaTime);
    }

    private void HandleRotation()
    {
        var direction = targetObject.transform.position - transform.position;
        var rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
}
