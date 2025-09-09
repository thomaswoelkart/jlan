using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemybehavior : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;

    public Rigidbody2D rd;
    public BoxCollider2D myBoxCollider;

    void Start()
    {
      
    }

    void Update()
    {
        if(IsFacingRight())
        {
            rd.velocity = new Vector3(-moveSpeed, 0f, 10f);
        }
        else
        {
            rd.velocity = new Vector3(moveSpeed, 0f, 10f);
        }
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > Mathf.Epsilon;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("OnTriggerExit2D");
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 10f);
    }
}
