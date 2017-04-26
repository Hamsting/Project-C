using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
	private static readonly Vector2 ZEROPOS = new Vector2(-6.494f, 3.264f);
	private static readonly float FIELD_WIDTH = 3.107f;
	private static readonly float FIELD_HEIGHT = -1.537f;
	private static readonly float UNIT_PADDING_X = 0.657f;
	private static readonly float UNIT_PADDING_Y = -1.181f;
	private Vector3 startPos;



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

	public Vector3 CalculateGridPos(Unit _u)
	{
		Vector3 v = _u.transform.position;
		v.x = ZEROPOS.x + FIELD_WIDTH * _u.fieldPos.x;
		v.y = ZEROPOS.y + FIELD_HEIGHT * _u.fieldPos.y + UNIT_PADDING_Y;
		v.z -= _u.fieldPos.y * 0.1f;
		if (_u.faction == GameManager.FACTION_BLUE)
			v.x += UNIT_PADDING_X;
		else if (_u.faction == GameManager.FACTION_RED)
			v.x += FIELD_WIDTH - UNIT_PADDING_X;
		else
			v.x += FIELD_WIDTH * 0.5f;
		
		return v;
	}
}
