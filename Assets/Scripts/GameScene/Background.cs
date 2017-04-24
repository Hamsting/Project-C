using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{



	public void Initialize()
	{
		
	}

	public void Tick()
	{
		
	}

	public void OnMouseUp()
	{
		if (!CameraManager.Instance.cameraMoving)
			UIManager.Instance.skillPick.DisableSkillPick();
	}
}
