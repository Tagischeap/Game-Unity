using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectProjectile : MonoBehaviour
{
    [SerializeField] private Transform vfxHit;
    [SerializeField] private Transform vfxDestroy;
    [SerializeField] private float lifeTime = 30f;
    [SerializeField] private float speed = 15f;
    [SerializeField] private float spin = 90f;

    private float timer = 0f;
    private Rigidbody objectRigidbody;
    [SerializeField] private bool destroyOnImpact = true;
    
    private void Awake() 
    {
        objectRigidbody = GetComponent<Rigidbody>();
    }

    private void Start() 
    {
        timer = 0f;
        objectRigidbody.velocity = transform.forward * speed;
        objectRigidbody.angularVelocity = transform.right * spin;
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (lifeTime <= (timer % 60))
        {
            destroyObject();
        }
    }
    
    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<ActionTarget>() != null) 
        {
            Destroy(gameObject);
            if (vfxHit != null)
            {
                Instantiate(vfxHit, transform.position, Quaternion.identity);
            }
        }
        else
        {
            if (destroyOnImpact)
            {
                destroyObject();
            }
        }
        //TODO Player touches object
    }

    private void destroyObject()
    {
            Destroy(gameObject);
            if (vfxDestroy != null)
            {
                Instantiate(vfxDestroy, transform.position, Quaternion.identity);
            }
    }
}
