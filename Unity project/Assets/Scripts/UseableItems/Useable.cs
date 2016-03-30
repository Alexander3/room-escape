using UnityEngine;
using System.Collections;

public abstract class Useable : MonoBehaviour {
	public bool useOnlyOnce = false;
	public float highlightSpeed = 6f;
	public Color highlightColor = Color.white * 0.5f;
	protected bool _alreadyUsed = false;   
	protected bool _highlighting = false;
	protected bool _highlight = true;
	private Material _highlightedMaterial;
	private Material _material;
	private Material _originalMaterial;

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
		if (useOnlyOnce && _alreadyUsed)
			return;
		_highlighting = true;
		_highlight = true;
	}

	private void Highlight()
	{
		if (_highlight)
			_material.SetColor("_EmissionColor", Color.Lerp(_material.GetColor("_EmissionColor"), highlightColor, highlightSpeed * Time.deltaTime));
		else
			_material.SetColor("_EmissionColor", Color.Lerp(_material.GetColor("_EmissionColor"), Color.black, highlightSpeed * Time.deltaTime));
	}

	public void Use(){
		if (useOnlyOnce && _alreadyUsed)
			return;

		doUse();

		_alreadyUsed = true;
	}

	protected abstract void doUse();
}
