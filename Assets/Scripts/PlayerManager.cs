using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerManager : MonoBehaviour
{
    private Rigidbody _RB;
    Vector3 vel;
    private IEnumerator coroutine;
    private bool isGrounded = true;
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
        _RB = GetComponent<Rigidbody>();
        startPosition = transform.position;
        Cursor.lockState = CursorLockMode.Locked;
    }

    //TODO Player control management
    void Update()
    {
        vel = _RB.velocity;

        updateInputs();
        //Input.GetAxis("Mouse Y")

        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0) * Time.deltaTime * mouseSensitivity);
        if (Input.GetKeyDown(KeyCode.P))
        {
            transform.position = startPosition;
        }
    }
    private void move()
    {

    }
    private void jump()
    {
        _RB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts[0].normal == Vector3.up)
        {
            //isGrounded = true;
        }
    }

    private void updateInputs()
    {
        Camera camera = Camera.main;
        
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
                _RB.velocity = direction;
                //rb.velocity = (transform.forward * walkSpeed) * zAxis;
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

    public Quaternion _uprightJoinTargetRot;
    public float _uprightJointSpringStrength;
    public float _uprightJointSpringDamper;
    public void UpdateUprightForce()
    {
        Quaternion characterCurrent = transform.rotation;
        //Quaternion toGoal = UtilsMath.ShortestRotation(_uprightJoinTargetRot, characterCurrent);
        Quaternion toGoal = Quaternion.Slerp(_uprightJoinTargetRot, characterCurrent, 1);
        Vector3 rotAxis;
        float rotDegrees;
        toGoal.ToAngleAxis(out rotDegrees, out rotAxis);
        rotAxis.Normalize();

        float rotRadians = rotDegrees * Mathf.Deg2Rad;

        _RB.AddTorque((rotAxis * (rotRadians * _uprightJointSpringStrength)) - (_RB.angularVelocity * _uprightJointSpringDamper));
    }
    public float RayLength = 6;
    public float RideHeight = 5;
    public float RideSpringStrength = 1;
    public float RideSpringDamper = 1;
    private void FixedUpdate()
    {
        Vector3 DownDir = transform.TransformDirection(Vector3.down);
        RaycastHit _rayHit;
        bool _rayDidHit = Physics.Raycast(transform.position, DownDir, out _rayHit, 1);

        Debug.DrawRay(transform.position, DownDir, Color.red);

        //rb.AddForce(-Physics.gravity, ForceMode.VelocityChange);
        if (_rayDidHit)
        {
            Vector3 vel = _RB.velocity;
            Vector3 rayDir = transform.TransformDirection(DownDir);

            Vector3 otherVel = Vector3.zero;
            Rigidbody hitBody = _rayHit.rigidbody;
            if (hitBody != null)
            {
                otherVel = hitBody.velocity;
            }
            float rayDirVel = Vector3.Dot(rayDir, vel);
            float otherDirVel = Vector3.Dot(rayDir, otherVel);

            float relVel = rayDirVel - otherDirVel;
            float x = _rayHit.distance - RideHeight;

            float springForce = (x * RideSpringStrength) - (relVel * RideSpringDamper);
            Debug.DrawLine(transform.position, transform.position + (rayDir * springForce), Color.yellow);

            _RB.AddForce(rayDir * springForce);
            if(hitBody != null)
            {
                hitBody.AddForceAtPosition(rayDir * -springForce, _rayHit.point);
            }

        }
        UpdateUprightForce();
    }
}

//TODO Make the player able to spin but not strafe when in the air
//TODO Camera Following, and control.
//TODO Stairs and ledge steping.