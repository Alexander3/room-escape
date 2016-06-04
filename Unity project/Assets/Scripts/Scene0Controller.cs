using UnityEngine;
using UnityEngine.UI;
using System.Linq;


class Scene0Controller : BaseController
{
	public GameObject SwitchBoxDoor = null;
	public GameObject Tvset4 = null;
	public GameObject SwitchBoxSlider2 = null;
	public GameObject pokretlo = null;
	public GameObject safeDoor = null;

    private Color[] _screensColorsOrder	= { Color.green, Color.red, Color.blue };
	private bool[] _screensColorsState = new bool[3];
	private bool[] _switchesState = new bool[4];

    public void Start()
    {
		ColorScreen.ScreenUsed += (short id, Color c) => {
			if (c == _screensColorsOrder[id])
				_screensColorsState[id] = true;
			else
				_screensColorsState[id] = false;

			if (_screensColorsState.All(x => x)){
				unlock(SwitchBoxDoor);
			}
			else
				lockAgain(SwitchBoxDoor);
		};

		MoveSwitch.SwitchMoved += (short id, bool state) => {
			_switchesState [id] = state;
            if (_switchesState[0] == true)
            {
				unlock(Tvset4);
            }
            else
				lockAgain(Tvset4);
			
			if (_switchesState[1] == true)
				unlock(pokretlo);
			else
				lockAgain(pokretlo);
        };

		TurnLock.LockTurned += (short rot) => {
			if (rot % 360 == (int)(360 * 0.35f))
				unlock(safeDoor);
			else
				lockAgain(safeDoor);
		};

        TextScreen.TvScreenUsed += () =>
        {
			unlock(SwitchBoxSlider2);
        };
    }
}
