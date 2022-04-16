using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * https://www.youtube.com/watch?v=LbDQHv9z-F0
 */

public class CameraFollow : MonoBehaviour
{
    public float CameraMoveSpeed = 120.0f;
    public GameObject CameraFollowObj;
    Vector3 FollowPOS;
    public float clampAngle = 80.0f;
    public float inputSensitivity = 150.0f;
    public GameObject CameraObj;
    public GameObject PlayerObj;
    public float camDistanceXToPlayer;
    public float camDistanceYToPlayer;
    public float camDistanceZToPlayer;
    public float mouseX;
    public float mouseY;
    public float finalInputX;
    public float finalInputZ;
    public float smoothX;
    public float smoothY;
    private float rotX = 0.0f;
    private float rotY = 0.0f;
    public bool invertX = false;
    public bool invertY = false;

    public string[] controls = {
    "joystick button 0", //0
    "joystick button 1", //1
    "joystick button 2", //2
    "joystick button 3", //3
    "joystick button 4", //4
    "joystick button 5", //5
    "joystick button 6", //6
    "joystick button 7", //7
    "joystick button 8", //8
    "joystick button 9", //9
    "joystick axis X", //10
    "joystick axis Y", //11
    "joystick axis 3", //12
    "joystick axis 4", //13
    "joystick axis 5", //14
    "joystick axis 6", //15
    "joystick axis 7" //16
    };

    //Use this for initialization
    void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotX = rot.x;
        rotY = rot.y;

        //Locks Cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    //Update is called once per frame
    void Update()
    {

        //Setup the rotation of the sticks and mouse
        //float inputX = Input.GetAxis("Horizontal");
        //float inputZ = Input.GetAxis("Vertical");
        float inputX = 0;
        float inputZ = 0;
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        finalInputX = inputX + mouseX;
        finalInputZ = inputZ + mouseY;
        if (invertX)
            finalInputX *= -1;
        if (invertY)
            finalInputZ *= -1;

        rotX += finalInputZ * inputSensitivity * Time.deltaTime;
        rotY += finalInputX * inputSensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        transform.rotation = localRotation;

    }

    void LateUpdate()
    {
        CameraUpdater();
    }

    void CameraUpdater()
    {
        //Set the targer object to follow
        Transform target = CameraFollowObj.transform;

        //Move towards the game object that is the target
        float step = CameraMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }

}
