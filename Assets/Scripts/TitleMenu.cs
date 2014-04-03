using UnityEngine;
using System.Collections;

public class TitleMenu : MonoBehaviour {

    public Animator anim;
    public Font BigBNFont;
    public Font BNFont;
    public AudioClip transition;
    public SpriteRenderer fade;
    bool startPressed = false;

    public Texture arrow;
    float arrowHeight = (Screen.height / 1.5f);
    public int arrowLoc = 1;

    bool fadeIn = true;
    bool buttonInput;
    bool mouseInput;
    int transitionNo = 0;

    void OnGUI()
    {
        GUI.backgroundColor = new Color(0, 0, 0, 0);
        GUI.skin.font = BigBNFont;

        if (!fadeIn && transitionNo == 0)
        {
            if (startPressed)
            {
                if (buttonInput)
                    GUI.Label(new Rect((Screen.width / 2) - 75 - 25, arrowHeight, 50, 30), arrow);

                if (GUI.Button(new Rect((Screen.width / 2) - 75, (Screen.height / 1.5f), 200, 30), "Vs. Player 2"))
                {
                    transitionNo = 1;
                    StartCoroutine("NextLevel");
                }

                if (GUI.Button(new Rect((Screen.width / 2) - 75, (Screen.height / 1.5f) + 50, 200, 30), "Vs. CPU"))
                {
                    transitionNo = 2;
                    StartCoroutine("NextLevel");
                }

                DisplayControls();
            }
            else
                GUI.Label(new Rect((Screen.width / 2) - 75, Screen.height / 1.5f, 300, 30), "Press Enter");

            if (startPressed)
            {
                if (buttonInput)
                    GUI.Label(new Rect((Screen.width / 2) - 75 - 25, arrowHeight, 50, 30), arrow);

                if (GUI.Button(new Rect((Screen.width / 2) - 75, (Screen.height / 1.5f), 200, 30), "Vs. Player 2"))
                {
                    transitionNo = 1;
                    StartCoroutine("NextLevel");
                }

                if (GUI.Button(new Rect((Screen.width / 2) - 75, (Screen.height / 1.5f) + 50, 200, 30), "Vs. CPU"))
                {
                    transitionNo = 2;
                    StartCoroutine("NextLevel");
                }

                DisplayControls();
            }
            else
                GUI.Label(new Rect((Screen.width / 2) - 75, Screen.height / 1.5f, 300, 30), "Press Enter");
        }
    }

    void Update()
    {
        if (!fadeIn)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (buttonInput)
                {
                    switch (arrowLoc)
                    {
                        case 1:
                            transitionNo = 2;
                            StartCoroutine("NextLevel");
                            break;
                        case 2:
                            transitionNo = 3;
                            StartCoroutine("NextLevel");
                            break;
                    }
                }

                startPressed = true;
                buttonInput = true;
            }

            if (Input.GetAxis("Mouse X") != 0)
            {
                mouseInput = true;
                buttonInput = false;
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (buttonInput && arrowLoc == 2)
                {
                    arrowHeight = (Screen.height / 1.5f);
                    arrowLoc = 1;
                }

                if (mouseInput)
                {
                    mouseInput = false;
                    buttonInput = true;
                }
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (buttonInput && arrowLoc == 1)
                {
                    arrowHeight = (Screen.height / 1.5f) + 50;
                    arrowLoc = 2;
                }

                if (mouseInput)
                {
                    mouseInput = false;
                    buttonInput = true;
                }

            }
        }
        else
        {
            fade.color = Color.Lerp(fade.color, new Color(1, 1, 1, 0), .1f);
            Debug.Log(fade.color.a);

            if (fade.color.a <= .01)
                fadeIn = false;
        }

    }

    IEnumerator NextLevel()
    {
        Time.timeScale = 1;
        GetComponent<AudioSource>().clip = transition;
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(.05f);
        anim.SetBool("transition", true);
        anim.transform.localScale = new Vector3(7.7f, 6.7f, transform.localScale.z);
        yield return new WaitForSeconds(.05f);
        float animLength = anim.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animLength - .05f);
        Debug.Log("I got here");

        Application.LoadLevel(transitionNo);
    }

    void DisplayControls()
    {
        GUI.skin.font = BNFont;
        GUI.backgroundColor = new Color(0, 0, 0, .5f);
        GUI.Box(new Rect((Screen.width * .065f), (Screen.height * .53f) - 40, 210, 250), "Player 1   Controls:");
        GUI.Label(new Rect((Screen.width * .065f) + 10, (Screen.height * .53f) - 10, 200, 100), "E\t\t\t\t\t\t-\tMove Up");
        GUI.Label(new Rect((Screen.width * .065f) + 10, (Screen.height * .53f) + 10, 200, 100), "D\t\t\t\t\t\t-\tMove Down");
        GUI.Label(new Rect((Screen.width * .065f) + 10, (Screen.height * .53f) + 30, 200, 100), "S\t\t\t\t\t\t-\tMove Left");
        GUI.Label(new Rect((Screen.width * .065f) + 10, (Screen.height * .53f) + 50, 200, 100), "F\t\t\t\t\t\t-\tMove Right");
        GUI.Label(new Rect((Screen.width * .065f) + 10, (Screen.height * .53f) + 70, 200, 100), "A\t\t\t\t\t\t-\tBuster");
        GUI.Label(new Rect((Screen.width * .065f) + 10, (Screen.height * .53f) + 90, 200, 100), "Q\t\t\t\t\t\t-\tAbility 1");
        GUI.Label(new Rect((Screen.width * .065f) + 10, (Screen.height * .53f) + 110, 200, 100), "W\t\t\t\t\t\t-\tAbility 2");
        GUI.Label(new Rect((Screen.width * .065f) + 10, (Screen.height * .53f) + 130, 200, 100), "E\t\t\t\t\t\t-\tAbility 3");
        GUI.Label(new Rect((Screen.width * .065f) + 10, (Screen.height * .53f) + 150, 200, 100), "R\t\t\t\t\t\t-\tAbility 4");
        GUI.Label(new Rect((Screen.width * .065f) + 10, (Screen.height * .53f) + 170, 200, 100), "Enter\t\t\t\t-\tConfirm");
        GUI.Label(new Rect((Screen.width * .065f) + 10, (Screen.height * .53f) + 190, 200, 100), "Esc/Enter\t-\tPause");

        GUI.Box(new Rect((Screen.width * .70f), (Screen.height * .53f) - 40, 210, 250), "Player 2   Controls:");
        GUI.Label(new Rect((Screen.width * .70f) + 10, (Screen.height * .53f) - 10, 200, 100), "I\t\t\t\t\t\t-\tMove Up");
        GUI.Label(new Rect((Screen.width * .70f) + 10, (Screen.height * .53f) + 10, 200, 100), "K\t\t\t\t\t\t-\tMove Down");
        GUI.Label(new Rect((Screen.width * .70f) + 10, (Screen.height * .53f) + 30, 200, 100), "J\t\t\t\t\t\t-\tMove Left");
        GUI.Label(new Rect((Screen.width * .70f) + 10, (Screen.height * .53f) + 50, 200, 100), "L\t\t\t\t\t\t-\tMove Right");
        GUI.Label(new Rect((Screen.width * .70f) + 10, (Screen.height * .53f) + 70, 200, 100), "Space\t\t\t\t-\tBuster");
        GUI.Label(new Rect((Screen.width * .70f) + 10, (Screen.height * .53f) + 90, 200, 100), "Y\t\t\t\t\t\t-\tAbility 1");
        GUI.Label(new Rect((Screen.width * .70f) + 10, (Screen.height * .53f) + 110, 200, 100), "U\t\t\t\t\t\t-\tAbility 2");
        GUI.Label(new Rect((Screen.width * .70f) + 10, (Screen.height * .53f) + 130, 200, 100), "O\t\t\t\t\t\t-\tAbility 3");
        GUI.Label(new Rect((Screen.width * .70f) + 10, (Screen.height * .53f) + 150, 200, 100), "P\t\t\t\t\t\t-\tAbility 4");
        GUI.Label(new Rect((Screen.width * .70f) + 10, (Screen.height * .53f) + 170, 200, 100), "Enter\t\t\t\t-\tConfirm");
        GUI.Label(new Rect((Screen.width * .70f) + 10, (Screen.height * .53f) + 190, 200, 100), "Esc/Enter\t-\tPause");
        GUI.backgroundColor = new Color(0, 0, 0, 0);
        GUI.skin.font = BigBNFont;
    }
}
