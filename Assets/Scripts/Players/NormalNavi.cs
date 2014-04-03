using UnityEngine;
using System.Collections;

public class NormalNavi : MonoBehaviour {

    //player statuyses
    CharBasic myStatus;
    CharBasic enemyStatus;

    // game objects we might need
    public Transform LSword;
    Animator anim;
    Animator chargeAnim;
    public Transform enemy;

    // modifications to character
    int charge = 0;
    float chargeDelay = 0;
    int damageAdd = 0;

    //Ability 1
    public Texture q;
    public Texture qdisable;
    float qCooldown = 7f;
    float curQCooldown = 0f;

    // Ability 2
    public Texture w;
    public Texture wdisable;
    float wCooldown = 7f;
    float curWCooldown = 0f;

    // Ability 3
    public Texture e;
    public Texture edisable;
    float eCooldown = 10f;
    float curECooldown = 0f;

    // Ability 4
    public Texture r;
    public Texture rdisable;
    float rCooldown = 15f;
    float curRCooldown = 0f;

    // HUD Stuff
    public Texture healthBox;
    public GUISkin BNSkin;
    public GUISkin smallBNSkin;

    // Stuff for GUI
    void OnGUI()
    {
        GUI.skin = BNSkin;

        if (myStatus != null)
        {
            switch (myStatus.playerNo)
            {
                case 1:
                    Player1GUI();
                    break;
                case 2:
                    Player2GUI();
                    break;
            }
        }
    }

    // WHat should be drawn for Player 1's GUI
    void Player1GUI()
    {
        // draw the ability icons
        GUI.DrawTexture(new Rect(Screen.width * .01f, Screen.height * .02f, healthBox.width, healthBox.height * 1.5f), healthBox);
        GUI.Label(new Rect((Screen.width * .01f) + (healthBox.width * .15f), Screen.height * .023f, healthBox.width, healthBox.height * 1.5f), myStatus.health.ToString());

        if(curQCooldown == 0)
            GUI.DrawTexture(new Rect(Screen.width * .1f, Screen.height * .02f, q.width * .5f, q.height * .5f), q);
        else
            GUI.DrawTexture(new Rect(Screen.width * .1f, Screen.height * .02f, q.width * .5f, q.height * .5f), qdisable);

        if (curWCooldown == 0)
            GUI.DrawTexture(new Rect(Screen.width * .16f, Screen.height * .02f, w.width * .5f, w.height * .5f), w);
        else
            GUI.DrawTexture(new Rect(Screen.width * .16f, Screen.height * .02f, w.width * .5f, w.height * .5f), wdisable);

        if (curECooldown == 0)
            GUI.DrawTexture(new Rect(Screen.width * .22f, Screen.height * .02f, e.width * .5f, e.height * .5f), e);
        else
            GUI.DrawTexture(new Rect(Screen.width * .22f, Screen.height * .02f, e.width * .5f, e.height * .5f), edisable);

        if (curRCooldown == 0)
            GUI.DrawTexture(new Rect(Screen.width * .28f, Screen.height * .02f, r.width * .5f, r.height * .5f), r);
        else
            GUI.DrawTexture(new Rect(Screen.width * .28f, Screen.height * .02f, r.width * .5f, r.height * .5f), rdisable);

        // draw the keycode/cooldown for each ability
        GUI.skin = smallBNSkin;
        if (curQCooldown > 0)
            GUI.Label(new Rect((Screen.width * .1f), (Screen.height * .03f) + (q.height * .5f), q.width * .5f, q.height * .5f), Mathf.Ceil(curQCooldown).ToString());
        else
            GUI.Label(new Rect((Screen.width * .1f), (Screen.height * .03f) + (q.height * .5f), q.width * .5f, q.height * .5f), "Q");

        if (curWCooldown > 0)
            GUI.Label(new Rect((Screen.width * .16f), (Screen.height * .03f) + (w.height * .5f), w.width * .5f, w.height * .5f), Mathf.Ceil(curWCooldown).ToString());
        else
            GUI.Label(new Rect((Screen.width * .16f), (Screen.height * .03f) + (w.height * .5f), w.width * .5f, w.height * .5f), "W");

        if (curECooldown > 0)
            GUI.Label(new Rect((Screen.width * .22f), (Screen.height * .03f) + (e.height * .5f), e.width * .5f, e.height * .5f), Mathf.Ceil(curECooldown).ToString());
        else
            GUI.Label(new Rect((Screen.width * .22f), (Screen.height * .03f) + (e.height * .5f), e.width * .5f, e.height * .5f), "R");

        if (curRCooldown > 0)
            GUI.Label(new Rect((Screen.width * .28f), (Screen.height * .03f) + (r.height * .5f), r.width * .5f, r.height * .5f), Mathf.Ceil(curRCooldown).ToString());
        else
            GUI.Label(new Rect((Screen.width * .28f), (Screen.height * .03f) + (r.height * .5f), r.width * .5f, r.height * .5f), "T");
    }

    // WHat should be drawn for Player 2's GUI
    void Player2GUI()
    {
        // draw the health
        GUI.DrawTexture(new Rect((Screen.width * .99f) - healthBox.width, Screen.height * .02f, healthBox.width, healthBox.height * 1.5f), healthBox);
        GUI.Label(new Rect((Screen.width * .98f) - (healthBox.width * .70f), Screen.height * .023f, healthBox.width, healthBox.height * 1.5f), myStatus.health.ToString());

        // draw the ability icons
        if (curRCooldown == 0)
            GUI.DrawTexture(new Rect(Screen.width * .87f, Screen.height * .02f, r.width * .5f, r.height * .5f), r);
        else
            GUI.DrawTexture(new Rect(Screen.width * .87f, Screen.height * .02f, r.width * .5f, r.height * .5f), rdisable);

        if (curECooldown == 0)
            GUI.DrawTexture(new Rect(Screen.width * .81f, Screen.height * .02f, e.width * .5f, e.height * .5f), e);
        else
            GUI.DrawTexture(new Rect(Screen.width * .81f, Screen.height * .02f, e.width * .5f, e.height * .5f), edisable);

        if (curWCooldown == 0)
            GUI.DrawTexture(new Rect(Screen.width * .75f, Screen.height * .02f, w.width * .5f, w.height * .5f), w);
        else
            GUI.DrawTexture(new Rect(Screen.width * .75f, Screen.height * .02f, w.width * .5f, w.height * .5f), wdisable);

        if (curQCooldown == 0)
            GUI.DrawTexture(new Rect(Screen.width * .69f, Screen.height * .02f, q.width * .5f, q.height * .5f), q);
        else
            GUI.DrawTexture(new Rect(Screen.width * .69f, Screen.height * .02f, q.width * .5f, q.height * .5f), qdisable);

        // draw the keycode/cooldown for each ability
        GUI.skin = smallBNSkin;
        if (curRCooldown > 0)
            GUI.Label(new Rect((Screen.width * .87f), (Screen.height * .03f) + (r.height * .5f), r.width * .5f, r.height * .5f), Mathf.Ceil(curRCooldown).ToString());
        else
            GUI.Label(new Rect((Screen.width * .87f), (Screen.height * .03f) + (r.height * .5f), r.width * .5f, r.height * .5f), "P");

        if (curECooldown > 0)
            GUI.Label(new Rect((Screen.width * .81f), (Screen.height * .03f) + (e.height * .5f), e.width * .5f, e.height * .5f), Mathf.Ceil(curECooldown).ToString());
        else
            GUI.Label(new Rect((Screen.width * .81f), (Screen.height * .03f) + (e.height * .5f), e.width * .5f, e.height * .5f), "O");

        if (curWCooldown > 0)
            GUI.Label(new Rect((Screen.width * .75f), (Screen.height * .03f) + (w.height * .5f), w.width * .5f, w.height * .5f), Mathf.Ceil(curWCooldown).ToString());
        else
            GUI.Label(new Rect((Screen.width * .75f), (Screen.height * .03f) + (w.height * .5f), w.width * .5f, w.height * .5f), "U");

        if (curQCooldown > 0)
            GUI.Label(new Rect((Screen.width * .69f), (Screen.height * .03f) + (q.height * .5f), q.width * .5f, q.height * .5f), Mathf.Ceil(curQCooldown).ToString());
        else
            GUI.Label(new Rect((Screen.width * .69f), (Screen.height * .03f) + (q.height * .5f), q.width * .5f, q.height * .5f), "Y");
    }

    // set up the default values for the character
    public void SetUpNNaviValues(int playerNum, Transform en)
    {
        enemy = en;
        myStatus = GetComponent<CharBasic>();
        myStatus.SetUpBasicValues(playerNum, GetComponentsInChildren<Animator>()[0], 250);
        anim = GetComponent<Animator>();
        chargeAnim = GetComponentsInChildren<Animator>()[1];
    }

    // update once per frame
    void Update()
    {
        // get the enemy status
        if(enemyStatus == null)
            enemyStatus = enemy.GetComponentInChildren<CharBasic>();

        //update our status
        myStatus.BasicUpdate();

        // are we hit?
        if (!myStatus.disableInput && !anim.GetBool("isHit"))
        {
            switch (myStatus.playerNo)
            {
                case 1:
                    P1Update();
                    break;
                case 2:
                    P2Update();
                    break;
            }
        }

        // update those cooldowns
        UpdateCooldowns();
    }

    // Update Player 1
    void P1Update()
    {
        if (Input.GetKey(KeyCode.A))
            Charge();

        if (Input.GetKeyUp(KeyCode.A) || (charge == 100 && !Input.GetKey(KeyCode.A)))
            StartCoroutine("Buster");

        if (Input.GetKeyDown(KeyCode.Q) && curQCooldown == 0)
            StartCoroutine("Cannon");

        if (Input.GetKeyDown(KeyCode.W) && curWCooldown == 0)
            StartCoroutine("Sword");

        if (Input.GetKeyDown(KeyCode.R) && curECooldown == 0)
            StartCoroutine("Heal10");

        if (Input.GetKeyDown(KeyCode.T) && curRCooldown == 0)
            StartCoroutine("Att30");
    }

    // Update Player 2
    void P2Update()
    {
        if (Input.GetKey(KeyCode.Space))
            Charge();

        if (Input.GetKeyUp(KeyCode.Space) || (charge == 100 && !Input.GetKeyUp(KeyCode.Space)))
            StartCoroutine("Buster");

        if (Input.GetKeyDown(KeyCode.Y) && curQCooldown == 0)
            StartCoroutine("Cannon");

        if (Input.GetKeyDown(KeyCode.U) && curWCooldown == 0)
            StartCoroutine("Sword");

        if (Input.GetKeyDown(KeyCode.O) && curECooldown == 0)
            StartCoroutine("Heal10");

        if (Input.GetKeyDown(KeyCode.P) && curRCooldown == 0)
            StartCoroutine("Att30");
    }

    // Update the cooldowns on the abilities
    void UpdateCooldowns()
    {
        if (curQCooldown != 0)
        {
            curQCooldown -= Time.deltaTime;
            if (curQCooldown < 0)
                curQCooldown = 0;
        }

        if (curWCooldown != 0)
        {
            curWCooldown -= Time.deltaTime;
            if (curWCooldown < 0)
                curWCooldown = 0;
        }

        if (curECooldown != 0)
        {
            curECooldown -= Time.deltaTime;
            if (curECooldown < 0)
                curECooldown = 0;
        }

        if (curRCooldown != 0)
        {
            curRCooldown -= Time.deltaTime;
            if (curRCooldown < 0)
                curRCooldown = 0;
        }
    }

    // Charge the megabuster
    void Charge()
    {
        chargeDelay += Time.deltaTime;

        if (charge < 100 && chargeDelay > .1f)
        {
            charge += 1;
            chargeAnim.SetInteger("ChargeLevel", charge);
        }
    }

    // Perform the MegaBuster Attack
    // set the booleans for animations
    // reset values
    // play animation
    // animation done, reset values
    IEnumerator Buster()
    {
        chargeDelay = 0;
        chargeAnim.SetInteger("ChargeLevel", 0);
        anim.SetBool("Buster", true);
        anim.SetBool("Heal", false);
        myStatus.disableInput = true;
        yield return new WaitForSeconds(.1f);
        float animLength = anim.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animLength - .1f);
        CheckBusterHit();

        anim.SetBool("Buster", false);
        charge = 0;
        damageAdd = 0;
        myStatus.disableInput = false;
    }

    // Perform the Cannon Ability
    // set the booleans for animations
    // reset values
    // play animation
    // animation done, reset values
    IEnumerator Cannon()
    {
        anim.SetBool("QAbility", true);
        anim.SetBool("Heal", false);
        myStatus.disableInput = true;
        curQCooldown = qCooldown;
        charge = 0;
        chargeDelay = 0;
        chargeAnim.SetInteger("ChargeLevel", charge);
        yield return new WaitForSeconds(.1f);
        float animLength = anim.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animLength - .1f);
        CheckCannonHit();

        anim.SetBool("QAbility", false);
        damageAdd = 0;
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
        myStatus.disableInput = false;
    }

    // Perform the Sword Ability
    // set the booleans for animations
    // reset values
    // play animation (create sword attack)
    // animation done, reset values
    IEnumerator Sword()
    {
        anim.SetBool("WAbility", true);
        anim.SetBool("Heal", false);
        myStatus.disableInput = true;
        curWCooldown = wCooldown;
        charge = 0;
        chargeDelay = 0;
        chargeAnim.SetInteger("ChargeLevel", charge);
        yield return new WaitForSeconds(.1f);
        float animLength = anim.GetCurrentAnimatorStateInfo(0).length;
        Transform sword = Instantiate(LSword) as Transform;
        if(myStatus.playerNo == 1)
            sword.GetComponent<LSword>().SetUpValues(myStatus.column + 1, myStatus.row, enemyStatus, damageAdd, myStatus.playerNo, new Vector2(transform.position.x + 1.5f, transform.position.y - 3f));
        else
            sword.GetComponent<LSword>().SetUpValues(myStatus.column - 1, myStatus.row, enemyStatus, damageAdd, myStatus.playerNo, new Vector2(transform.position.x - 1.5f, transform.position.y - 3f));
        yield return new WaitForSeconds(animLength - .1f);

        anim.SetBool("WAbility", false);
        damageAdd = 0;
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
        myStatus.disableInput = false;
    }

    // Perform the Heal Ability
    // set the booleans for animations
    // reset values
    // play animation
    // animation done, reset values
    IEnumerator Heal10()
    {
        if (myStatus.health < 250)
        {
            myStatus.health += 10;
            if (myStatus.health > 250)
                myStatus.health = 250;
        }
        curECooldown = eCooldown;
        charge = 0;
        chargeDelay = 0;
        anim.SetBool("Heal", true);
        yield return new WaitForSeconds(.1f);
        float animLength = anim.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animLength - .1f);
        anim.SetBool("Heal", false);
        
    }

    // Perfomr the Attack + 30 Ability
    // set the booleans for animations
    // reset values
    // play animation
    // animation done, reset values
    IEnumerator Att30()
    {
        curRCooldown = rCooldown;
        charge = 0;
        chargeDelay = 0;
        yield return new WaitForSeconds(0f);
        damageAdd = 30;
        GetComponent<SpriteRenderer>().color = new Color(0, 1, 0);
    }

    // Check if the Cannon ability hit
    void CheckCannonHit()
    {
        if (enemyStatus.row == myStatus.row)
        {
            enemyStatus.TakeDamage(40 + damageAdd, true);
        }
    }

    // Check if the buster hit?
    void CheckBusterHit()
    {
        if (enemyStatus.row == myStatus.row)
        {
            if (charge == 100)
                enemyStatus.TakeDamage(10, false);
            else
                enemyStatus.TakeDamage(1, false);
        }
    }
}
