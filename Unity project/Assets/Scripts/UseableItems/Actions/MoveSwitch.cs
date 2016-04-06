using UnityEngine;
using System.Collections;

public class MoveSwitch : MoveOnUse {
	public delegate void OnSwitchMove(short id, bool state);
	public static event OnSwitchMove SwitchMoved;
	private bool _state = false;
	public short id;


	override protected void doUse()
	{     
		base.doUse ();
		_state = !_state;
		SwitchMoved (id, _state);
	}     
}

