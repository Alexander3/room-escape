using UnityEngine;

public class Scene1Door : MonoBehaviour {
    private bool won = false;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "KeyCard" && enabled)
        {
            won = true;
            Time.timeScale = 0;
        }
    }

    private const int w = 600, h = 200;
    void OnGUI()
    {
        if (won)
        {
            GUI.color = new Color(0, 0, 0, 0.8f);
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), new Texture2D(10, 10));
            var centeredStyle = GUI.skin.GetStyle("Label");
            centeredStyle.alignment = TextAnchor.UpperCenter;
            centeredStyle.fontSize = 100;
            centeredStyle.fontStyle = FontStyle.Bold;
            GUI.color = Color.red;
            GUI.Label(new Rect(Screen.width / 2 - w /2, Screen.height / 2 - h/2, w, h), "You Win!", centeredStyle);
        }
    }
}
