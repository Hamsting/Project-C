using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPick : MonoBehaviour
{
	private static readonly float ANIMATION_TIME = 0.35f;
	private static readonly Vector3[] PICKPOS =
	{
		new Vector3(-170f,  170f, 0f), // 0
		new Vector3(   0f,  220f, 0f), // 1
		new Vector3( 170f,  170f, 0f), // 2
		new Vector3(-220f,    0f, 0f), // 3
		new Vector3(   0f,    0f, 0f), // 4
		new Vector3( 220f,    0f, 0f), // 5
		new Vector3(-170f, -170f, 0f), // 6
		new Vector3(   0f, -220f, 0f), // 7
		new Vector3( 170f, -170f, 0f), // 8
	};
	private static readonly int[,] PICKPOS_ZONE =
	{
		{ 5, 3, 0, 0 }, // 1
		{ 5, 3, 0, 0 }, // 2
		{ 8, 6, 1, 0 }, // 3
		{ 2, 8, 0, 6 }, // 4
	};

	public List<GameObject> skillPicks;
	public Unit target;

	private RectTransform tr;
	private bool enable = false;



	public void Initialize()
	{
		enable = false;
		tr = this.GetComponent<RectTransform>();
		for (int i = 0; i < skillPicks.Count; ++i)
		{
			skillPicks[i].transform.localPosition = PICKPOS[4];
			skillPicks[i].SetActive(false);
		}
	}

	public void Tick()
	{
		if (enable)
		{
			Vector3 pos = CameraManager.Instance.cam.WorldToScreenPoint(target.center);
			tr.anchoredPosition = pos;
		}
	}

	public void EnableSkillPick(Unit _target)
	{
		if (enable)
			return;

		target = _target;
		int sCount = target.skills.Count;
		for (int i = 0; i < sCount; ++i)
		{
			GameObject s = skillPicks[i];
			MOMove m = s.AddComponent<MOMove>();
			if (sCount == 1 && target.faction == GameManager.FACTION_RED)
				m.Initialize(s, ANIMATION_TIME - 0.05f, PICKPOS[4], PICKPOS[PICKPOS_ZONE[sCount - 1, 1]], 1.0f, false, true);
			else
				m.Initialize(s, ANIMATION_TIME - 0.05f, PICKPOS[4], PICKPOS[PICKPOS_ZONE[sCount - 1, i]], 1.0f, false, true);
			MOOpacity o = s.AddComponent<MOOpacity>();
			o.Initialize(s, MOOpacity.RendererType.Image, ANIMATION_TIME, 0.0f, 1.0f, 1.0f);
			s.SetActive(true);
		}
		enable = true;
		CameraManager.Instance.FocusUnit(target);
	}

	public void DisableSkillPick()
	{
		if (!enable)
			return;

		int sCount = target.skills.Count;
		for (int i = 0; i < sCount; ++i)
		{
			GameObject s = skillPicks[i];
			MOMove m = s.AddComponent<MOMove>();
			if (sCount == 1 && target.faction == GameManager.FACTION_RED)
				m.Initialize(s, ANIMATION_TIME - 0.05f, PICKPOS[PICKPOS_ZONE[sCount - 1, 1]], PICKPOS[4], 1.0f, false, true);
			else
				m.Initialize(s, ANIMATION_TIME - 0.05f, PICKPOS[PICKPOS_ZONE[sCount - 1, i]], PICKPOS[4], 1.0f, false, true);
			MOOpacity o = s.AddComponent<MOOpacity>();
			o.Initialize(s, MOOpacity.RendererType.Image, ANIMATION_TIME, 1.0f, 0.0f, 1.0f);
			if (i == 0)
				o.AddOnMotionEnd(new Motion.MotionEvent(DisableSkillPickCallBack));
		}
		enable = false;
	}

	private void DisableSkillPickCallBack()
	{
		if (!enable)
		{ 
			for (int i = 0; i < skillPicks.Count; ++i)
			{
				Motion.DeleteAllMotion(skillPicks[i]);
				skillPicks[i].SetActive(false);
			}
			target = null;
		}
	}

	private void ReEnableSkillPick(Unit _target)
	{
		for (int i = 0; i < skillPicks.Count; ++i)
		{
			Motion.DeleteAllMotion(skillPicks[i]);
			skillPicks[i].transform.localPosition = PICKPOS[4];
			skillPicks[i].SetActive(false);
		}
		enable = false;
		EnableSkillPick(_target);
	}

	public void ToggleSkillPick(Unit _target)
	{
		if (enable)
		{
			if (target != null && target != _target)
				ReEnableSkillPick(_target);
			else
				DisableSkillPick();
		}
		else
			EnableSkillPick(_target);
	}
}
