using UnityEngine;
using System.Collections;

public class TurnLock : RotateOnUse
{
	public delegate void OnTurnLock(float rot);
	public static event OnTurnLock LockTurned;
	private float _degrees = 0;
	override protected void doUse()
	{
		base.doUse ();
		_degrees += Rotation.y;
		LockTurned(_degrees);
	}
}
