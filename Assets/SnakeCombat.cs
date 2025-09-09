using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeCombat : MonoBehaviour
{
    public Animator animator;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask playerLayer;

    public int snakeDmg = 20;

    //bool wait = false;

    //void Attack()
    //{
    //    print("Attack");

    //    //macht die Animation
    //    //animator.SetTrigger("IsAttacking");

    //    //schaut ob ein Gegner in Reichweite ist
    //    Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

    //    //Schaden machen
    //    foreach (Collider2D player in hitPlayer)
    //    {
    //        print("Dmg");

    //        // player.GetComponent<enemyAttack>().TakeDamahe(snakeDmg);
    //        StartCoroutine(TimeCounter());
    //    }

    //}
    /*
    IEnumerator TimeCounter()
    {
        wait = true;
        yield return new WaitForSeconds(5);
        wait = false;
    }
    */
    private void OnDrawGizmos()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    //void OnCollisionEnter2D(Collision2D col)
    //{
    //    print("collide");
    //    if (Input.GetButtonDown("Attack") && !wait)
    //    {
    //        Attack();
    //    }
    //}
}
