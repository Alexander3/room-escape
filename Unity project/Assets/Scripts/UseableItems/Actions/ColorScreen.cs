using UnityEngine;
using System.Collections;

public class ColorScreen : Useable
{
	public Color[] colors = { Color.red, Color.blue, Color.green, Color.yellow, Color.magenta };
	private short cid =0;
	override protected void doUse()
	{    
		_material.SetColor ("_Color", colors[cid++]);
		if (cid == colors.Length)
			cid = 0;
	}
}

