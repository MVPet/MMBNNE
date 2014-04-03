using UnityEngine;
using System.Collections;

public class LSword : MonoBehaviour {

    Animator anim;
    float col;
    float row;
    int damAdd = 0;
    int flipCheck = 1;
    CharBasic eHealth;

    void Awake()
    {
        anim = GetComponent<Animator>();
        StartCoroutine("JustSwordThings");
    }

    public void SetUpValues(float column, float rowIn, CharBasic enemy, int damageAdd, int parentPlayer, Vector2 pos)
    {
        transform.position = new Vector3(pos.x, pos.y, -3f);
        damAdd = damageAdd;
        col = column;
        eHealth = enemy;
        row = rowIn;
        if (parentPlayer == 2)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            flipCheck = -1;
        }
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
            if (eHealth.column == col || eHealth.column == (col + (1 * flipCheck)))
                eHealth.TakeDamage(80 + damAdd, true);
        }
    }
}
