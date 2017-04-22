using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultRule : GameRule
{



	public override void Initialize()
	{
		GameManager.Instance.turn = 0;
	}

	public override void Tick()
	{
		
	}

	public override void GameStart()
	{
		GameManager.Instance.turn = GameManager.FACTION_BLUE;
	}

	public override void TurnEnd()
	{
		GameManager g = GameManager.Instance;
		int currentTurn = g.turn;
		int enemyTurn = 3 - currentTurn;
		List<Unit> endUnits = g.FindUnitsWithFaction(currentTurn);
		List<Unit> startUnits = g.FindUnitsWithFaction(enemyTurn);
		List<Skill> endSkills = g.FindSkillsWithFaction(currentTurn);
		List<Skill> startSkills = g.FindSkillsWithFaction(enemyTurn);

		for (int i = 0; i < endUnits.Count; ++i)
			endUnits[i].OnTurnEnded();
		for (int i = 0; i < startUnits.Count; ++i)
			startUnits[i].OnTurnStarted();
		for (int i = 0; i < endSkills.Count; ++i)
			endSkills[i].OnTurnEnded();
		for (int i = 0; i < startSkills.Count; ++i)
			startSkills[i].OnTurnStarted();

		g.turn = enemyTurn;
	}
}
