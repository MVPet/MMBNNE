using UnityEngine;
using System.Collections;

public class NNaviAI : MonoBehaviour
{
    float updateTime = 0;
    float currMoves = 0;
    float numOfMoves = 0;
    bool isHealing = false;
    int numOfBuster = 0;
    bool weChargin = false;

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
            GUI.DrawTexture(new Rect((Screen.width * .99f) - healthBox.width, Screen.height * .02f, healthBox.width, healthBox.height * 1.5f), healthBox);
            GUI.Label(new Rect((Screen.width * .98f) - (healthBox.width * .75f), Screen.height * .023f, healthBox.width, healthBox.height * 1.5f), myStatus.health.ToString());

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
    }

    public void SetUpNNaviValues(int playerNum, Transform en)
    {
        enemy = en;
        myStatus = GetComponent<CharBasic>();
        myStatus.SetUpBasicValues(playerNum, GetComponentsInChildren<Animator>()[0], 100);
        anim = GetComponent<Animator>();
        chargeAnim = GetComponentsInChildren<Animator>()[1];
        numOfMoves = Mathf.Floor(Random.Range(1, 7.99f));
    }

    void Update()
    {
        if (enemyStatus == null)
            enemyStatus = enemy.GetComponentInChildren<CharBasic>();

        myStatus.BasicUpdate();

        if (!myStatus.disableInput && !anim.GetBool("isHit"))
        {
            AIUpdate();
        }

        UpdateCooldowns();
    }

    void AIUpdate()
    {
        if (numOfBuster < 3)
        {
            if (!weChargin)
            {
                if (enemyStatus.row == myStatus.row)
                {
                    numOfBuster++;
                    ShouldWeBust();
                }
            }
            else
            {
                Charge();
                if (charge == 100 && enemyStatus.row == myStatus.row)
                {
                    numOfBuster++;
                    StartCoroutine("Buster");
                }
            }
        }

        if (updateTime >= 1)
        {
            if (currMoves >= numOfMoves && !weChargin)
            {
                DoSomething();
            }
            else
                StartCoroutine("AIMove");
        }
        else
            updateTime += Time.deltaTime;

    }

    void ShouldWeBust()
    {
        int prob = Random.Range(1, 3);

        if(prob == 1)
            StartCoroutine("Buster");
    }

    void DoSomething()
    {
        numOfBuster = 0;
        updateTime = 0;
        bool somethingDone = false;

        do
        {
            int prob = Random.Range(1, 5);

            if (prob == 1 && curQCooldown == 0)
            {
                StartCoroutine("AICannon");

                currMoves = 0;
                numOfMoves = Mathf.Floor(Random.Range(1, 7.99f));
                somethingDone = true;
            }

            if (prob == 2 && curWCooldown == 0 && (enemyStatus.column >= (myStatus.minCol - 2)))
            {
                StartCoroutine("AISword");

                currMoves = 0;
                numOfMoves = Mathf.Floor(Random.Range(1, 7.99f));
                somethingDone = true;
            }

            if (prob == 3 && curECooldown == 0 && !isHealing && myStatus.health != myStatus.maxHealth)
            {
                StartCoroutine("Heal10");

                currMoves = 0;
                numOfMoves = Mathf.Floor(Random.Range(1, 7.99f));
                somethingDone = true;
            }

            if (prob == 4 && curRCooldown == 0 && damageAdd != 30)
            {
                StartCoroutine("Att30");

                currMoves = 0;
                numOfMoves = Mathf.Floor(Random.Range(1, 7.99f));
                somethingDone = true;
            }

            if (!(prob == 1 && curQCooldown == 0) && (prob == 2 && curWCooldown == 0 && !(enemyStatus.column >= (myStatus.minCol - 2)))
                && !(prob == 3 && curECooldown == 0 && !isHealing && myStatus.health != myStatus.maxHealth) && !(prob == 4 && curRCooldown == 0 && damageAdd != 30))
            {
                somethingDone = true;
                currMoves = 0;
                numOfMoves = Mathf.Floor(Random.Range(1, 7.99f));
            }

        } while (!somethingDone);
    }

    IEnumerator AISword()
    {
        float deltaX = myStatus.minCol - myStatus.column;
        float deltaY = enemyStatus.row - myStatus.row;

        myStatus.disableInput = true;
        anim.SetBool("move1", true);
        yield return new WaitForSeconds(.05f);
        float animLength = anim.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animLength - .05f);
        anim.SetBool("move1", false);

        transform.position = new Vector3(transform.position.x + (deltaX * 3), transform.position.y - (deltaY * 2));
        myStatus.column = myStatus.minCol;
        myStatus.row = enemyStatus.row;

        anim.SetBool("move2", true);
        yield return new WaitForSeconds(.05f);
        animLength = anim.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animLength - .05f);
        anim.SetBool("move2", false);
        myStatus.disableInput = false;

        StartCoroutine("Sword");
    }

    IEnumerator AICannon()
    {
        //Move then Use Cannon
        float newX = Random.Range(myStatus.minCol, myStatus.maxCol);
        float deltaX = newX - myStatus.column;
        float deltaY = enemyStatus.row - myStatus.row;

        myStatus.disableInput = true;
        anim.SetBool("move1", true);
        yield return new WaitForSeconds(.05f);
        float animLength = anim.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animLength - .05f);
        anim.SetBool("move1", false);

        transform.position = new Vector3(transform.position.x + (deltaX * 3), transform.position.y - (deltaY * 2));
        myStatus.column = newX;
        myStatus.row = enemyStatus.row;

        anim.SetBool("move2", true);
        yield return new WaitForSeconds(.05f);
        animLength = anim.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animLength - .05f);
        anim.SetBool("move2", false);
        myStatus.disableInput = false;

        StartCoroutine("Cannon");
    }

    IEnumerator AIMove()
    {
        //Move then use Sword
        numOfBuster = 0;
        updateTime = 0;
        Vector2 newPos;
        newPos.x = Mathf.Floor(Random.Range(myStatus.minCol, myStatus.maxCol+ .99f));
        newPos.y = Mathf.Floor(Random.Range(1, 3.99f));

        float deltaX = newPos.x - myStatus.column;
        float deltaY = newPos.y - myStatus.row;

        myStatus.disableInput = true;
        anim.SetBool("move1", true);
        yield return new WaitForSeconds(.05f);
        float animLength = anim.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animLength - .05f);

        transform.position = new Vector3(transform.position.x + (deltaX * 3), transform.position.y - (deltaY * 2));
        myStatus.column = newPos.x;
        myStatus.row = newPos.y;
        currMoves++;

        anim.SetBool("move2", true);
        yield return new WaitForSeconds(.05f);
        animLength = anim.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animLength - .05f);
        anim.SetBool("move1", false);
        anim.SetBool("move2", false);
        myStatus.disableInput = false;

        ShouldWeCharge();
    }

    void ShouldWeCharge()
    {
        int prob = Random.Range(1, 5);

        if (prob == 4)
            weChargin = true;
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
        weChargin = false;
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
        if (myStatus.playerNo == 1)
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
        isHealing = true;
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
            if (myStatus.health > myStatus.maxHealth)
                myStatus.health = myStatus.maxHealth;
        }
        isHealing = false;
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
