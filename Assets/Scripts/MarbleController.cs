using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleController : MonoBehaviour
{
    public Rigidbody sphereRB;
    //public Rigidbody sphereRB;
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";
    private float horizontalInput;
    private float verticalInput;
    private float currentForce;
    public float maxForce;
    public float forwardForce;
    public float steerForce;
    // Start is called before the first frame update
    private void Start() 
    {
        //dechilds model
        sphereRB.transform.parent = null;
    }
    private void Update()
    {
        GetInput();
    }
    private void FixedUpdate() 
    {
        Debug.Log(maxForce + " : " + currentForce + " | " + forwardForce + ", " + verticalInput);
        // set model position to sphere
        transform.position = sphereRB.transform.position;
        //adjust speed for sphere
        currentForce = verticalInput;
        currentForce *= verticalInput != 0 ? forwardForce : maxForce;
        if (verticalInput != 0)
        {
            sphereRB.AddForce(transform.forward * currentForce, ForceMode.Acceleration);
        }
        //steer the sphere
        float rot = horizontalInput * steerForce * Time.deltaTime;
        transform.Rotate(0, rot, 0, Space.World);
        
    }
    public void GetInput()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
    }
}