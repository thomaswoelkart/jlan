using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class BatScript : MonoBehaviour
{
    public bool wait = false;
    public bool isAttack = true;
    public bool isDead = false;
    bool turned = false;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        //enemyAttack enemyAttack = GetComponent<enemyAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead)
        {
            transform.Translate(new Vector2(-1f, 1f) * 2 * Time.deltaTime);
        }
    }

    public void Attack()
    {
        
        if (isAttack && !wait && !GetComponent<Animator>().GetBool("IsDead") && !isDead) // && !(GetComponent<enemyAttack>().GetCurHealth() == 0)
        {
            GetComponent<AIPath>().maxSpeed = 0;
            transform.GetChild(0).GetComponent<Animator>().SetTrigger("IsAttacking"); //SetBool("IsAttacking", true);  dasdasdadsad
            //doDamage();
            StartCoroutine(AttackTicks(2));

        }
        else if (!isAttack && !isDead)
        {
            //GetComponentInParent<Animator>().SetBool("IsAttacking", false);
            print("EndAttack");
            GetComponent<AIPath>().maxSpeed = 3;
        }
    }

    public void dead()
    {
        isDead = true;
        if(!turned)
        {
            transform.Rotate(new Vector2(0, 180));
            turned = true;
        }
            
        //print("THIS BAT DEAD");
    }
    public void doDamage()
    {
        if (isAttack)
            GameObject.Find("EventSystem").GetComponent<LifeScript>().DelLeben(2);
    }

    IEnumerator AttackTicks(int ticks)
    {
        wait = true;
        yield return new WaitForSeconds(ticks);
        wait = false;

        Attack();
    }
}
