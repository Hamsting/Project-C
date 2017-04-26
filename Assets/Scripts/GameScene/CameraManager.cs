using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
	private static float DRAG_START_DISTANCE = 100f;
	private static CameraManager _instance;
	public static CameraManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = GameObject.FindObjectOfType<CameraManager>();
				if (_instance == null)
					Debug.LogError("Error! : CameraManager Instance is null.");
			}
			return _instance;
		}
	}

	public Camera cam;
	public Camera touchCam;
	public bool cameraMoving = false;
	public bool cameraZooming = false;
	public float camSize = 1f;
	public float camRatio = 1f;
	public Transform camLimitTL;
	public Transform camLimitBR;

	private float maxCamSize = 5f;
	private float minCamSize = 1f;
	private float zoomingSpeed = 7f;
	private Vector3 firstCamPos = Vector3.zero;
	private Vector3 targetCamPos = Vector3.zero;



	public void Initialize()
	{
		cam = this.GetComponent<Camera>();
		touchCam = Camera.main;
		camSize = cam.orthographicSize;
		camRatio = cam.aspect;
		targetCamPos = cam.transform.position;
	}

	public void Tick()
	{
		camSize = cam.orthographicSize;
		camRatio = cam.aspect;

		float zoomingPercent = TouchManager.Instance.GetZoomingPercent();
		if (zoomingPercent != 0f)
		{
			camSize = Mathf.Clamp(camSize + zoomingSpeed * zoomingPercent, minCamSize, maxCamSize);
			cam.orthographicSize = camSize;
			touchCam.orthographicSize = camSize;

			targetCamPos.x = Mathf.Clamp(targetCamPos.x, camLimitTL.transform.position.x + (camSize * camRatio), camLimitBR.transform.position.x - (camSize * camRatio));
			targetCamPos.y = Mathf.Clamp(targetCamPos.y, camLimitBR.transform.position.y + camSize, camLimitTL.transform.position.y - camSize);
			touchCam.transform.position = targetCamPos;
			cameraZooming = true;
		}
		else
			cameraZooming = false;

        if ((!TouchManager.Instance.touchable || (TouchManager.Instance.touchable && Input.touchCount == 1)) && 
			TouchManager.Instance.touchState == TouchState.Stay)
		{
			if (!cameraMoving)
			{
				float sDragDis = Vector3.Distance(TouchManager.Instance.screenPosition, TouchManager.Instance.firstScreenPosition);
				if (sDragDis >= DRAG_START_DISTANCE)
				{
					cameraMoving = true;
					firstCamPos = cam.transform.position;
				}
			}
			else
			{
				Vector3 camPos = targetCamPos - TouchManager.Instance.worldPosition + TouchManager.Instance.firstWorldPosition;
				camPos.x = Mathf.Clamp(camPos.x, camLimitTL.transform.position.x + (camSize * camRatio), camLimitBR.transform.position.x - (camSize * camRatio));
				camPos.y = Mathf.Clamp(camPos.y, camLimitBR.transform.position.y + camSize, camLimitTL.transform.position.y - camSize);
				camPos.z = -10f;
				targetCamPos = camPos;
				touchCam.transform.position = camPos;
			}
        }
		else if (TouchManager.Instance.touchState == TouchState.Release)
		{
			cameraMoving = false;
		}

		if (cam.transform.position != targetCamPos)
		{
			Vector3 move = Vector3.Lerp(cam.transform.position, targetCamPos, 0.25f);
			if (Vector3.Distance(move, targetCamPos) <= 0.005f)
				move = targetCamPos;
			cam.transform.position = move;
		}
	}

	public void FocusUnit(Unit _u)
	{
		Vector3 camPos = _u.center;
		camPos.x = Mathf.Clamp(camPos.x, camLimitTL.transform.position.x + (camSize * camRatio), camLimitBR.transform.position.x - (camSize * camRatio));
		camPos.y = Mathf.Clamp(camPos.y, camLimitBR.transform.position.y + camSize, camLimitTL.transform.position.y - camSize);
		camPos.z = -10f;
		targetCamPos = camPos;
		touchCam.transform.position = camPos;
	}
}
