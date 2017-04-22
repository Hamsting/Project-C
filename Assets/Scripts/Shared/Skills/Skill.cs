using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
	/*
	public static readonly int PASSIVE_SKILL	= 0;
	public static readonly int MOVING_SKILL		= 1;
	public static readonly int FIRST_SKILL		= 2;
	public static readonly int SECOND_SKILL		= 3;
	public static readonly int THIRD_SKILL		= 4;
	*/

	public int faction = 0;
	public Unit owner;
	public bool useEffect = true;

	protected Animator anim;



	public virtual void Initialize()
	{
		if (useEffect)
		{
			anim = this.GetComponent<Animator>();
			if (anim == null)
				Debug.LogError("Cannot Find Skill's Animator.");
		}
	}

	public virtual void Tick()
	{
		
	}

	public virtual void OnTurnStarted()
	{

	}

	public virtual void OnTurnEnded()
	{

	}

	public virtual void OnSkillStart()
	{

	}

	public virtual void OnSkillEnd()
	{
		Destroy(this.gameObject);
	}

	public virtual void OnSkillEvent(int _n)
	{

	}
}
