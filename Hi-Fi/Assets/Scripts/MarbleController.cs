using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

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
	private PlayerInput _playerInput;
	private StarterAssetsInputs _input;
    public GameObject player;


    [Header("Too many Variables")]
    private bool jumpInput, sprintInput;
    private float currentForce;
    public float maxForce, forwardForce, steerForce;
    public float jumpForce, maxBoost, boostForce;
    public bool drifting = false, boosting;

    public bool frontRWheel, frontLWheel;
    public bool rearRWheel, rearLWheel;
    public bool Parked = true;

    [SerializeField] private Transform fRWheelOffset, fLWheelOffset;
    [SerializeField] private Transform rRWheelOffset, rLWheelOffset;

    [SerializeField] private BoxCollider boxColider;

		private bool IsCurrentDeviceMouse => _playerInput.currentControlScheme == "KeyboardMouse";

		[Header("Cinemachine")]
		[Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
		public GameObject CinemachineCameraTarget;
		[Tooltip("How far in degrees can you move the camera up")]
		public float TopClamp = 70.0f;
		[Tooltip("How far in degrees can you move the camera down")]
		public float BottomClamp = -30.0f;
		[Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
		public float CameraAngleOverride = 0.0f;
		[Tooltip("For locking the camera position on all axis")]
		public bool LockCameraPosition = false;

		// cinemachine
		private float _cinemachineTargetYaw;
		private float _cinemachineTargetPitch;
		private GameObject _mainCamera;
		private const float _threshold = 0.01f;

    // Start is called before the first frame update
    private void Start() 
    {
        //dechilds model
        //sphereRB.transform.parent = null;
        Physics.IgnoreCollision(sphere.GetComponent<SphereCollider>(), boxColider);
        //Physics.IgnoreCollision(sphere.GetComponent<SphereCollider>(), col);

		_input = player.GetComponent<StarterAssetsInputs>();
		_playerInput = player.GetComponent<PlayerInput>();
    }
    private void Update()
    {
        if (!Parked)
        {
            GetInput();
        }
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
                    //sphereRB.AddForce(sphereRB.velocity * 0.25f, ForceMode.Acceleration);
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
    private void LateUpdate()
    {
		CameraRotation();
    }
    public void GetInput()
    {
        //horizontalInput = Input.GetAxis(HORIZONTAL);
        //verticalInput = Input.GetAxis(VERTICAL);
        //jumpInput = Input.GetButton("Jump");
        //sprintInput = Input.GetButton("Fire3");

        horizontalInput = _input.move.x;
        verticalInput = _input.move.y;
        //jumpInput = Input.GetButton("Jump");
        //sprintInput = Input.GetButton("Fire3");
    }
	private void CameraRotation()
	{
		// if there is an input and camera position is not fixed
		if (_input.look.sqrMagnitude >= _threshold && !LockCameraPosition)
		{
			//Don't multiply mouse input by Time.deltaTime;
			float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;
			
			_cinemachineTargetYaw += _input.look.x * deltaTimeMultiplier;
			_cinemachineTargetPitch += _input.look.y * deltaTimeMultiplier;
		}
		// clamp our rotations so our values are limited 360 degrees

		_cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
		_cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);
		// Cinemachine will follow this target
        
		CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride, _cinemachineTargetYaw, 0.0f);
		}
	private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
	{
		if (lfAngle < -360f) lfAngle += 360f;
		if (lfAngle > 360f) lfAngle -= 360f;
		return Mathf.Clamp(lfAngle, lfMin, lfMax);
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