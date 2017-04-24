using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FieldState
{
	public static readonly int NORMAL			= 0x00000000;
	public static readonly int BLOCK_BLUE		= 0x00000010;
	public static readonly int BLOCK_RED		= 0x00000020;

	public int state = 0x00000000;
	public Unit blueUnit;
	public Unit redUnit;



	public bool CanMove(Unit _u)
	{
		if (_u.faction == GameManager.FACTION_BLUE)
		{
			if (blueUnit != null)
				return false;
			if ((state & BLOCK_BLUE) == 1)
				return false;
		}
		else if (_u.faction == GameManager.FACTION_RED)
		{
			if (redUnit != null)
				return false;
			if ((state & BLOCK_RED) == 1)
				return false;
		}
		return true;
	}

	public void AddState(int _state)
	{
		if ((state & _state) == 0)
			state += _state;
	}

	public void RemoveState(int _state)
	{
		if ((state & _state) == 1)
			state -= _state;
	}
}
