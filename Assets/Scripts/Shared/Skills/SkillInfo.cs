using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillInfo
{
	public int id = 000000;
	public int coolDown = 1;
	public int startDelay = 0;
	public bool passive = false;
	public bool useEffect = true;
	public string skillName = "";
	public string desc = "";
	public Sprite icon;
	public GameObject skillPrefab;
}
