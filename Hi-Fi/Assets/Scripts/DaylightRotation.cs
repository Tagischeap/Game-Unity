using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaylightRotation : MonoBehaviour
{
    [SerializeField] private float timeOfDay = 90f;
    [SerializeField] private Vector3 rotationDirection;
    [SerializeField] private float rotationSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationDirection * rotationSpeed, Space.Self);
        timeOfDay += Time.deltaTime;
    }
}
