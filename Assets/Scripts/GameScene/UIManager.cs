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
	public Minimap minimap;


	public void Initialize()
	{
		GameManager g = GameManager.Instance;
		uiMapName.text = g.stageInfo.mapName;
		uiAreaName.text = g.stageInfo.areaName;

		skillPick.Initialize();
		minimap.Initialize();
	}

	public void Tick()
	{
		skillPick.Tick();
		minimap.Tick();
	}

	public void OnSettingButtonReleased()
	{
		GameManager.Instance.PushTestUnit();
	}
}
