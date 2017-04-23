using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MOMove : Motion
{
	private Vector3 start;
	private Vector3 end;
	private bool worldPos;



	public void Initialize(GameObject _target, float _endTime, Vector3 _start, Vector3 _end, float _ease = 0.0f, bool _worldPos = true)
	{
		base.Initialize(_target, _endTime, _ease);
		start = _start;
		end = _end;
		worldPos = _worldPos;
	}

	protected override void Update()
	{
		base.Update();
		
		Vector3 pos = Vector3.Lerp(start, end, ac.Evaluate(Mathf.Clamp01(timer / endTime)));
		if (worldPos)
			target.transform.position = pos;
		else
			target.transform.localPosition = pos;
	}
}
