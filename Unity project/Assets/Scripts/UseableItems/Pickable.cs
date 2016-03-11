using UnityEngine;
using System.Collections;

public interface Pickable : Useable
{
	Vector3 HandShift { get; }
	Quaternion RotationShift { get; }
}
