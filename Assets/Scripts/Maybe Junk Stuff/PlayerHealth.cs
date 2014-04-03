using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

    public int health = 100;
    public int column;
    public int row;
    Animator anim;

    public Texture q;
    public Texture w;
    public Texture e;
    public Texture r;


    void Start()
    {
        anim = GetComponent<Animator>();
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
