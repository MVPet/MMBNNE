using UnityEngine;
using System.Collections;

public class CharBasic : MonoBehaviour {

    // values every charcter should have
    public int health;
    public int maxHealth = 250;
    public int minCol;
    public int maxCol;
    public int playerNo;
    public float column;
    public float row;
    Animator anim;
    public bool isAlive = true;

    public bool disableInput;

    // Set up our basic values
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

    // Update once per frame
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

    // Check on Player 1 once per frame
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

    // Check on Player 2 once per frame
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

    // Move right
    // set the booleans for animations
    // set values
    // play animation
    // animation done, reset values
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

    // Move left
    // set the booleans for animations
    // set values
    // play animation
    // animation done, reset values
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

    // Move up
    // set the booleans for animations
    // set values
    // play animation
    // animation done, reset values
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

    // Move down
    // set the booleans for animations
    // set values
    // play animation
    // animation done, reset values
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

    // We took damage...
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

    // ...and we got stunned
    IEnumerator HitStun()
    {
        anim.SetBool("isHit", true);
        yield return new WaitForSeconds(.1f);
        float animLength = anim.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animLength - .1f);

        anim.SetBool("isHit", false);
    }
}
