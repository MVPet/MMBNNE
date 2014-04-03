using UnityEngine;
using System.Collections;

public class Player2Controller : MonoBehaviour
{
    int maxCol = 6;
    int column;
    int row;
    Animator anim;
    public bool isAlive = true;

    public Transform enemy;
    PlayerHealth eHealth;
    public bool disableInput = true;

    public Transform LSword;
    public Color newColor = Color.white;


    int charge = 0;
    float delay = 0;
    int damageAdd = 0;

    float QCooldown = 7f;
    public float curQCooldown = 0f;

    float WCooldown = 7f;
    public float curWCooldown = 0f;

    float ECooldown = 10f;
    public float curECooldown = 0f;

    float RCooldown = 15f;
    public float curRCooldown = 0f;

    Animator cAnim;

    Animator[] animators;

    void Start()
    {
        column = 5;
        row = 2;
        animators = GetComponentsInChildren<Animator>();
        anim = animators[0];
        cAnim = animators[1];
        eHealth = enemy.GetComponentInChildren<PlayerHealth>();

        GetComponent<PlayerHealth>().column = 5;
        GetComponent<PlayerHealth>().row = 2;
    }

    void Update()
    {

        anim.SetBool("isAlive", isAlive);

        if (!isAlive)
        {
            GetComponent<SpriteRenderer>().color = Color.Lerp(GetComponent<SpriteRenderer>().color, new Color(1, 1, 1, 0), Time.deltaTime * 2f);
            anim.SetBool("isHit", false);
        }

        if (!disableInput && !anim.GetBool("isHit"))
        {
            if (Input.GetKeyDown(KeyCode.L))
                MoveRight();
            if (Input.GetKeyDown(KeyCode.J))
                MoveLeft();
            if (Input.GetKeyDown(KeyCode.I))
                MoveUp();
            if (Input.GetKeyDown(KeyCode.K))
                MoveDown();

            if (Input.GetKey(KeyCode.Space))
                Charge();
            if (Input.GetKeyUp(KeyCode.Space))
                StartCoroutine("Buster");

            if (Input.GetKeyUp(KeyCode.Y) && curQCooldown == 0)
                StartCoroutine("Cannon");

            if (Input.GetKeyUp(KeyCode.U) && curWCooldown == 0)
                StartCoroutine("Sword");

            if (Input.GetKeyUp(KeyCode.O) && curECooldown == 0)
                StartCoroutine("Heal10");

            if (Input.GetKeyUp(KeyCode.P) && curRCooldown == 0)
                StartCoroutine("Att30");
        }

        UpdateCooldowns();
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
        delay += Time.deltaTime;

        if (charge < 100 && delay > .1f)
        {
            charge += 1;
            cAnim.SetInteger("ChargeLevel", charge);
        }
    }

    IEnumerator Buster()
    {
        delay = 0;
        cAnim.SetInteger("ChargeLevel", 0);
        anim.SetBool("Buster", true);
        disableInput = true;
        yield return new WaitForSeconds(.1f);
        float animLength = anim.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animLength - .1f);
        CheckBusterHit();

        anim.SetBool("Buster", false);
        charge = 0;
        damageAdd = 0;
        disableInput = false;
    }

    IEnumerator Cannon()
    {
        anim.SetBool("QAbility", true);
        disableInput = true;
        curQCooldown = QCooldown;
        charge = 0;
        delay = 0;
        cAnim.SetInteger("ChargeLevel", charge);
        yield return new WaitForSeconds(.1f);
        float animLength = anim.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animLength - .1f);
        CheckCannonHit();

        anim.SetBool("QAbility", false);
        damageAdd = 0;
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
        disableInput = false;
    }

    IEnumerator Sword()
    {
        anim.SetBool("WAbility", true);
        disableInput = true;
        curWCooldown = WCooldown;
        charge = 0;
        delay = 0;
        cAnim.SetInteger("ChargeLevel", charge);
        yield return new WaitForSeconds(.1f);
        float animLength = anim.GetCurrentAnimatorStateInfo(0).length;
        Transform sword = Instantiate(LSword, new Vector3(transform.position.x -1.5f, transform.position.y - 3f, -3), Quaternion.identity) as Transform;
        sword.GetComponent<P2LSword>().SetUpValues(column - 1, row, eHealth, damageAdd);
        yield return new WaitForSeconds(animLength - .1f);

        anim.SetBool("WAbility", false);
        damageAdd = 0;
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
        disableInput = false;
    }

    IEnumerator Heal10()
    {
        //Missing animations
        curECooldown = ECooldown;
        charge = 0;
        delay = 0;
        yield return new WaitForSeconds(0f);
        GetComponent<PlayerHealth>().health += 10;
    }

    IEnumerator Att30()
    {
        //missing animations
        curRCooldown = RCooldown;
        charge = 0;
        delay = 0;
        yield return new WaitForSeconds(0f);
        GetComponent<SpriteRenderer>().color = new Color(0, 1, 0);
        damageAdd = 30;
    }

    void CheckCannonHit()
    {
        if (eHealth.row == row)
        {
            eHealth.TakeDamage(40 + damageAdd, true);
        }
    }

    void CheckBusterHit()
    {
        if (eHealth.row == row)
        {
            if (charge == 100)
                eHealth.TakeDamage(10, false);
            else
                eHealth.TakeDamage(1, false);
        }
    }

    void MoveRight()
    {
        if (column != maxCol)
        {
            column++;
            GetComponent<PlayerHealth>().column = column;
            transform.position = new Vector3(transform.position.x + 3, transform.position.y, transform.position.z);
        }
    }

    void MoveLeft()
    {
        if (column != 4)
        {
            column--;
            GetComponent<PlayerHealth>().column = column;
            transform.position = new Vector3(transform.position.x - 3, transform.position.y, transform.position.z);
        }
    }

    void MoveUp()
    {
        if (row != 1)
        {
            row--;
            GetComponent<PlayerHealth>().row = row;
            transform.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
        }
    }

    void MoveDown()
    {
        if (row != 3)
        {
            row++;
            GetComponent<PlayerHealth>().row = row;
            transform.position = new Vector3(transform.position.x, transform.position.y - 2, transform.position.z);
        }
    }
}
