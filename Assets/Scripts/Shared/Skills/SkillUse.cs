using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUse
{
	public static readonly int USABLE		= 0;
	public static readonly int COOLDOWN		= 1;
	public static readonly int STARTDELAY	= 2;

	public int skillID = 000000;
	public int coolDownLeft = 0;
	public int startDelayLeft = 0;
	public bool passive = false;
	public SkillInfo info;



	public void Initialize(int _skillID)
	{
		skillID = _skillID;
		info = SkillDB.Instance.FindSkillInfoWithID(skillID);
		if (info == null)
		{
			Debug.LogError("Cannot Find Skill : " + skillID);
			return;
		}
		startDelayLeft = info.startDelay;
		passive = info.passive;
	}

	public void Tick()
	{

	}

	public void OnTurnStarted()
	{
		if (startDelayLeft > 0)
			--startDelayLeft;
		if (coolDownLeft > 0)
			--coolDownLeft;
	}

	public void OnTurnEnded()
	{
		
	}

	public void UseSkill()
	{
		coolDownLeft = info.coolDown;

	}

	public int Usable()
	{
		if (startDelayLeft > 0)
			return STARTDELAY;
		if (coolDownLeft > 0)
			return COOLDOWN;

		return USABLE;
	}
}
