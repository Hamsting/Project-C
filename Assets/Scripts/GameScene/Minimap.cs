using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
	private static readonly Vector3 FLAG_ZEROPOS = new Vector3(13.7f, -362.2f, -0.01f);
	private static readonly float GRID_WIDTH = 76.7f;
	private static readonly float GRID_HEIGHT = -38.3f;
	private static readonly float GRID_PADDING = 16.3f;
	private static readonly Color32 FLAG_BLUE = new Color32(32, 200, 244, 255);
	private static readonly Color32 FLAG_RED = new Color32(255, 0, 0, 255);
	private static readonly Color32 FLAG_NATURAL = new Color32(244, 208, 61, 255);

	public List<GameObject> flags;

	private int flagCount = 0;



	public void Initialize()
	{
		if (flags == null)
			flags = new List<GameObject>();
		flagCount = 0;
	}

	public void Tick()
	{
	}

	public void UpdateFlag()
	{
		flagCount = 0;
		GameManager g = GameManager.Instance;
		for (int x = 0; x < 12; ++x)
		{
			for (int y = 0; y < 4; ++y)
			{
				FieldState fs = g.fieldState[x, y];
				if (fs.blueUnit != null)
					PlaceFlag(x, y, GameManager.FACTION_BLUE);
				if (fs.redUnit != null)
					PlaceFlag(x, y, GameManager.FACTION_RED);
			}
		}

		for (int i = flagCount; i < flags.Count; ++i)
			flags[i].SetActive(false);
	}

	private void PlaceFlag(int _x, int _y, int _faction)
	{
		if (flags.Count == 0)
		{
			Debug.LogError("Flag GameObject's Count is zero.");
			return;
		}

		if (flagCount >= flags.Count)
			flags.Add(Instantiate(flags[0], this.transform));

		GameObject f = flags[flagCount];

		float fx = _x * GRID_WIDTH;
		if (_faction == GameManager.FACTION_BLUE)
			fx += GRID_PADDING;
		else if (_faction == GameManager.FACTION_RED)
			fx += GRID_WIDTH - GRID_PADDING;
		else
			fx += GRID_WIDTH * 0.5f;
		float fy = _y * GRID_HEIGHT + GRID_HEIGHT * 0.5f;
		Vector3 pos = FLAG_ZEROPOS + new Vector3(fx, fy, 0f);
		f.transform.localPosition = pos;

		SpriteRenderer sr = f.GetComponent<SpriteRenderer>();
		if (_faction == GameManager.FACTION_BLUE)
			sr.color = FLAG_BLUE;
		else if (_faction == GameManager.FACTION_RED)
			sr.color = FLAG_RED;
		else
			sr.color = FLAG_NATURAL;

		f.SetActive(true);
		++flagCount;
    }

	public void OnMouseUp()
	{
		if (!CameraManager.Instance.cameraMoving)
			UIManager.Instance.skillPick.DisableSkillPick();
	}
}
