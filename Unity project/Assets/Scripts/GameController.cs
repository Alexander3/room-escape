using UnityEngine;
using UnityEngine.UI;
using System.Linq;


class GameController : MonoBehaviour
{
	public GameObject Door;
	public GameObject SwitchBoxDoor;
    public GameObject Tvset4;
    public GameObject SwitchBoxSlider2;
    public GameObject pokretlo;

	private Color[] _screensColorsOrder	= { Color.green, Color.red, Color.blue };
	private bool[] _screensColorsState = new bool[3];

	private bool[] _switchesState = new bool[4];
	private bool[] _requiredSwitchesState = {true,false,false,false};




    public void Start()
    {
		ColorScreen.ScreenUsed += (short id, Color c) => {
			if (c == _screensColorsOrder[id])
				_screensColorsState[id] = true;
			else
				_screensColorsState[id] = false;

			if (_screensColorsState.All(x => x)){
				SwitchBoxDoor.GetComponent<Useable>().CanBeUsed = true;
				Debug.Log("unlocked");
			}
			else
				SwitchBoxDoor.GetComponent<Useable>().CanBeUsed = false;
		};

		MoveSwitch.SwitchMoved += (short id, bool state) => {
			_switchesState [id] = state;
            if (_switchesState[0] == true)
            {
                //Door.GetComponent<Door>().enabled = true;
                Tvset4.GetComponent<Useable>().CanBeUsed = true;
                Debug.Log("unlocked");
                 
                   if (_switchesState[1] == true)
                
                      pokretlo.GetComponent<Useable>().CanBeUsed = true;
                
                   else
                      pokretlo.GetComponent<Useable>().CanBeUsed = false;
            }
            else
                //Door.GetComponent<Door>().enabled = false;
                Tvset4.GetComponent<Useable>().CanBeUsed = false; ;
        };

        TextScreen.TvScreenUsed += () =>
        {
            SwitchBoxSlider2.GetComponent<Useable>().CanBeUsed = true;


        };

    }
}
