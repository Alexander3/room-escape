using UnityEngine;
using System.Linq;


class GameController : MonoBehaviour
{
	public delegate void Unlocker(string objectName);
	public static event Unlocker Unlock;
	public delegate void Locker(string objectName);
	public static event Locker Lock;

	private Color[] _screensColorsOrder	= { Color.green, Color.red, Color.blue };
	private bool[] _screensColorsState = new bool[3];
    public void Start()
    {
		ColorScreen.ScreenUsed += (short id, Color c) => {
			if (c == _screensColorsOrder[id])
				_screensColorsState[id] = true;
			_screensColorsState.All(a => a);
			if (_screensColorsState.All (x => x))
				Unlock("SwitchBoxDoor");
			else if(Lock != null)
				Lock("SwitchBoxDoor");

		};

    }
}
