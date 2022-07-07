using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;

public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private CinemachineVirtualCamera followVirtualCamera;
    [SerializeField] private float cameraFreeSensitivity;
    [SerializeField] private float cameraAimSensitivity;
    [SerializeField] private LayerMask aimColliderLayerMask;
    [SerializeField] private Transform pfObject;
    [SerializeField] private GameObject spawnObjectPosition;
    [SerializeField] private bool hitScan;

    [SerializeField] private Transform cursorPoint;
    [SerializeField] private Transform cursorSelection;


    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs starterAssetsInputs;

    private Vector3 mouseWorldPosition = Vector3.zero;

    private void Awake() 
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
    }
    private void Update() 
    {   
        //Aim Raycast
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        Transform hitTransform = null;
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            mouseWorldPosition = raycastHit.point;
            hitTransform = raycastHit.transform;

            if (hitTransform != null)
            {
                if (hitTransform.GetComponent<ActionTarget>() != null) 
                {
                    cursorSelection.transform.position = new Vector3(hitTransform.position.x, hitTransform.GetComponent<Collider>().bounds.max.y + 0.25f, hitTransform.position.z);
                }
                else
                {
                   cursorSelection.transform.position = new Vector3(0, 0.25f, 0) + cursorPoint.position;
                }
                //hitable
                cursorPoint.position = raycastHit.point;
            }
        }
        
        //Inputs

        if (starterAssetsInputs.aim)
        {
            //Aiming
            aimVirtualCamera.gameObject.SetActive(true);
            followVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.SetCameraSensitivity(cameraAimSensitivity);
            thirdPersonController.SetRotateOnMove(false);
            faceAim(Time.deltaTime * 20f);
        }
        else
        {
            //Not Aiming
            aimVirtualCamera.gameObject.SetActive(false);
            followVirtualCamera.gameObject.SetActive(true);
            thirdPersonController.SetCameraSensitivity(cameraFreeSensitivity);
            thirdPersonController.SetRotateOnMove(true);
        }

        if (starterAssetsInputs.action) 
        {
            faceAim(90f);
            Vector3 aimDir = (mouseWorldPosition - spawnObjectPosition.transform.position).normalized;
            if (hitScan)
            {
                if(hitTransform != null)
                {
                    //Hit Something
                    Instantiate(pfObject, mouseWorldPosition, Quaternion.LookRotation(aimDir, Vector3.up));
                }
            }
            else
            {
                Instantiate(pfObject, spawnObjectPosition.transform.position, Quaternion.LookRotation(aimDir, Vector3.up));
            }
            starterAssetsInputs.action = false;
        }
    }

    private void faceAim(float rate)
    {
            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
            transform.forward = Vector3.Lerp(transform.forward, aimDirection, rate);
    }
}
