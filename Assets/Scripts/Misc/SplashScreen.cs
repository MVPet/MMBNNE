using UnityEngine;
using System.Collections;

public class SplashScreen : MonoBehaviour {

    public float duration = 3;
    public float currentDuration = 0;
    public bool fadeOut;
    public bool fadeIn = true;
    public SpriteRenderer splashScreen;

    void Update()
    {
        if (fadeIn)
            splashScreen.color = Color.Lerp(splashScreen.color, new Color(1, 1, 1, 1), .1f);
        else
            currentDuration += Time.deltaTime;

        if (fadeOut)
            splashScreen.color = Color.Lerp(splashScreen.color, new Color(1, 1, 1, 0), .1f);

        if (splashScreen.color.a <= .01f)
            Application.LoadLevel(1);
        else if (splashScreen.color.a >= .95f)
            fadeIn = false;

        if (currentDuration >= duration)
            fadeOut = true;

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Escape))
            Application.LoadLevel(1);
    }
}
