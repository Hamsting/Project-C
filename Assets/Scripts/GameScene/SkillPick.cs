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

	public List<GameObject> skills;

	private bool enable = false;



	public void Initialize()
	{
		enable = false;
		for (int i = 0; i < skills.Count; ++i)
		{
			skills[i].transform.localPosition = PICKPOS[4];
			skills[i].SetActive(false);
		}
	}

	public void Tick()
	{
		
	}

	public void EnableSkillPick()
	{
		if (enable)
			return;

		for (int i = 0; i < skills.Count; ++i)
		{
			GameObject s = skills[i];
			MOMove m = s.AddComponent<MOMove>();
			m.Initialize(s, ANIMATION_TIME - 0.05f, PICKPOS[4], PICKPOS[2], 1.0f, false);
			MOOpacity o = s.AddComponent<MOOpacity>();
			o.Initialize(s, MOOpacity.RendererType.Image, ANIMATION_TIME, 0.0f, 1.0f, 1.0f);
			s.SetActive(true);
		}
		enable = true;
	}

	public void DisableSkillPick()
	{
		if (!enable)
			return;

		for (int i = 0; i < skills.Count; ++i)
		{
			GameObject s = skills[i];
			MOMove m = s.AddComponent<MOMove>();
			m.Initialize(s, ANIMATION_TIME - 0.05f, PICKPOS[2], PICKPOS[4], 1.0f, false);
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
			for (int i = 0; i < skills.Count; ++i)
			{
				Motion.DeleteAllMotion(skills[i]);
				skills[i].SetActive(false);
			}
		}
	}
}
