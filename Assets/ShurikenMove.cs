using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenMove : MonoBehaviour
{
    public int damage = 15;
    public float speed = 20f;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        enemyAttack enemy = hitInfo.GetComponent<enemyAttack>();
        if (hitInfo.name == "snake" || hitInfo.name == "AttackPointSnake" || hitInfo.name == "bat" || hitInfo.name == "AttackPointBat" || hitInfo.name == "AttackPoint")
        {
            if(hitInfo.name == "AttackPointSnake")
                enemy.TakeDamahe(damage, hitInfo.transform.parent.gameObject);
            else
                enemy.TakeDamahe(damage, hitInfo?.gameObject);
        }
        
        Destroy(gameObject);    
    }
}
