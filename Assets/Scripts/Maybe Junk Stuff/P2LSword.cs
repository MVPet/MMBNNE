using UnityEngine;
using System.Collections;

public class P2LSword : MonoBehaviour {

    Animator anim;
    int col;
    int row;
    int damAdd = 0;
    CharBasic eHealth;

    void Awake()
    {
        anim = GetComponent<Animator>();
        StartCoroutine("JustSwordThings");
    }

    public void SetUpValues(int column, int rowIn, PlayerHealth enemy, int damageAdd)
    {
        damAdd = damageAdd;
        col = column;
        //eHealth = enemy;
        row = rowIn;
    }

    IEnumerator JustSwordThings()
    {
        yield return new WaitForSeconds(.1f);
        float animLength = anim.GetCurrentAnimatorStateInfo(0).length;
        CheckHit();
        yield return new WaitForSeconds(animLength - .1f);
        Destroy(this.gameObject);
    }

    void CheckHit()
    {
        if (eHealth.row == row)
        {
            if (eHealth.column == col || eHealth.column == (col - 1))
                eHealth.TakeDamage(80 + damAdd, true);
        }
    }
}
