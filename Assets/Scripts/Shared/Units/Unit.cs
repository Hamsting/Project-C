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

	protected bool movable = true;
	protected bool attackable = true;



	public virtual void Initialize()
	{
		skills = new List<SkillUse>();
		buffs = new List<Buff>();
	}
	
	public virtual void Tick ()
	{
		hp = Mathf.Clamp(hp, 0, hpMax);

		for (int i = 0; i < skills.Count; ++i)
			skills[i].Tick();
		for (int i = 0; i < buffs.Count; ++i)
			buffs[i].Tick();
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
}