using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
	public bool acted = false;
	public int faction = 0;
	public int hp = 1000;
	public int hpMax = 1000;
	public int attackPower = 100;
	public int defensePower = 100;
	public float criticalChance = 5.0f;
	public float criticalPower = 150.0f;
	public List<SkillUse> skills;
	public List<Buff> buffs;
	public Vector2 center;
	public FieldPos fieldPos;

	protected bool movable = true;
	protected bool attackable = true;
	protected BoxCollider2D col;



	public virtual void Initialize()
	{
		skills = new List<SkillUse>();
		buffs = new List<Buff>();
		col = this.GetComponent<BoxCollider2D>();
		fieldPos = new FieldPos(0, 0);
		SetGridPos();
    }

	public virtual void Initialize(int _fx, int _fy)
	{
		Initialize();
		fieldPos = new FieldPos(_fx, _fy);
		SetGridPos();
	}
	
	public virtual void Tick ()
	{
		hp = Mathf.Clamp(hp, 0, hpMax);

		for (int i = 0; i < skills.Count; ++i)
			skills[i].Tick();
		for (int i = 0; i < buffs.Count; ++i)
			buffs[i].Tick();
		center = this.transform.TransformPoint(col.offset);
	}

	public void OnTurnStarted()
	{
		for (int i = 0; i < skills.Count; ++i)
			skills[i].OnTurnStarted();
		for (int i = 0; i < buffs.Count; ++i)
			buffs[i].OnTurnStarted();
	}

	public void OnTurnEnded()
	{
		for (int i = 0; i < skills.Count; ++i)
			skills[i].OnTurnEnded();
		for (int i = 0; i < buffs.Count; ++i)
			buffs[i].OnTurnEnded();
	}

	public void Move()
	{
		MOMove m = this.gameObject.AddComponent<MOMove>();
		m.Initialize(this.gameObject, 2.0f, this.transform.position, this.transform.position + new Vector3(2f, 2f, 0f));
	}

	public void UseSkill(int _number)
	{
		if (_number < 0 || _number > 4)
		{
			Debug.LogError("Wrong Number : " + _number);
			return;
		}
		skills[_number].UseSkill();
	}

	public int GetSkillUsable(int _number)
	{
		if (_number < 0 || _number > 4)
		{
			Debug.LogError("Wrong Number : " + _number);
			return -1;
		}
		return skills[_number].Usable();
	}

	public void OnMouseUp()
	{
		if (!CameraManager.Instance.cameraMoving && !CameraManager.Instance.cameraZooming)
			UIManager.Instance.skillPick.ToggleSkillPick(this);
	}
	
	public void SetGridPos()
	{
		this.transform.position = GameManager.Instance.grid.CalculateGridPos(this);
		if (faction == GameManager.FACTION_RED)
			this.transform.localScale = new Vector3(-1f, 1f, 1f);
		else
			this.transform.localScale = new Vector3(1f, 1f, 1f);
	}

	public void SetFieldPos(FieldPos _fpos)
	{
		fieldPos = _fpos;
		SetGridPos();
	}
}