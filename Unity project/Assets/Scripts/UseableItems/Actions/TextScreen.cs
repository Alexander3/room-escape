using UnityEngine;
using UnityEngine.UI;


public class TextScreen : Useable
{
	public delegate void TvScreenUse();
	public static event TvScreenUse TvScreenUsed;

    public string code;
	public GameObject Display;

	private Text text;

    override protected void doUse()
    {
		text = Display.GetComponent<Text>();

        text.text = code;
        if (TvScreenUsed != null)
            TvScreenUsed();
    }
}
