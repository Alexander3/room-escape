using UnityEngine;
using UnityEngine.UI;


public class TextScreen : Useable
{

    public Text text;
    public string code;


    public delegate void TvScreenUse();
    public static event TvScreenUse TvScreenUsed;

    override protected void doUse()
    {
        text = GameObject.Find("Text").GetComponent<Text>();

        text.text = code;
        if (TvScreenUsed != null)
            TvScreenUsed();

    }
}
