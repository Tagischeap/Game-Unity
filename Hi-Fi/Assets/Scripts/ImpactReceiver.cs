using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ImpactReceiver : MonoBehaviour
{
	private CharacterController _controller;

	public bool canBePushed = true;
	public float mass = 1f;

    private float hitForce;

	private Vector3 impact = Vector3.zero;
	
    void Start() {
		_controller = GetComponent<CharacterController>();
	}
    
    void Update()
    {
        if(impact.magnitude > 0.2)
        {
            // Moves Character
            _controller.Move(impact * Time.deltaTime); 
        }
        // impact returns to zero
        impact = Vector3.Lerp(impact, Vector3.zero, 5*Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        Rigidbody rb = other.collider.attachedRigidbody;
        hitForce = rb.mass;
        AddImpact(other.relativeVelocity * hitForce);
    }

    public void AddImpact(Vector3 force)
    {
        Vector3 direction = force.normalized;
        //direction.y = 0.5f; //Upwards velocity
        impact += direction.normalized * force.magnitude / mass;
    }
}
