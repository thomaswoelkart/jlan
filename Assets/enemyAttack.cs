using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class enemyAttack : MonoBehaviour
{
    public Animator a;

    bool dead;
    public int maxHealth = 60;
    int currentHealth;

    void Update()
    {
        if (dead)
        {
            GameObject.Find("AttackPoint").SetActive(false);
            //print(GameObject.Find("AttackPoint"));
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        // a = GameObject.Find("snake").GetComponent<Animator>();   
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    public void TakeDamahe(int damage, GameObject type)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die(type);
        }
    }

    void Die(GameObject type)
    {
        //print(type);

        string name = type?.name;

        if (name != null)
        {
            if (type.name.Contains("snake"))
            {
                type.GetComponent<SnakeScript>().dead();
            }

            else if (type.name.Contains("bat"))
            {
                type.GetComponent<BatScript>().dead();
            }
            // GetComponent<>    
            //  a.SetBool("Dead", true);

            dead = false;
            GetComponent<Collider2D>().enabled = false;
            enabled = false;

            //print("Collider enable: " + GetComponent<Collider2D>().enabled + "\tSpeed: " + type.GetComponent<BatScript>().GetComponentInParent<AIPath>().maxSpeed);
        }
    }
}
