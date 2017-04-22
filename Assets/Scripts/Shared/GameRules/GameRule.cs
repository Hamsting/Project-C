using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRule
{



	public static GameRule GetGameRule(GameRules _type)
	{
		switch (_type)
		{
			case GameRules.DefaultRule:
				return new DefaultRule();
			default:
				return new DefaultRule();
		}
	}

	public virtual void Initialize()
	{
		
	}

	public virtual void Tick()
	{

	}

	public virtual void GameStart()
	{

	}
	
	public virtual void TurnEnd()
	{

	}
}

public enum GameRules
{
	DefaultRule = 0,
}
