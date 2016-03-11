using UnityEngine;
using System.Collections;

public interface Useable {
	void Use();
	void UnHighlightItem();
	void HighlightItem();
	float HighlightIntensity { get; }
}
