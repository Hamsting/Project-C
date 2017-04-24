using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitInfo
{
	public int id = 0000;
	public int hp = 1000;
	public int attackPower = 100;
	public int defensePower = 100;
	public float criticalChance = 5.0f;
	public float criticalPower = 150.0f;
	public List<int> skills;
	public GameObject unitPrefab;
}
