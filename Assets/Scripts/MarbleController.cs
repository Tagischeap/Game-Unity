using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleController : MonoBehaviour
{
    [SerializeField] private Rigidbody sphereRB;
    [SerializeField] private SphereCollider sphereCL; 
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";
    private float horizontalInput;
    private float verticalInput;
    private bool jumpInput;
    private bool sprintInput;
    private float currentForce;
    public float maxForce;
    public float forwardForce;
    public float steerForce;
    public float jumpForce;
    public float maxBoost;
    private float boostForce;
    public bool drifting;
    public bool boosting;

    public bool frontRWheel;
    public bool frontLWheel;
    public bool rearRWheel;
    public bool rearLWheel;

    [SerializeField] private Transform fRWheelOffset;
    [SerializeField] private Transform fLWheelOffset;
    [SerializeField] private Transform rRWheelOffset;
    [SerializeField] private Transform rLWheelOffset;

    // Start is called before the first frame update
    private void Start() 
    {
        //dechilds model
        sphereRB.transform.parent = null;
    }
    private void Update()
    {
        GetInput();
        GetRayCasts();
    }
    private void FixedUpdate() 
    {
        //Debug.Log(maxForce + " : " + currentForce + " | " + forwardForce + ", " + verticalInput);
        // set model position to sphere
        transform.position = sphereRB.transform.position;
        //adjust speed for sphere
        currentForce = verticalInput;
        currentForce *= (verticalInput != 0 ? forwardForce : maxForce) + boostForce;
        if (frontLWheel || frontRWheel || rearLWheel || rearRWheel)
        {
            if (verticalInput != 0)
            {
                sphereRB.AddForce(transform.forward * currentForce, ForceMode.Acceleration);
            }
            if (jumpInput)
            {
                if(drifting)
                {

                }
                else
                {
                    sphereRB.AddForce(transform.up * jumpForce, ForceMode.Impulse);
                    drifting = true;
                }
            }
            else
            {    
                drifting = false;
            }
        }

        if (sprintInput)
        {
            boostForce = maxBoost;
        }
        else
        {
            boostForce = 0;
        }
        //steer the sphere
        float rot = horizontalInput * steerForce * Time.deltaTime;
        transform.Rotate(0, rot, 0, Space.World);
        
    }
    public void GetInput()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
        jumpInput = Input.GetButton("Jump");
        sprintInput = Input.GetButton("Fire3");
        

    }

    private void GetRayCasts()
    {
        RaycastHit hit;
        Debug.DrawRay(fRWheelOffset.position, Vector3.down, frontRWheel ? Color.blue :Color.green);
        Debug.DrawRay(fLWheelOffset.position, Vector3.down, frontLWheel ? Color.blue :Color.green);
        Debug.DrawRay(rRWheelOffset.position, Vector3.down, rearRWheel ? Color.blue :Color.green);
        Debug.DrawRay(rLWheelOffset.position, Vector3.down, rearLWheel ? Color.blue :Color.green);

        frontRWheel = Physics.Raycast(fRWheelOffset.position, Vector3.down, out hit, 1.0f);
        frontLWheel = Physics.Raycast(fLWheelOffset.position, Vector3.down, out hit, 1.0f);
        rearRWheel = Physics.Raycast(rRWheelOffset.position, Vector3.down, out hit, 1.0f);
        rearLWheel = Physics.Raycast(rLWheelOffset.position, Vector3.down, out hit, 1.0f);
    }
}