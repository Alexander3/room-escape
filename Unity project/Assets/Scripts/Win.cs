using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour {
    public float fadeSpeed = 0.08f;

    private float alpha = 1.0f, fadeTo = 0.0f;
    private bool fading = false;
    
    private void stopFading()
    {
        fading = false;
    }
    void OnTriggerEnter(Collider other)
	{
        if (other is CharacterController)
        {
            fading = true;
            SceneManager.LoadScene(1);
            fadeTo = 0;
            Invoke("stopFading", 5);
        }
    }
    void OnGUI()
    {
        if (fading)
        {
            alpha = Mathf.Lerp(alpha, fadeTo, Time.deltaTime * fadeSpeed);
            GUI.color = new Color(0, 0, 0, alpha);
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), new Texture2D(10,10));
        }
    }
}
