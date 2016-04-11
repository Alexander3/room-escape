﻿using UnityEngine;
using System.Collections;

public abstract class Useable : MonoBehaviour {
	public bool UseOnlyOnce = false;
	public float HighlightSpeed = 6f;
	public Color AcceptColor = Color.white * 0.5f;
	public static Color RejectColor = Color.red * 0.5f;
	public bool CanBeUsed = true;

	protected bool _alreadyUsed = false;   
	protected bool _highlighting = false;
	protected bool _highlight = true;
	protected Material _material;
	private Color _highlightColor;

	protected void Start()
	{
		_material = GetComponent<Renderer>().material;
		_material.EnableKeyword ("_EMISSION");				
	}

	protected void Update()
	{
		if (_highlighting)
			Highlight();
	}

	public void UnHighlightItem()
	{
		_highlighting = true;
		_highlight = false;
	}

	public void HighlightItem()
	{
		if (UseOnlyOnce && _alreadyUsed)
			return;
		_highlighting = true;
		_highlight = true;
		_highlightColor = CanBeUsed ? AcceptColor : RejectColor;
	}

	private void Highlight()
	{
		Color color = _material.GetColor ("_EmissionColor");

		if (_highlight)
			color = Color.Lerp (color, _highlightColor, HighlightSpeed * Time.deltaTime);
		else
			color = Color.Lerp (color, Color.black, HighlightSpeed * Time.deltaTime);
		
		_material.SetColor ("_EmissionColor", color);
		if (color == Color.black || color == _highlightColor)
			_highlighting = false;
	}

	public void Use(){
		if (UseOnlyOnce && _alreadyUsed || !CanBeUsed)
			return;

		doUse();
		_alreadyUsed = true;
	}

	protected abstract void doUse();
}
