using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePos : MonoBehaviour
{
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
    }

    //public void AnimationHandler(string animation)
    //{
    //    Animator animator = GetComponent<Animator>();

    //    if (animation == "walk")
    //        animator.SetFloat(animation, player


    //        animator.SetTrigger(animation);
    //}
}
