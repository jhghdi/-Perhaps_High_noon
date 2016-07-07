using UnityEngine;
using System.Collections;

public class csFadeInOut : MonoBehaviour
{
	public float fadeSpeed = 1.5f;          // Speed that the screen fades to and from black.
	GUITexture guiT;

	private bool phaseStarting = true;      // Whether or not the scene is still fading in.
	private bool phaseEnding = false;      // Whether or not the scene is still fading out.

	void Awake ()
	{
		guiT = gameObject.GetComponent<GUITexture> ();
		// Set the texture so that it is the the size of the screen and covers it.
		guiT.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
	}
		
	void Update ()
	{
		// If the scene is starting...
		if (phaseStarting) {
            // ... call the StartScene function.
            FadeIn();
			return;
		}
		if (phaseEnding) {
			FadeOut ();
			return;
		}
	}

	void FadeIn()
	{
		// Fade the texture to clear.
		// Lerp the colour of the texture between itself and transparent.
		guiT.color = Color.Lerp(guiT.color, Color.clear, fadeSpeed * Time.deltaTime);

		// If the texture is almost clear...
		if(guiT.color.a <= 0.05f)
		{
			// ... set the colour to clear and disable the GUITexture.
			guiT.color = Color.clear;
			guiT.enabled = false;

            // The scene is no longer starting.
            phaseStarting = false;
		}
	}

	void FadeOut()
	{
		// Start fading towards black.
		// Lerp the colour of the texture between itself and black.
		guiT.color = Color.Lerp(guiT.color, Color.black, fadeSpeed * Time.deltaTime);

        // If the screen is almost black...
        if (guiT.color.a >= 0.95f)
        {
            // ... reload the level.
            GameObject.Find("StageManager").SendMessage("OnFadeEnded");
            phaseEnding = false;
        }
	}

	void EndPhase(){
		// Make sure the texture is enabled.
		guiT.enabled = true;

        phaseEnding = true; 
	}

    void StartPhase()
    {
        // Make sure the texture is enabled.
        guiT.enabled = true;

        phaseStarting = true;
    }
}