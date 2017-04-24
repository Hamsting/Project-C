using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{



	public void Initialize()
	{
		GameManager g = GameManager.Instance;
        this.GetComponent<SpriteRenderer>().color = g.stageInfo.gridColor;
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
