using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public Transform Player1;
    public Transform Player2;
    public Transform BStart;
    public Transform BEnd;
    public AudioSource camera;
    public Transform pauseTintPrefab;
    Transform pauseTint;
    Transform winAnim;
    int winner = 0;
    int whichPause = 0;
    bool isPaused;
    bool hasBattleStarted = false;
    bool maybeQuit = false;
    bool battleDone = false;

    public Texture arrow;
    float arrowHeight = (Screen.height * .25f) + 30;
    int arrowLoc = 1;

    public GUISkin BNSkin;
    public GUISkin SmallBNSkin;

    public AudioClip winnerAudio;
    public AudioClip loserAudio;

    void Start()
    {
        StartCoroutine("StartBattle");
    }

    void OnGUI()
    {
        GUI.skin = BNSkin;
        if (winAnim != null && winAnim.GetComponent<SpriteRenderer>().color.a >= .9)
        {
            battleDone = true;
            GUI.Label(new Rect((Screen.width * .3f), (Screen.height / 2) + 50, 300, 30), "Press ENTER to Play Again");
            GUI.Label(new Rect((Screen.width * .3f), (Screen.height / 2) + 80, 300, 30), "Press ESC to Quit");
        }

        if (isPaused)
        {
            if (maybeQuit)
            {
                GUI.Label(new Rect((Screen.width * .35f), Screen.height * .25f, 450, 30), "Are you sure you want to quit");
                GUI.Label(new Rect((Screen.width * .6f) - 100, arrowHeight, 120, 30), arrow);
                GUI.Label(new Rect((Screen.width * .6f) - 60, (Screen.height * .25f) + 30, 120, 30), "Yes");
                GUI.Label(new Rect((Screen.width * .6f) - 60, (Screen.height * .25f) + 60, 120, 30), "No");
            }
            else
            {
                GUI.Label(new Rect((Screen.width * .5f) - 60, Screen.height * .25f, 120, 30), "PAUSED");

                if (whichPause == 1)
                    GUI.Label(new Rect((Screen.width * .35f), Screen.height * .45f, 300, 30), "Press ENTER to Quit");
                else
                    GUI.Label(new Rect((Screen.width * .35f), Screen.height * .45f, 300, 30), "Press ESC to Quit");
            }

            DisplayControls();
        }
    }

    void DisplayControls()
    {
        GUI.skin = SmallBNSkin;
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
        GUI.skin = BNSkin;
    }

    void Update()
    {
        if ((isPaused && Player1.GetComponentInChildren<CharBasic>().disableInput == false) || (isPaused && Player2.GetComponentInChildren<CharBasic>().disableInput == false))
        {
            Player1.GetComponentInChildren<CharBasic>().disableInput = true;
            Player2.GetComponentInChildren<CharBasic>().disableInput = true;
        }

        if (winAnim != null)
            winAnim.GetComponent<SpriteRenderer>().color = Color.Lerp(winAnim.GetComponent<SpriteRenderer>().color, new Color(1, 1, 1, 1), Time.deltaTime * 1.5f);

        if (Player1.GetComponentInChildren<CharBasic>().health == 0 || Player2.GetComponentInChildren<CharBasic>().health == 0)
        {
            Player1.GetComponentInChildren<CharBasic>().disableInput = true;
            Player2.GetComponentInChildren<CharBasic>().disableInput = true;
            
            if (pauseTint == null)
                pauseTint = Instantiate(pauseTintPrefab) as Transform;

            if(winAnim == null)
                winAnim = Instantiate(BEnd) as Transform;

            if (Player1.GetComponentInChildren<CharBasic>().health == 0)
            {
                Player1.GetComponentInChildren<CharBasic>().isAlive = false;
                winner = 2;
            }
            else
            {
                Player2.GetComponentInChildren<CharBasic>().isAlive = false;
                winner = 1;
            }

            winAnim.GetComponent<Animator>().SetInteger("winner", winner);

            if (Player1.GetComponentInChildren<CharBasic>().health == 0 && Player2.GetComponentInChildren<NNaviAI>() != null)
            {
                camera.clip = loserAudio;
                if(!camera.isPlaying)
                    camera.Play();
            }
            else
            {
                camera.clip = winnerAudio;
                if (!camera.isPlaying)
                    camera.Play();
            }
        }

        if (hasBattleStarted)
        {
            PauseInput();
        }
    }

    void PauseInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!battleDone)
            {
                if (maybeQuit)
                    maybeQuit = false;
                else if (whichPause == 2 && isPaused)
                    maybeQuit = true;
                else if (!isPaused)
                {
                    whichPause = 1;
                    Player1.GetComponentInChildren<CharBasic>().disableInput = true;
                    Player2.GetComponentInChildren<CharBasic>().disableInput = true;
                    isPaused = true;
                    Time.timeScale = 0;
                    pauseTint = Instantiate(pauseTintPrefab) as Transform;
                }
                else
                {
                    Player1.GetComponentInChildren<CharBasic>().disableInput = false;
                    Player2.GetComponentInChildren<CharBasic>().disableInput = false;
                    isPaused = false;
                    Time.timeScale = 1;
                    Destroy(pauseTint.gameObject);
                }
            }
            else
                Application.LoadLevel(0);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!battleDone)
            {
                if (maybeQuit)
                {
                    if (arrowLoc == 1)
                        Application.LoadLevel(0);
                    else
                        maybeQuit = false;
                }
                else if (whichPause == 1 && isPaused)
                    maybeQuit = true;
                else if (!isPaused)
                {
                    whichPause = 2;
                    Player1.GetComponentInChildren<CharBasic>().disableInput = true;
                    Player2.GetComponentInChildren<CharBasic>().disableInput = true;
                    isPaused = true;
                    Time.timeScale = 0;
                    pauseTint = Instantiate(pauseTintPrefab) as Transform;
                }
                else
                {
                    Player1.GetComponentInChildren<CharBasic>().disableInput = false;
                    Player2.GetComponentInChildren<CharBasic>().disableInput = false;
                    isPaused = false;
                    Time.timeScale = 1;
                    Destroy(pauseTint.gameObject);
                }
            }
            else
                Application.LoadLevel(Application.loadedLevel);
        }

        if (maybeQuit)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (arrowLoc == 1)
                {
                    arrowLoc = 2;
                    arrowHeight = (Screen.height * .25f) + 60;
                }
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (arrowLoc == 2)
                {
                    arrowLoc = 1;
                    arrowHeight = (Screen.height * .25f) + 30;
                }
            }
        }
    }

    IEnumerator StartBattle()
    {
        Transform anim = Instantiate(BStart) as Transform;

        yield return new WaitForSeconds(.05f);
        float animLength = anim.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animLength - .05f);

        anim.GetComponent<Animator>().SetBool("weReady", true);

        yield return new WaitForSeconds(.05f);
        animLength = anim.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animLength - .05f);

        Destroy(anim.gameObject);

        yield return new WaitForSeconds(.1f);
        Player1.GetComponentInChildren<CharBasic>().disableInput = false;
        Player2.GetComponentInChildren<CharBasic>().disableInput = false;
        hasBattleStarted = true;
    }
}
