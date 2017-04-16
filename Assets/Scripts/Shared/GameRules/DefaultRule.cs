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
}
