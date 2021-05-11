using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerManager : MonoBehaviour
{
    public Rigidbody rb;
    Vector3 vel;
    private IEnumerator coroutine;
    private bool isGrounded;
    private GlobalManager global;
    public Vector3 startPosition;
    private Vector3 cameraRelative;
    public bool canMove = true;
    public bool canSpin = true;
    public float stepRate = 0.25f;
    public float walkSpeed = 5;
    public float runSpeed = 10;
    public float jumpForce = 120;
    public float spinSpeed = 50;
    public float mouseSensitivity = 500;
    void Start()
    {
        startPosition = transform.position;
    }
    //TODO Player control management
    void Update()
    {
        updateInputs();
        //Input.GetAxis("Mouse Y")
        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0) * Time.deltaTime * mouseSensitivity);
        if (Input.GetKeyDown(KeyCode.P))
        {
            transform.position = startPosition;
        }
    }
    private void jump()
    {
        isGrounded = false;
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts[0].normal == Vector3.up)
        {
            isGrounded = true;
        }
    }

    private void updateInputs() {
        if (GetComponent<Camera>() != null)
        {
            cameraRelative = GetComponent<Camera>().transform.InverseTransformDirection(transform.position);
            print("--------------------");
            if(cameraRelative.x > 0)
            {
                print("Forward");
            }
            else
            {
                print("Back");
            }
            if(cameraRelative.y > 0)
            {
                
                print("Up");
            }
            else
            {
                print("Down");
            }
            if(cameraRelative.z > 0)
            {
                print("Right");
            }
            else
            {
                print("Left");
            }
        }
        vel = rb.velocity;
        float xAxis = Input.GetAxis("Horizontal");
        float zAxis = Input.GetAxis("Vertical");
        Vector3 aim = new Vector3 ( xAxis, 0, zAxis);
        Vector3 direction = new Vector3 ((xAxis * walkSpeed), 0, (zAxis * walkSpeed));
        if (Input.GetButton("Vertical") || Input.GetButton("Horizontal"))
        {
            if (canSpin)
            {
                //Spins character to look where they're aiming
                if (aim != Vector3.zero)
                {
                    float step = spinSpeed * Time.deltaTime;
                    Vector3 newDirection = Vector3.RotateTowards(transform.forward, aim, step, 0.0f);
                    transform.rotation = Quaternion.LookRotation(newDirection);
                }
            }
            if (canMove && isGrounded)
            {
                //rb.AddForce(direction, ForceMode.VelocityChange);
                //rb.velocity = direction;
                rb.velocity = (transform.forward * walkSpeed) * zAxis;
            }
        }
        if (Input.GetButton("Jump"))
        {
            if (isGrounded)
            {
                jump();
            }
        }
        if(Mathf.Abs(transform.position.y) > 100)
        {
            Debug.Log("Fall out");
            if (global != null)
            {
                global.GameOver();
            }
        }
    }
}

//TODO Make the player able to spin but not strafe when in the air
//TODO Camera Following, and control.
//TODO Stairs and ledge steping.