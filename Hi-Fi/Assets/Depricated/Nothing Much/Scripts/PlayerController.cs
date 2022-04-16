using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]

public class PlayerController2 : MonoBehaviour
{

    public float speed = 1.0f;
    public bool canJump = true;
    public bool canMove = true;
    public bool canAct = true;
    public bool isMoving = false;
    private bool grounded = false;
    public float gravity = 10.0f;
    public float maxVelocityChange = 1.0f;
    public float jumpHeight = 2.0f;
    public Rigidbody rigidbody;
    public Camera cameraBase;
    public Animator animator;

    // Use this for initialization
    void Start()
    {
    }

    void Update()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        RaycastHit hit;
        Vector3 offset = transform.position + new Vector3(0, 0, 0);
        Vector3 height1 = rigidbody.transform.position + new Vector3(0, 0.25f, 0);
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(offset, transform.TransformDirection(Vector3.forward), out hit, 0.25f))
        {
            Debug.DrawRay(offset, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            //Debug.Log("Did Hit");
            Debug.DrawRay(height1, transform.TransformDirection(Vector3.down) * 0.25f, Color.green);
        }
        else
        {
            Debug.DrawRay(offset, transform.TransformDirection(Vector3.forward) * 0.25f, Color.white);
            //Debug.Log("Not Hit");
            Debug.DrawRay(height1, transform.TransformDirection(Vector3.down) * 0.25f, Color.white);
        }
    }

    void FixedUpdate()
    {
        if (rigidbody.velocity != Vector3.zero)
            isMoving = true;
        else
            isMoving = false;


        animator.SetBool("Grounded", grounded);
        if (grounded)
        {
            if (canMove)
            {
                //Turn away from camera
                if ((Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0))
                {
                    // look at camera...
                    transform.LookAt(Camera.main.transform.position, -Vector3.up);
                    // then lock rotation to Y axis only...
                    transform.localEulerAngles = new Vector3(0, 180 + transform.localEulerAngles.y - Camera.main.transform.position.y, 0);
                }
                // Calculate how fast we should be moving
                Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                targetVelocity = transform.TransformDirection(targetVelocity);
                //targetVelocity *= speed;

                //Applies input to animation controller variables
                animator.SetFloat("MoveSpeed", System.Math.Abs(targetVelocity.x) * Input.GetAxis("Horizontal"));
                animator.SetFloat("MoveSpeed", System.Math.Abs(targetVelocity.z) * Input.GetAxis("Vertical"));
                
                // Apply a force that attempts to reach our target velocity
                Vector3 velocity = rigidbody.velocity;
                Vector3 velocityChange = (targetVelocity - velocity);
                velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
                velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
                velocityChange.y = 0;
                rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
                
            // --Jump
                if (canJump && Input.GetButton("Jump"))
                {
                    rigidbody.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
                    //Jump animation
                    animator.SetBool("Jump", Input.GetButton("Jump"));
                }
            }
            // --Action animations
            if (canAct)
            {
               /* if (grounded)
                {
                    bool isWaving = false;
                    if (!isWaving && Input.GetButtonDown("Fire1"))
                    {
                        animator.SetBool("Wave", true);
                        isWaving = true;
                        canMove = false;
                    }
                    if (isWaving)
                    {
                        isWaving = isAnimated(1);
                        canMove = !isWaving;
                    }
                } */


            }
            canAct = true;
        }

        // We apply gravity manually for more tuning control
        rigidbody.AddForce(new Vector3(0, -gravity * rigidbody.mass, 0));
        grounded = false;

    }

    bool isAnimated(int i)
    {
        //Debug.Log(animator.GetCurrentAnimatorStateInfo(1).length + ":" + animator.GetCurrentAnimatorStateInfo(1).normalizedTime);
        return animator.GetCurrentAnimatorStateInfo(i).length >= animator.GetCurrentAnimatorStateInfo(i).normalizedTime;
    }

    void OnCollisionStay()
    {
        grounded = true;
    }

    float CalculateJumpVerticalSpeed()
    {
        // From the jump height and gravity we deduce the upwards speed 
        // for the character to reach at the apex.
        return Mathf.Sqrt(2 * jumpHeight * gravity);
    }
    
}