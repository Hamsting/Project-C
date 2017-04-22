using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDB : MonoBehaviour
{
	private static SkillDB _instance;
	public static SkillDB Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = GameObject.FindObjectOfType<SkillDB>();
				if (_instance == null)
					Debug.LogError("Error! : SkillDB Instance is null.");
			}
			return _instance;
		}
	}

	public List<SkillInfo> db;



	public SkillInfo FindSkillInfoWithID(int _id)
	{
		for (int i = 0; i < db.Count; ++i)
			if (db[i].id == _id)
				return db[i];

		return null;
	}
}
