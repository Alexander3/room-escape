using UnityEngine;
using System.Collections;

public class OnlyPickable : MonoBehaviour, Pickable
{
	[SerializeField]
	private float _highlightIntensity = 0.7f;
	public float HighlightIntensity { get { return _highlightIntensity; } }

	public Vector3 handShift = new Vector3(1, 1, 1);
	public Quaternion rotationShift = new Quaternion(30, 30, 30, 1);

	public Vector3 HandShift { get { return handShift; } }
	public Quaternion RotationShift { get { return rotationShift; } }


	public Vector3 MoveShift;
	public float MoveSpeed = 6f;
	public bool UseOnlyOnce = false;
	private bool _alreadyUsed = false;

	public Color HighlightColor;
	public float HighlightSpeed = 6f;

	private bool _movingToTarget = false;
	private bool _moving;

	private bool _highlighting = false;
	private bool _highlight = true;

	private Vector3 _originPosition;
	private Material _originalMaterial;
	private Material _highlightedMaterial;
	private Material _material;
	private Vector3 _objectSpaceMoveToPosition;

	public void Start()
	{
		_originPosition = this.transform.position;

		_objectSpaceMoveToPosition = _originPosition + MoveShift;

		_material = GetComponent<Renderer>().material;
		_material.SetColor("_EmissionColor", HighlightColor * 0.01f);

		_originalMaterial = new Material(_material);

		_highlightedMaterial = new Material(_material);
		_highlightedMaterial.SetColor("_EmissionColor", HighlightColor * HighlightIntensity);
	}

	public void Use()
	{

	}

	public void Update()
	{

		if (_highlighting)
			Highlight();


#if UNITY_EDITOR
        // Refresh color when debugging to see live changes
        _highlightedMaterial.SetColor("_EmissionColor", HighlightColor * HighlightIntensity);
#endif
	}

	public void HighlightItem()
	{
		if (UseOnlyOnce && _alreadyUsed)
			return;

		_highlighting = true;
		_highlight = true;
	}

	public void UnHighlightItem()
	{
		_highlighting = true;
		_highlight = false;
	}



	private void Highlight()
	{
		if (_highlight)
			_material.Lerp(_material, _highlightedMaterial, HighlightSpeed * Time.deltaTime);
		else
			_material.Lerp(_material, _originalMaterial, HighlightSpeed * Time.deltaTime);
	}
}
