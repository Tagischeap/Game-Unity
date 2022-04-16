using UnityEngine;
using System.Collections;

public class TouchCamera : MonoBehaviour {

	public float moveSensitivityX = 0.0025f;
	public float moveSensitivityY = 0.0025f;
	public bool updateZoomSensitivity = true;
	public float orthoZoomSpeed = 0.05f;
	public float minZoom = 3.5f;
	public float maxZoom = 20.0f;
	public bool invertMoveX = false;
	public bool invertMoveY = false;
	public float mapWidth = 30f;
	public float mapHeight = 30f;
	public float inertiaDuration = 0.25f;

	Vector3 statZ = new Vector3 (0, 0, -10);
	Vector3 camDefPos;


	private Camera _camera;

	private float minX, maxX, minY, maxY;
	private float horizontalExtent, verticalExtent;

	private float scrollVelocity = 0.0f;
	private float timeTouchPhaseEnded;
	private Vector2 scrollDirection = Vector2.zero;

	void Start () 
	{
		_camera = Camera.main;

		maxZoom = 0.5f * (mapWidth / _camera.aspect);

		if (mapWidth > mapHeight)
			maxZoom = 0.5f * mapHeight;

		if (_camera.orthographicSize > maxZoom)
			_camera.orthographicSize = maxZoom;

		camDefPos = Camera.main.transform.position;
		CalculateLevelBounds ();
	}

	void Update () 
	{

		if (updateZoomSensitivity)
		{
			moveSensitivityX = _camera.orthographicSize / 5.0f;
			moveSensitivityY = _camera.orthographicSize / 5.0f;
		}

		Touch[] touches = Input.touches;

		if (touches.Length < 1)
		{
			//if the camera is currently scrolling
			if (scrollVelocity != 0.0f)
			{
				//slow down over time
				float t = (Time.time - timeTouchPhaseEnded) / inertiaDuration;
				float frameVelocity = Mathf.Lerp (scrollVelocity, 0.0f, t);
				_camera.transform.position += -new Vector3(scrollDirection.x, scrollDirection.y, 0)
					* (frameVelocity * 0.05f) * Time.deltaTime;

				if (t >= 1.0f)
					scrollVelocity = 0.0f;
			}
		}

		if (touches.Length > 0)
		{

			//Single touch (move)
			if (touches.Length == 1)
			{
				if (touches[0].phase == TouchPhase.Began)
				{
					scrollVelocity = 0.0f;
				}
				else if (touches[0].phase == TouchPhase.Moved)
				{
					Vector2 delta = touches[0].deltaPosition;

					float positionX = delta.x * moveSensitivityX * Time.deltaTime;
					positionX = invertMoveX ? positionX : positionX * -1;

					float positionY = delta.y * moveSensitivityY * Time.deltaTime;
					positionY = invertMoveY ? positionY : positionY * -1;

					_camera.transform.position += new Vector3 (positionX, positionY, 0);

					scrollDirection = touches[0].deltaPosition.normalized;
					scrollVelocity = touches[0].deltaPosition.magnitude / touches[0].deltaTime;


					if (scrollVelocity <= 100)
						scrollVelocity = 0;
				}
				else if (touches[0].phase == TouchPhase.Ended)
				{
					timeTouchPhaseEnded = Time.time;
				}
			}


			//Double touch (zoom)
			if (touches.Length == 2)
			{
				Vector2 cameraViewsize = new Vector2 (_camera.pixelWidth, _camera.pixelHeight);

				Touch touchOne = touches[0];
				Touch touchTwo = touches[1];

				Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
				Vector2 touchTwoPrevPos = touchTwo.position - touchTwo.deltaPosition;

				float prevTouchDeltaMag = (touchOnePrevPos - touchTwoPrevPos).magnitude;
				float touchDeltaMag = (touchOne.position - touchTwo.position).magnitude;

				float deltaMagDiff = prevTouchDeltaMag - touchDeltaMag;

				_camera.transform.position += _camera.transform.TransformDirection (
					(touchOnePrevPos + touchTwoPrevPos - cameraViewsize)
					* _camera.orthographicSize / cameraViewsize.y);

				_camera.orthographicSize += deltaMagDiff * orthoZoomSpeed;
				_camera.orthographicSize = Mathf.Clamp (_camera.orthographicSize, minZoom, maxZoom) - 0.001f;

				_camera.transform.position -= _camera.transform.TransformDirection (
					(touchOne.position + touchTwo.position - cameraViewsize)
					* _camera.orthographicSize / cameraViewsize.y);

				CalculateLevelBounds ();
			}
		}
	}
		

	void CalculateLevelBounds ()
	{
		verticalExtent = _camera.orthographicSize * Screen.width / Screen.height;
		horizontalExtent = _camera.orthographicSize * Screen.width / Screen.height;
		minX = horizontalExtent - mapWidth / 2.0f;
		maxX = mapWidth / 2.0f - horizontalExtent;
		minY = verticalExtent - mapHeight / 2.0f;
		maxY = mapHeight / 2.0f - verticalExtent;
	}

	void CamOutOfBoundsCheck(){
		if (Camera.main.transform.position.x > maxX || Camera.main.transform.position.y > maxY
			||Camera.main.transform.position.x < minX || Camera.main.transform.position.y < minY
			||Camera.main.transform.position.z != -10) {
			/*Camera.main.transform.position = new Vector3 (Camera.main.transform.position.x, Camera.main.transform.position.y,
				-10);*/
			Camera.main.transform.position = camDefPos;
		}
	}

	void LateUpdate ()
	{
		Vector3 limitedCameraPosition = _camera.transform.position;
		limitedCameraPosition.x = Mathf.Clamp (limitedCameraPosition.x, minX, maxX);
		limitedCameraPosition.y = Mathf.Clamp (limitedCameraPosition.y, minY, maxY);
		_camera.transform.position = limitedCameraPosition;
		CamOutOfBoundsCheck ();
	}

	void OnDrawGizmos ()
	{
		Gizmos.DrawWireCube (Vector3.zero, new Vector3 (mapWidth, mapHeight, 0));
	}
}