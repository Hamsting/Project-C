using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDB : MonoBehaviour
{
	private static UnitDB _instance;
	public static UnitDB Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = GameObject.FindObjectOfType<UnitDB>();
				if (_instance == null)
					Debug.LogError("Error! : UnitDB Instance is null.");
			}
			return _instance;
		}
	}

	public List<UnitInfo> db;



	public UnitInfo FinUnitInfoWithID(int _id)
	{
		for (int i = 0; i < db.Count; ++i)
			if (db[i].id == _id)
				return db[i];

		return null;
	}
}
