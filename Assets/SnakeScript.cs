using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeScript : MonoBehaviour
{
    public bool wait = false;
    public bool isAttack = true;
    public bool isDead = false;

    const string LEFT = "left";
    const string RIGHT= "right";

    //GameObject snakeAP = GameObject.Find("Snake_AttackPoint");

    [SerializeField]
    public Transform castPos;

    [SerializeField]
    float baseCastDist;

    string facingDirection;

    Vector3 baseScale;

    public Rigidbody2D rb2d;
    public float moveSpeed = 5;

    // Start is called before the first frame update
    void Start()
    {
        baseScale = transform.localScale;
        facingDirection = RIGHT;
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        float vX = moveSpeed;

        if(facingDirection == LEFT)
        {
            vX = -moveSpeed;
        }

        rb2d.velocity = new Vector2(vX, rb2d.velocity.y);

        if(HittingWall() || IsNearEdge()) //|| IsNearEdge()
        {
            if(facingDirection == LEFT)
                ChangeFacingDirection(RIGHT);
            else
                ChangeFacingDirection(LEFT);
        }
    }

    void ChangeFacingDirection(string newDirection)
    {
        Vector3 newScale = baseScale;

        if(newDirection == LEFT)
            newScale.x = -newScale.x;

        transform.localScale = newScale;

        facingDirection = newDirection;
    }

    bool HittingWall()
    {
        bool val = false;

        float castDist;

        if(facingDirection == LEFT)
            castDist = -baseCastDist;
        else
            castDist = -baseCastDist;

        Vector3 targetPos = castPos.position;
        targetPos.x += castDist;

        if (Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Default")))
            val = true;
        else
            val = false;

        return val;
    }

    bool IsNearEdge()
    {
        bool val = true;

        float castDist = baseCastDist;

        Vector3 targetPos = castPos.position;
        targetPos.y -= castDist;

        Debug.DrawLine(castPos.position, targetPos, Color.red);

        if (Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Default")))
            val = false;
        else
            val = true;

        return val;
    }

    public void dead()
    {
        isDead = true;
        GetComponent<Animator>().SetBool("Dead", true);

        GetComponent<BoxCollider2D>().enabled = false;
        moveSpeed = 0;
    }
    
    public void Attack()
    {
        if(isAttack && !wait && !GetComponent<Animator>().GetBool("Dead")) // && !(GetComponent<enemyAttack>().GetCurHealth() == 0)
        {
            //print("attack");
            moveSpeed = 0;
            GetComponentInParent<Animator>().SetTrigger("Hit"); //SetBool("IsAttacking", true);
            StartCoroutine(AttackTicks(2));
        }
        else if(!isAttack && !(GetComponent<Animator>().GetBool("Dead")))
        {
            //GetComponentInParent<Animator>().SetBool("IsAttacking", false);
            //print("EndAttack");
            moveSpeed = 2;
        }
    }

    public void doDamage()
    {
        if(isAttack)
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
