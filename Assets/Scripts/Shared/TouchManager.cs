using UnityEngine;

public class TouchManager : MonoBehaviour
{
	private static TouchManager _instance;
	public static TouchManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = GameObject.FindObjectOfType<TouchManager>();
				if (_instance == null)
					Debug.LogError("Error! : TouchManager Instance is null.");
			}
			return _instance;
		}
	}

	public bool touchable = false;
	public Vector3 screenPosition = Vector3.zero;
	public Vector3 worldPosition = Vector3.zero;
	public Vector3 firstScreenPosition = Vector3.zero;
	public Vector3 firstWorldPosition = Vector3.zero;
	public TouchState touchState = TouchState.None;

	private float zoomingLastDistance = -1f;
	private int mainTouchID = 0;

	private bool initialized = false;



	public void Initialize()
	{
		if (!initialized)
		{
			touchable = Input.touchSupported;
			DontDestroyOnLoad(this.gameObject);
			initialized = true;
		}
	}

	public void Tick()
	{
		if (!initialized)
			return;

		// 터치 기기의 경우. (Android, iOS, ...)
		if (touchable)
		{
			if (Input.touchCount > 0)
			{
				Touch t = Input.GetTouch(0);
				screenPosition = t.position;
				worldPosition = CameraManager.Instance.touchCam.ScreenToWorldPoint(screenPosition);
				
				if (touchState == TouchState.None)
				{
					touchState = TouchState.Press;
					firstScreenPosition = screenPosition;
					firstWorldPosition = worldPosition;
				}
				else if (touchState == TouchState.Press)
					touchState = TouchState.Stay;
				if (t.fingerId != mainTouchID)
				{
					firstScreenPosition = screenPosition;
					firstWorldPosition = worldPosition;
				}
				mainTouchID = t.fingerId;
			}
			else
			{
				if (touchState == TouchState.Stay)
					touchState = TouchState.Release;
				else
					touchState = TouchState.None;
			}
		}
		// 그 외의 경우. (Windows, 유니티 에디터, ...)
		else
		{
			if (Input.GetMouseButton(0))
			{
				screenPosition = Input.mousePosition;
				worldPosition = CameraManager.Instance.touchCam.ScreenToWorldPoint(screenPosition);

				if (touchState == TouchState.None)
				{
					touchState = TouchState.Press;
					firstScreenPosition = screenPosition;
					firstWorldPosition = worldPosition;
				}
				else if (touchState == TouchState.Press)
					touchState = TouchState.Stay;
			}
			else
			{
				if (touchState == TouchState.Stay)
					touchState = TouchState.Release;
				else
					touchState = TouchState.None;
			}
		}
	}

	public GameObject GetTouchedGameObject()
	{
		if (touchState == TouchState.None)
			return null;

		Ray ray = CameraManager.Instance.touchCam.ScreenPointToRay(screenPosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1, QueryTriggerInteraction.Ignore))
		{
			return hit.transform.gameObject;
		}
		else
			return null;
	}

	public GameObject[] GetTouchedGameObjects()
	{
		if (touchState == TouchState.None)
			return new GameObject[0];

		Ray ray = CameraManager.Instance.touchCam.ScreenPointToRay(screenPosition);
		RaycastHit[] hits = Physics.RaycastAll(ray, Mathf.Infinity, 1, QueryTriggerInteraction.Ignore);
		GameObject[] objs = new GameObject[hits.Length];
		for (int i = 0; i < hits.Length; ++i)
		{
			objs[i] = hits[i].transform.gameObject;
		}
		return objs;
	}

	public float GetZoomingPercent()
	{
		float delta = 0f;
		if (touchable)
		{
			if (Input.touchCount >= 2)
			{
				Touch st = Input.GetTouch(1);
				float zoomingDistance = Vector3.Distance(screenPosition, st.position);
				if (zoomingLastDistance <= 0f)
					zoomingLastDistance = zoomingDistance;
				float screenWidth = Screen.width;
				delta = (zoomingLastDistance - zoomingDistance) / screenWidth;
				zoomingLastDistance = zoomingDistance;
			}
			else
				zoomingLastDistance = -1f;
		}
		else
		{
			delta = Input.GetAxis("Mouse ScrollWheel") * -0.2f;
        }
		return delta;
	}
}

public enum TouchState
{
	None = 0,
	Press = 1,
	Stay = 2,
	Release = 3,
}