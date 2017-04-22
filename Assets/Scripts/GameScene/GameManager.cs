using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	private static GameManager _instance;
	public static GameManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = GameObject.FindObjectOfType<GameManager>();
				if (_instance == null)
					Debug.LogError("Error! : GameManager Instance is null.");
			}
			return _instance;
		}
	}
	public static readonly float GRID_WIDTH = 0.846f;
	public static readonly int FACTION_NONE				= 0;
	public static readonly int FACTION_BLUE				= 1;
	public static readonly int FACTION_RED				= 2;
	public static readonly int FACTION_FRIENDLY_NATURAL	= 3;
	public static readonly int FACTION_HOSTILE_NATURAL	= 4;


	public int turn;
	public StageInfo stageInfo;
	public GameRule gameRule;
	public List<Unit> units;
	public List<Skill> skills;
	public GameObject field;
	public GameObject grid;
	public Text uiMapName;
	public Text uiAreaName;

	public bool debugMode = false;
	


	void Start()
	{
		if (debugMode)
		{
			stageInfo.mapNumber = 0001;
			stageInfo.mapName = "산 남부 중턱";
			stageInfo.areaName = "디스메어 산";
			stageInfo.gridColor = new Color(0f, 179f / 255f, 63f / 255f);
			stageInfo.gameRule = GameRules.DefaultRule;
		}
		else
			stageInfo = GlobalData.stageInfo;

		units = new List<Unit>();
		skills = new List<Skill>();
		gameRule = GameRule.GetGameRule(stageInfo.gameRule);
		grid.GetComponent<SpriteRenderer>().color = stageInfo.gridColor;
		uiMapName.text = stageInfo.mapName;
		uiAreaName.text = stageInfo.areaName;

		TouchManager.Instance.Initialize();
		CameraManager.Instance.Initialize();
		gameRule.Initialize();
	}
	
	void Update()
	{
		TouchManager.Instance.Tick();
		CameraManager.Instance.Tick();
		gameRule.Tick();
		for (int i = 0; i < units.Count; ++i)
			units[i].Tick();
		for (int i = 0; i < skills.Count; ++i)
			skills[i].Tick();
	}

	public List<Unit> FindUnitsWithFaction(int _faction)
	{
		List<Unit> ul = new List<Unit>();
		for (int i = 0; i < units.Count; ++i)
			if (units[i].faction == _faction)
				ul.Add(units[i]);

		return ul;
	}

	public List<Skill> FindSkillsWithFaction(int _faction)
	{
		List<Skill> sl = new List<Skill>();
		for (int i = 0; i < skills.Count; ++i)
			if (skills[i].faction == _faction)
				sl.Add(skills[i]);

		return sl;
	}
}
