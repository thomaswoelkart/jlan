using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCompat : MonoBehaviour
{
    Animator animator;

    Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    public int PlayerDmg = 20;

    public AudioClip swordSoundClip;
    float volume;
    AudioSource soundPlayer;

    //bool waitToAttack;

    // Start is called before the first frame update
    void Start()
    {
        soundPlayer = GameObject.Find("EventSystem").GetComponent<AudioSource>();

        GameObject player = GameObject.Find("Player");

        animator = player.GetComponent<Animator>();

        attackPoint = GameObject.Find("PlayerAttackPoint").transform;

        volume = (float)SaveScript.Sound;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Attack"))
        {
            Attack();
        }
    }

    void Attack()
    {

        //waitToAttack = true;
        //macht die Animation
        animator.SetTrigger("IsAttacking");
        GameObject.Find("Sword").GetComponent<Animator>().SetTrigger("IsAttacking");
        soundPlayer.PlayOneShot(swordSoundClip, volume);
        //print(attackPoint.position + " " + attackRange.ToString() + " " + enemyLayers.ToString());

        //schaut ob ein Gegner in Reichweite ist
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);


        //Schaden machen
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy != null)
            {
                string name = enemy?.name == "snake" ? enemy?.name : enemy?.transform?.parent?.name;
                if (name != null)
                {
                    if (name == "bat")
                    {
                        //print("damage to " + name);
                        //print(enemy.transform.parent);
                        enemy.transform.parent.GetComponent<enemyAttack>().TakeDamahe(PlayerDmg, enemy.transform.parent.gameObject);
                    }
                    else if (name == "snake")
                    {
                        //print("damage to " + name);
                        enemy?.GetComponent<enemyAttack>()?.TakeDamahe(PlayerDmg, enemy?.gameObject);
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
