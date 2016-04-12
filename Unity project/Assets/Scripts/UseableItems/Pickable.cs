using UnityEngine;
using System.Collections;

public abstract class Pickable : Useable
{
	public Vector3 handShift = new Vector3(0.26f, 0, -0.28f);
	public Quaternion rotationShift = new Quaternion(-1.6f, 21.6f, 21.6f, 1);
	public float MoveSpeed = 0.1f;
}
