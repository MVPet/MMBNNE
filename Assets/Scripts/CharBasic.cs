using UnityEngine;
using System.Collections;

public class CharBasic : MonoBehaviour {

    public int health;
    public int maxHealth = 100;
    public int minCol;
    public int maxCol;
    public int playerNo;
    public float column;
    public float row;
    Animator anim;
    public bool isAlive = true;

    public bool disableInput;

    public void SetUpBasicValues(int playerNum, Animator moveAnim, int recHealth)
    {
        playerNo = playerNum;
        health = recHealth;

        if (playerNo == 1)
        {
            minCol = 1;
            maxCol = 3;
            column = 2;
            row = 2;
        }
        else if (playerNo == 2)
        {
            minCol = 4;
            maxCol = 6;
            column = 5;
            row = 2;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }

        anim = moveAnim;
        isAlive = true;

        disableInput = true;
    }

    public void BasicUpdate()
    {
         anim.SetBool("isAlive", isAlive);

        if (!isAlive)
        {
            GetComponent<SpriteRenderer>().color = Color.Lerp(GetComponent<SpriteRenderer>().color, new Color(1, 1, 1, 0), Time.deltaTime * 2f);
            anim.SetBool("isHit", false);
        }

        if (!disableInput && !anim.GetBool("isHit"))
        {
            switch (playerNo)
            {
                case 1:
                    P1Update();
                    break;
                case 2:
                    P2Update();
                    break;
            }
        }
    }

    void P1Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            StartCoroutine("MoveRight");
        if (Input.GetKeyDown(KeyCode.S))
            StartCoroutine("MoveLeft");
        if (Input.GetKeyDown(KeyCode.E))
            StartCoroutine("MoveUp");
        if (Input.GetKeyDown(KeyCode.D))
            StartCoroutine("MoveDown");
    }

    void P2Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            StartCoroutine("MoveRight");
        if (Input.GetKeyDown(KeyCode.J))
            StartCoroutine("MoveLeft");
        if (Input.GetKeyDown(KeyCode.I))
            StartCoroutine("MoveUp");
        if (Input.GetKeyDown(KeyCode.K))
            StartCoroutine("MoveDown");
    }



    IEnumerator MoveRight()
    {
        if (column != maxCol)
        {
            disableInput = true;
            anim.SetBool("move1", true);
            yield return new WaitForSeconds(.05f);
            float animLength = anim.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(animLength - .05f);
            column++;
            transform.position = new Vector3(transform.position.x + 3, transform.position.y, transform.position.z);
            anim.SetBool("move2", true);
            yield return new WaitForSeconds(.05f);
            animLength = anim.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(animLength - .05f);
            anim.SetBool("move1", false);
            anim.SetBool("move2", false);
            disableInput = false;
        }
    }

    IEnumerator MoveLeft()
    {
        if (column != minCol)
        {
            disableInput = true;
            anim.SetBool("move1", true);
            yield return new WaitForSeconds(.05f);
            float animLength = anim.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(animLength - .05f);
            column--;
            transform.position = new Vector3(transform.position.x - 3, transform.position.y, transform.position.z);
            anim.SetBool("move2", true);
            yield return new WaitForSeconds(.05f);
            animLength = anim.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(animLength - .05f);
            anim.SetBool("move1", false);
            anim.SetBool("move2", false);
            disableInput = false;
        }
    }

    IEnumerator MoveUp()
    {
        if (row != 1)
        {
            disableInput = true;
            anim.SetBool("move1", true);
            yield return new WaitForSeconds(.05f);
            float animLength = anim.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(animLength - .05f);
            row--;
            transform.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
            anim.SetBool("move2", true);
            yield return new WaitForSeconds(.05f);
            animLength = anim.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(animLength - .05f);
            anim.SetBool("move1", false);
            anim.SetBool("move2", false);
            disableInput = false;
        }
    }

    IEnumerator MoveDown()
    {
        if (row != 3)
        {
            disableInput = true;
            anim.SetBool("move1", true);
            yield return new WaitForSeconds(.05f);
            float animLength = anim.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(animLength - .05f);
            row++;
            transform.position = new Vector3(transform.position.x, transform.position.y - 2, transform.position.z);
            anim.SetBool("move2", true);
            yield return new WaitForSeconds(.05f);
            animLength = anim.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(animLength - .05f);
            anim.SetBool("move1", false);
            anim.SetBool("move2", false);
            disableInput = false;
        }
    }

    public void TakeDamage(int damage, bool hitStun)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
        }
        if (hitStun)
        {
            StartCoroutine("HitStun");
        }
    }

    IEnumerator HitStun()
    {
        anim.SetBool("isHit", true);
        yield return new WaitForSeconds(.1f);
        float animLength = anim.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animLength - .1f);

        anim.SetBool("isHit", false);
    }
}
