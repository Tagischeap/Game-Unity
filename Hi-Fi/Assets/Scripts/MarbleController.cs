using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleController : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal", VERTICAL = "Vertical";
    float horizontalInput, verticalInput;
    float speed, rotation;
    float currentSpeed,currentRotate;
    [SerializeField] private Rigidbody sphereRB;
    [SerializeField] Transform vehicleModel;
    [SerializeField] Transform sphere;

    [SerializeField] MeshCollider col;



    [Header("Too many Variables")]
    private bool jumpInput, sprintInput;
    private float currentForce;
    public float maxForce, forwardForce, steerForce;
    public float jumpForce, maxBoost, boostForce;
    public bool drifting = false, boosting;

    public bool frontRWheel, frontLWheel;
    public bool rearRWheel, rearLWheel;

    [SerializeField] private Transform fRWheelOffset, fLWheelOffset;
    [SerializeField] private Transform rRWheelOffset, rLWheelOffset;

    [SerializeField] private GameObject boxColider;

    // Start is called before the first frame update
    private void Start() 
    {
        //dechilds model
        //sphereRB.transform.parent = null;
        Physics.IgnoreCollision(sphere.GetComponent<SphereCollider>(), col);
    }
    private void Update()
    {
        GetInput();
        GetRayCasts();

        //transform.position = sphere.transform.position;
    }
    private void FixedUpdate() 
    {
        //---------------------------------------------------------------------------------------------------
        //Debug.Log(maxForce + " : " + currentForce + " | " + forwardForce + ", " + verticalInput);

        // set model position to sphere
        transform.position = sphere.transform.position;

        //steer the sphere
        float rot = horizontalInput * steerForce * Time.deltaTime;
        transform.Rotate(0, rot, 0, Space.World);

        //adjust speed for sphere
        currentForce = verticalInput;
        currentForce *= (verticalInput != 0 ? forwardForce : maxForce) + boostForce;
        
        //If wheels are grounded
        if (frontLWheel || frontRWheel || rearLWheel || rearRWheel)
        {
            if (verticalInput != 0)
            {
                sphereRB.AddForce(transform.forward * currentForce, ForceMode.Acceleration);
            }

            if (jumpInput)
            {
                if (!drifting)
                {
                    sphereRB.AddForce(transform.up * jumpForce, ForceMode.Impulse);
                }
                else
                {
                    sphereRB.AddForce(sphereRB.velocity * 0.25f, ForceMode.Acceleration);
                }
                drifting = true;
            }
            else
            {    
                drifting = false;
            }
        }
        // 
        if (sprintInput)
        {
            boostForce = maxBoost;
        }
        else
        {
            boostForce = 0;
        }
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
        Debug.DrawRay(fRWheelOffset.position, Vector3.down, frontRWheel ? Color.green :Color.red);
        Debug.DrawRay(fLWheelOffset.position, Vector3.down, frontLWheel ? Color.green :Color.red);
        Debug.DrawRay(rRWheelOffset.position, Vector3.down, rearRWheel ? Color.green :Color.red);
        Debug.DrawRay(rLWheelOffset.position, Vector3.down, rearLWheel ? Color.green :Color.red);

        frontRWheel = Physics.Raycast(fRWheelOffset.position, Vector3.down, out hit, 1.0f);
        frontLWheel = Physics.Raycast(fLWheelOffset.position, Vector3.down, out hit, 1.0f);
        rearRWheel = Physics.Raycast(rRWheelOffset.position, Vector3.down, out hit, 1.0f);
        rearLWheel = Physics.Raycast(rLWheelOffset.position, Vector3.down, out hit, 1.0f);
    }
}