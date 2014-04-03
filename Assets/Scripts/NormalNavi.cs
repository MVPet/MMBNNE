using UnityEngine;
using System.Collections;

public class NormalNavi : MonoBehaviour {

    CharBasic myStatus;
    CharBasic enemyStatus;

    public Transform LSword;
    Animator anim;
    Animator chargeAnim;

    int charge = 0;
    float chargeDelay = 0;
    int damageAdd = 0;

    public Texture q;
    public Texture qdisable;
    float qCooldown = 7f;
    float curQCooldown = 0f;

    public Texture w;
    public Texture wdisable;
    float wCooldown = 7f;
    float curWCooldown = 0f;

    public Texture e;
    public Texture edisable;
    float eCooldown = 10f;
    float curECooldown = 0f;

    public Texture r;
    public Texture rdisable;
    float rCooldown = 15f;
    float curRCooldown = 0f;

    public Texture healthBox;
    public GUISkin BNSkin;
    public GUISkin smallBNSkin;

    public Transform enemy;


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

    void Player1GUI()
    {
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

    void Player2GUI()
    {
        GUI.DrawTexture(new Rect((Screen.width * .99f) - healthBox.width, Screen.height * .02f, healthBox.width, healthBox.height * 1.5f), healthBox);
        GUI.Label(new Rect((Screen.width * .98f) - (healthBox.width * .70f), Screen.height * .023f, healthBox.width, healthBox.height * 1.5f), myStatus.health.ToString());

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

    public void SetUpNNaviValues(int playerNum, Transform en)
    {
        enemy = en;
        myStatus = GetComponent<CharBasic>();
        myStatus.SetUpBasicValues(playerNum, GetComponentsInChildren<Animator>()[0], 100);
        anim = GetComponent<Animator>();
        chargeAnim = GetComponentsInChildren<Animator>()[1];
    }

    void Update()
    {
        if(enemyStatus == null)
            enemyStatus = enemy.GetComponentInChildren<CharBasic>();

        myStatus.BasicUpdate();

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

        UpdateCooldowns();
    }

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

    void Charge()
    {
        chargeDelay += Time.deltaTime;

        if (charge < 100 && chargeDelay > .1f)
        {
            charge += 1;
            chargeAnim.SetInteger("ChargeLevel", charge);
        }
    }

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

    IEnumerator Heal10()
    {
        curECooldown = eCooldown;
        charge = 0;
        chargeDelay = 0;
        anim.SetBool("Heal", true);
        yield return new WaitForSeconds(.1f);
        float animLength = anim.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animLength - .1f);
        anim.SetBool("Heal", false);
        if (myStatus.health < myStatus.maxHealth)
        {
            myStatus.health += 10;
            if(myStatus.health > myStatus.maxHealth)
                myStatus.health = myStatus.maxHealth;
        }
    }

    IEnumerator Att30()
    {
        curRCooldown = rCooldown;
        charge = 0;
        chargeDelay = 0;
        yield return new WaitForSeconds(0f);
        damageAdd = 30;
        GetComponent<SpriteRenderer>().color = new Color(0, 1, 0);
    }

    void CheckCannonHit()
    {
        if (enemyStatus.row == myStatus.row)
        {
            enemyStatus.TakeDamage(40 + damageAdd, true);
        }
    }

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
