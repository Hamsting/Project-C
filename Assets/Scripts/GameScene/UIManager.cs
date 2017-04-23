using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	private static UIManager _instance;
	public static UIManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = GameObject.FindObjectOfType<UIManager>();
				if (_instance == null)
					Debug.LogError("Error! : UIManager Instance is null.");
			}
			return _instance;
		}
	}
	public Text uiMapName;
	public Text uiAreaName;
	public SkillPick skillPick;


	public void Initialize()
	{
		GameManager g = GameManager.Instance;
		uiMapName.text = g.stageInfo.mapName;
		uiAreaName.text = g.stageInfo.areaName;

		skillPick.Initialize();
	}

	public void Tick()
	{
		if (Input.GetKeyDown(KeyCode.O))
			skillPick.EnableSkillPick();
		if (Input.GetKeyDown(KeyCode.P))
			skillPick.DisableSkillPick();

		skillPick.Tick();
	}
}
