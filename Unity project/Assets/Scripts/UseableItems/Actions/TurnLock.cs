using UnityEngine;
using System.Collections;

public class TurnLock : RotateOnUse
{
	public delegate void OnTurnLock(short rot);
	public static event OnTurnLock LockTurned;
	private short _degrees = 0;
	override protected void doUse()
	{
		base.doUse ();
		_degrees += (short)Rotation.y;
		LockTurned(_degrees);
	}
}
