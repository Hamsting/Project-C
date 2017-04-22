using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motion : MonoBehaviour
{
	private static readonly float EASE_FIX = 0.8f;

	public delegate void MotionEvent();

	protected float timer = 0f;
	protected float endTime = 0f;
	protected float ease = 0f;
	protected GameObject target;
	protected MotionEvent onMotionEnd;
	protected AnimationCurve ac;



	protected void Initialize(GameObject _target, float _endTime)
	{
		timer = 0f;
		endTime = _endTime;
		target = _target;
	}

	protected void Initialize(GameObject _target, float _endTime, float _ease)
	{
		timer = 0f;
		endTime = _endTime;
		target = _target;
		ease = _ease;

		float e = (ease + 1.0f) * EASE_FIX * 0.5f;
		Keyframe[] kf = new Keyframe[2];
		kf[0] = new Keyframe(0.0f, 0.0f);
		kf[1] = new Keyframe(1.0f, 1.0f);
		kf[0].outTangent = Mathf.Tan(90.0f * e / 180f * Mathf.PI);
		kf[1].inTangent = Mathf.Tan(90.0f * (EASE_FIX - e) / 180f * Mathf.PI);
		ac = new AnimationCurve(kf);
	}

	protected virtual void Update()
	{
		if (target == null)
		{
			Debug.LogError("Target is Null.");
			OnMotionEnd();
			return;
		}

		timer += Time.deltaTime;
		if (timer >= endTime)
			OnMotionEnd();
	}

	protected virtual void OnMotionEnd()
	{
		onMotionEnd();
		Destroy(this);
	}

	public void AddOnMotionEnd(MotionEvent _e)
	{
		onMotionEnd += _e;
	}
}
