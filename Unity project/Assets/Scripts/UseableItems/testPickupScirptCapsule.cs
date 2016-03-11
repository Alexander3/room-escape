using UnityEngine;
using System.Collections;

public class testPickupScirptCapsule : MonoBehaviour, Pickable {
	public Vector3 handShift = new Vector3(1, 1, 1);
	public Quaternion rotationShift = new Quaternion(30, 30, 30, 1);

	public Vector3 HandShift { get { return handShift; } }
	public Quaternion RotationShift { get { return rotationShift; } }

	public float highlightIntensity = 0.7f;
	public float HighlightIntensity { get { return highlightIntensity; } }

	public void Use() { }


	public void HighlightItem()
	{
		
		Renderer rend = GetComponent<Renderer>();
		rend.material.SetColor("_EmissionColor", Color.white * highlightIntensity);
	}
	public void UnHighlightItem()
	{
		Renderer rend = GetComponent<Renderer>();
		rend.material.SetColor("_EmissionColor", Color.white * 0.1f);
	}
}
