using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
	public bool acted = false;
	public int maxHP = 1000;
	public int curHP = 1000;
	public int attackPower = 100;
	public int magicPower = 100;
	public int attackDefense = 10;
	public int magicDefense = 10;
	public int criticalChance = 100;
	public float criticalPower = 150.0f;
	public Skill passiveSkill;
	public Skill movingSkill;
	public Skill firstSkill;
	public Skill secondSkill;
	public Skill thirdSkill;



	public void Initialize()
	{
		
	}

	public void Tick()
	{
		if (passiveSkill != null)
			passiveSkill.Tick();
		if (movingSkill != null)
			movingSkill.Tick();
		if (firstSkill != null)
			firstSkill.Tick();
		if (secondSkill != null)
			secondSkill.Tick();
		if (thirdSkill != null)
			thirdSkill.Tick();
	}

	public void OnTurnStarted()
	{
		if (passiveSkill != null)
			passiveSkill.OnTurnStarted();
		if (movingSkill != null)
			movingSkill.OnTurnStarted();
		if (firstSkill != null)
			firstSkill.OnTurnStarted();
		if (secondSkill != null)
			secondSkill.OnTurnStarted();
		if (thirdSkill != null)
			thirdSkill.OnTurnStarted();
	}

	public void OnTurnEnded()
	{
		if (passiveSkill != null)
			passiveSkill.OnTurnEnded();
		if (movingSkill != null)
			movingSkill.OnTurnEnded();
		if (firstSkill != null)
			firstSkill.OnTurnEnded();
		if (secondSkill != null)
			secondSkill.OnTurnEnded();
		if (thirdSkill != null)
			thirdSkill.OnTurnEnded();
	}
}
