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

	public int turn;
	public StageInfo stageInfo;
	public GameRule gameRule;

	// 외부 환경.
	public GameObject grid;

	// 외부 UI.
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


	}
}
