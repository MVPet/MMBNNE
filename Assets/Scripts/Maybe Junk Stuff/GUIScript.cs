using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour {

    public Transform player;
    PlayerHealth pHealth;
    PlayerController pCont;
    public Transform enemy;
    PlayerHealth p2Health;
    Player2Controller p2Cont;
    public GUISkin BNSkin;
    public GUISkin SmallBNSkin;

    public Texture healthBox;

    void Start()
    {
        pHealth = player.GetComponentInChildren<PlayerHealth>();
        p2Health = enemy.GetComponentInChildren<PlayerHealth>();
        pCont = player.GetComponentInChildren<PlayerController>();
        p2Cont = enemy.GetComponentInChildren<Player2Controller>();
    }

    void OnGUI()
    {
        GUI.skin = BNSkin;

        //Draws the Health and Health boxes
        GUI.DrawTexture(new Rect(Screen.width * .01f, Screen.height * .02f, healthBox.width, healthBox.height * 1.5f), healthBox);
        GUI.DrawTexture(new Rect((Screen.width * .99f) - healthBox.width, Screen.height * .02f, healthBox.width, healthBox.height * 1.5f), healthBox);
        GUI.Label(new Rect((Screen.width * .01f) + (healthBox.width * .15f), Screen.height * .023f, healthBox.width, healthBox.height * 1.5f), pHealth.health.ToString());
        GUI.Label(new Rect((Screen.width * .98f) - (healthBox.width * .75f), Screen.height * .023f, healthBox.width, healthBox.height * 1.5f), p2Health.health.ToString());

        //Draws the Ability icons
        // NEED TO INCLUDE RESOLUTION SCALING
        GUI.DrawTexture(new Rect(Screen.width * .1f, Screen.height * .02f, pHealth.q.width * .5f, pHealth.q.height * .5f), pHealth.q);
        GUI.DrawTexture(new Rect(Screen.width * .16f, Screen.height * .02f, pHealth.w.width * .5f, pHealth.w.height * .5f), pHealth.w);
        GUI.DrawTexture(new Rect(Screen.width * .22f, Screen.height * .02f, pHealth.e.width * .5f, pHealth.e.height * .5f), pHealth.e);
        GUI.DrawTexture(new Rect(Screen.width * .28f, Screen.height * .02f, pHealth.r.width * .5f, pHealth.r.height * .5f), pHealth.r);

        GUI.DrawTexture(new Rect(Screen.width * .845f, Screen.height * .02f, p2Health.r.width * .5f, p2Health.r.height * .5f), p2Health.r);
        GUI.DrawTexture(new Rect(Screen.width * .785f, Screen.height * .02f, p2Health.e.width * .5f, p2Health.e.height * .5f), p2Health.e);
        GUI.DrawTexture(new Rect(Screen.width * .725f, Screen.height * .02f, p2Health.w.width * .5f, p2Health.w.height * .5f), p2Health.w);
        GUI.DrawTexture(new Rect(Screen.width * .665f, Screen.height * .02f, p2Health.q.width * .5f, p2Health.q.height * .5f), p2Health.q);

        CooldownGUI();
    }

    //The Text under the image
    //Says Button when not in cooldown
    void CooldownGUI()
    {
        GUI.skin = SmallBNSkin;
        if(pCont.curQCooldown > 0)
            GUI.Label(new Rect((Screen.width * .1f), (Screen.height * .03f) + (pHealth.q.height * .5f), pHealth.q.width * .5f, pHealth.q.height * .5f), Mathf.Ceil(pCont.curQCooldown).ToString());
        else
            GUI.Label(new Rect((Screen.width * .1f), (Screen.height * .03f) + (pHealth.q.height * .5f), pHealth.q.width * .5f, pHealth.q.height * .5f), "Q");

        if (pCont.curWCooldown > 0)
            GUI.Label(new Rect((Screen.width * .16f), (Screen.height * .03f) + (pHealth.w.height * .5f), pHealth.w.width * .5f, pHealth.w.height * .5f), Mathf.Ceil(pCont.curWCooldown).ToString());
        else
            GUI.Label(new Rect((Screen.width * .16f), (Screen.height * .03f) + (pHealth.w.height * .5f), pHealth.w.width * .5f, pHealth.w.height * .5f), "W");

        if (pCont.curECooldown > 0)
            GUI.Label(new Rect((Screen.width * .22f), (Screen.height * .03f) + (pHealth.e.height * .5f), pHealth.e.width * .5f, pHealth.e.height * .5f), Mathf.Ceil(pCont.curECooldown).ToString());
        else
            GUI.Label(new Rect((Screen.width * .22f), (Screen.height * .03f) + (pHealth.e.height * .5f), pHealth.e.width * .5f, pHealth.e.height * .5f), "E");

        if (pCont.curRCooldown > 0)
            GUI.Label(new Rect((Screen.width * .28f), (Screen.height * .03f) + (pHealth.r.height * .5f), pHealth.r.width * .5f, pHealth.r.height * .5f), Mathf.Ceil(pCont.curRCooldown).ToString());
        else
            GUI.Label(new Rect((Screen.width * .28f), (Screen.height * .03f) + (pHealth.r.height * .5f), pHealth.r.width * .5f, pHealth.r.height * .5f), "R");


        if (p2Cont.curRCooldown > 0)
            GUI.Label(new Rect((Screen.width * .845f), (Screen.height * .03f) + (p2Health.r.height * .5f), p2Health.r.width * .5f, p2Health.r.height * .5f), Mathf.Ceil(p2Cont.curRCooldown).ToString());
        else
            GUI.Label(new Rect((Screen.width * .845f), (Screen.height * .03f) + (p2Health.r.height * .5f), p2Health.r.width * .5f, p2Health.r.height * .5f), "P");

        if (p2Cont.curECooldown > 0)
            GUI.Label(new Rect((Screen.width * .785f), (Screen.height * .03f) + (p2Health.e.height * .5f), p2Health.e.width * .5f, p2Health.e.height * .5f), Mathf.Ceil(p2Cont.curECooldown).ToString());
        else
            GUI.Label(new Rect((Screen.width * .785f), (Screen.height * .03f) + (p2Health.e.height * .5f), p2Health.e.width * .5f, p2Health.e.height * .5f), "O");

        if (p2Cont.curWCooldown > 0)
            GUI.Label(new Rect((Screen.width * .725f), (Screen.height * .03f) + (p2Health.w.height * .5f), p2Health.w.width * .5f, p2Health.w.height * .5f), Mathf.Ceil(p2Cont.curWCooldown).ToString());
        else
            GUI.Label(new Rect((Screen.width * .725f), (Screen.height * .03f) + (p2Health.w.height * .5f), p2Health.w.width * .5f, p2Health.w.height * .5f), "U");

        if (p2Cont.curQCooldown > 0)
            GUI.Label(new Rect((Screen.width * .665f), (Screen.height * .03f) + (p2Health.q.height * .5f), p2Health.q.width * .5f, p2Health.q.height * .5f), Mathf.Ceil(p2Cont.curQCooldown).ToString());
        else
            GUI.Label(new Rect((Screen.width * .665f), (Screen.height * .03f) + (p2Health.q.height * .5f), p2Health.q.width * .5f, p2Health.q.height * .5f), "Y");
    }
}
