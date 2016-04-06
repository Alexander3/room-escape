using UnityEngine;
using System.Linq;


class GameController : MonoBehaviour
{
	public delegate void SwitchLock(string objectName);
	public static event SwitchLock Unlock;
	public static event SwitchLock Lock;

	private Color[] _screensColorsOrder	= { Color.green, Color.red, Color.blue };
	private bool[] _screensColorsState = new bool[3];
    public void Start()
    {
		ColorScreen.ScreenUsed += (short id, Color c) => {
			if (c == _screensColorsOrder[id])
				_screensColorsState[id] = true;
			else
				_screensColorsState[id] = false;
			_screensColorsState.All(a => a);
			if (_screensColorsState.All (x => x)){
				Unlock("SwitchBoxDoor");
				Debug.Log("unlocked");
			}
			else if(Lock != null)
				Lock("SwitchBoxDoor");

		};

    }
}
