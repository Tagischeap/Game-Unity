using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
	private Vector3 ResetCamera;
	private Vector3 Origin;
	private Vector3 Diference;
	private bool Drag=false;

	float cameraDistanceMax = 20f;
	float cameraDistanceMin = 5f;
	float cameraDistance = 10f;
	float scrollSpeed = 0.5f;

	public float orthoZoomSpeed = 0.05f;
	public float minZoom = 5.00f;
	public float maxZoom = 15.00f;

	void Start () {
		ResetCamera = Camera.main.transform.position;
		Input.simulateMouseWithTouches = false;
	}

	void LateUpdate () {
		Touch[] touches = Input.touches;

		if (Input.GetMouseButton (0) || touches.Length == 1) {
			Diference=(Camera.main.ScreenToWorldPoint (Input.mousePosition))- Camera.main.transform.position;
			if (Drag==false){
				Drag=true;
				Origin=Camera.main.ScreenToWorldPoint (Input.mousePosition);
			}
		} else {
			Drag=false;
		}
		if (Drag==true){
			Camera.main.transform.position = Origin-Diference;
		}
		/*
		//RESET CAMERA TO STARTING POSITION WITH RIGHT CLICK
		if (Input.GetMouseButton (1)) {
			Camera.main.transform.position=ResetCamera;
		}
		*/


		if (touches.Length == 2) {
			Touch touchOne = touches[0];
			Touch touchTwo = touches[1];

			Vector2 cameraViewsize = new Vector2 (Camera.main.pixelWidth, Camera.main.pixelHeight);

			Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
			Vector2 touchTwoPrevPos = touchTwo.position - touchTwo.deltaPosition;

			float prevTouchDeltaMag = (touchOnePrevPos - touchTwoPrevPos).magnitude;
			float touchDeltaMag = (touchOne.position - touchTwo.position).magnitude;

			float deltaMagDiff = prevTouchDeltaMag - touchDeltaMag;

			Camera.main.transform.position += Camera.main.transform.TransformDirection (
				(touchOnePrevPos + touchTwoPrevPos - cameraViewsize)
				* Camera.main.orthographicSize / cameraViewsize.y);

			Camera.main.orthographicSize += deltaMagDiff * orthoZoomSpeed;
			Camera.main.orthographicSize = Mathf.Clamp (Camera.main.orthographicSize, minZoom, maxZoom) - 0.001f;

			Camera.main.transform.position -= Camera.main.transform.TransformDirection (
				(touchOne.position + touchTwo.position - cameraViewsize)
				* Camera.main.orthographicSize / cameraViewsize.y);
		}

		var mouseWheel = Input.GetAxis("Mouse ScrollWheel");

		//scroll up
		if (mouseWheel > 0f)
		{
			Camera.main.orthographicSize = Mathf.Clamp (Camera.main.orthographicSize, minZoom, maxZoom) - scrollSpeed;
		}
		//scroll down
		else if (mouseWheel < 0f)
		{
			Camera.main.orthographicSize = Mathf.Clamp (Camera.main.orthographicSize, minZoom, maxZoom) + scrollSpeed;
		}

	}
}