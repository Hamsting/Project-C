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
	public FieldState[,] fieldState;
	public Grid grid;
	public Background background;
	public GameObject unitGroup;

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
		fieldState = new FieldState[12, 4];
		for (int x = 0; x < 12; ++x)
			for (int y = 0; y < 4; ++y)
				fieldState[x, y] = new FieldState();
		
		UIManager.Instance.Initialize();
		TouchManager.Instance.Initialize();
		CameraManager.Instance.Initialize();
		gameRule.Initialize();
		grid.Initialize();
		background.Initialize();
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
		UIManager.Instance.Tick();
		grid.Tick();
		background.Tick();

		if (Input.GetKeyDown(KeyCode.O))
			PushTestUnit();
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

	public void PushTestUnit()
	{
		int x = Random.Range(0, 12);
		int y = Random.Range(0, 4);
		int sc = Random.Range(1, 5);
		int f = (Random.Range(0, 2) == 0) ? FACTION_BLUE : FACTION_RED;

		if (!fieldState[x, y].CanAddUnit(f))
			return;

		UnitInfo info = UnitDB.Instance.FinUnitInfoWithID(0004);
		GameObject dupe = Instantiate(info.unitPrefab, unitGroup.transform);
		Unit u = dupe.GetComponent<Unit>();

		u.faction = f;
		u.Initialize(x, y);
        units.Add(u);
		fieldState[x, y].AddUnit(u);

		for (int i = 0; i < sc; ++i)
		{
			SkillUse s = new SkillUse();
			s.Initialize(999999);
			u.skills.Add(s);
		}

		UIManager.Instance.minimap.UpdateFlag();
	}
}
