﻿using UnityEngine;
using System.Collections;

public class ColorScreen : Useable
{
	public delegate void ScreenUse(short id, Color c);
	public static event ScreenUse ScreenUsed;
	public short id;

	public Color[] colors = { Color.red, Color.blue, Color.green, Color.yellow, Color.magenta };
	private short cid = 0;
	override protected void doUse()
	{    
		_material.SetColor ("_Color", colors[cid]);
		if (ScreenUsed != null)
			ScreenUsed (id, colors [cid]);
		if (++cid == colors.Length)
			cid = 0;
	}
}

