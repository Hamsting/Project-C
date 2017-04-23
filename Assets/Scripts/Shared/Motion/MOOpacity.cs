using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MOOpacity : Motion
{
	private float start;
	private float end;
	private bool changeChild = true;
	private RendererType type;
	private List<SpriteRenderer> spr;
	private List<Image> img;



	public void Initialize(GameObject _target, RendererType _type, float _endTime, float _start, float _end, float _ease = 0.0f, bool _changeChild = true)
	{
		base.Initialize(_target, _endTime, _ease);
		start = _start;
		end = _end;
		changeChild = _changeChild;
		type = _type;

		if (type == RendererType.SpriteRenderer)
			GetSpriteRenderers();
		else if (type == RendererType.Image)
			GetImages();
		else
			base.onMotionEnd();
	}

	protected override void Update()
	{
		base.Update();

		float o = Mathf.Lerp(start, end, ac.Evaluate(Mathf.Clamp01(timer / endTime)));
		if (type == RendererType.SpriteRenderer)
			TickSpriteRenderers(o);
		else if (type == RendererType.Image)
			TickImages(o);
	}

	private void GetSpriteRenderers()
	{
		spr = new List<SpriteRenderer>();
		SpriteRenderer tspr = target.GetComponent<SpriteRenderer>();
		if (tspr != null)
			spr.Add(tspr);

		if (changeChild)
		{
			SpriteRenderer[] csprs = target.GetComponentsInChildren<SpriteRenderer>();
			if (csprs.Length != 0)
				spr.AddRange(csprs);
		}
	}

	private void GetImages()
	{
		img = new List<Image>();
		Image timg = target.GetComponent<Image>();
		if (timg != null)
			img.Add(timg);

		if (changeChild)
		{
			Image[] cimgs = target.GetComponentsInChildren<Image>();
			if (cimgs.Length != 0)
				img.AddRange(cimgs);
		}
	}

	private void TickSpriteRenderers(float _o)
	{
		for (int i = 0; i < spr.Count; ++i)
		{
			Color c = spr[i].color;
			c.a = _o;
			spr[i].color = c;
		}
	}

	private void TickImages(float _o)
	{
		for (int i = 0; i < img.Count; ++i)
		{
			Color c = img[i].color;
			c.a = _o;
			img[i].color = c;
		}
	}

	public enum RendererType
	{
		None = 0,
		SpriteRenderer = 1,
		Image = 2,
	}
}
