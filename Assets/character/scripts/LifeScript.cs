using System.Collections;
using System.Collections.Generic;
using System.Drawing;
//using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.UI;

public class LifeScript : MonoBehaviour
{
    GameObject[] Heart = new GameObject[3];
    public Sprite FullHeart;
    public Sprite HalfHeart;
    public Sprite NoHeart;
    GameObject Player;
    public int leben = 6;

    public GameObject DeadMenu;

    bool dead = false;
    bool wait = false;
    bool waitDie = false;

    // Start is called before the first frame update
    void Start()
    {
        //---------------------------------------GameObject.FindWithTag---------------------------------------

        Player = GameObject.Find("Player");
        Heart[0] = GameObject.Find("Heart1");
        Heart[1] = GameObject.Find("Heart2");
        Heart[2] = GameObject.Find("Heart3");

        //----------------------------------------------------------------------------------------------------

        SetLeben(leben, true);
    }
    // Update is called once per frame
    void Update()
    {
        if (!dead && !wait)
            StartCoroutine(LebenEverySecond());
    }
    IEnumerator LebenEverySecond()
    {
        wait = true;
        yield return new WaitForSeconds(15);
        wait = false;
        AddLeben(1);
    }
    void Die(bool timer)
    {
        dead = true;
        if (!waitDie)
        {
            if(timer)
                StartCoroutine(DieTimer());
            else
            {
                DeadMenu.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
    IEnumerator DieTimer()
    {
        waitDie = true;
        Player.GetComponent<Animator>().SetTrigger("IsDead");
        yield return new WaitForSeconds(1.5f);
        DeadMenu.SetActive(true);
        waitDie = false;
    }
    public void AddLeben(int leben)
    {
        SetLeben(this.leben + leben, true);
    }
    public void DelLeben(int leben)
    {
        SetLeben(this.leben - leben, true);
    }
    public bool AmIDead()
    {
        return dead;
    }
    public void SetLeben(int leben, bool timer)
    {
        if (leben < 0)
            leben = 0;

        if (leben > 6)
            leben = 6;

        this.leben = leben;

        switch (leben)
        {
            case 0:
                Heart[0].GetComponent<SpriteRenderer>().sprite = NoHeart;
                Heart[1].GetComponent<SpriteRenderer>().sprite = NoHeart;
                Heart[2].GetComponent<SpriteRenderer>().sprite = NoHeart;
                Die(timer);
                break;
            case 1:
                Heart[0].GetComponent<SpriteRenderer>().sprite = HalfHeart;
                Heart[1].GetComponent<SpriteRenderer>().sprite = NoHeart;
                Heart[2].GetComponent<SpriteRenderer>().sprite = NoHeart;
                break;
            case 2:
                Heart[0].GetComponent<SpriteRenderer>().sprite = FullHeart;
                Heart[1].GetComponent<SpriteRenderer>().sprite = NoHeart;
                Heart[2].GetComponent<SpriteRenderer>().sprite = NoHeart;
                break;
            case 3:
                Heart[0].GetComponent<SpriteRenderer>().sprite = FullHeart;
                Heart[1].GetComponent<SpriteRenderer>().sprite = HalfHeart;
                Heart[2].GetComponent<SpriteRenderer>().sprite = NoHeart;
                break;
            case 4:
                Heart[0].GetComponent<SpriteRenderer>().sprite = FullHeart;
                Heart[1].GetComponent<SpriteRenderer>().sprite = FullHeart;
                Heart[2].GetComponent<SpriteRenderer>().sprite = NoHeart;
                break;
            case 5:
                Heart[0].GetComponent<SpriteRenderer>().sprite = FullHeart;
                Heart[1].GetComponent<SpriteRenderer>().sprite = FullHeart;
                Heart[2].GetComponent<SpriteRenderer>().sprite = HalfHeart;
                break;
            case 6:
                Heart[0].GetComponent<SpriteRenderer>().sprite = FullHeart;
                Heart[1].GetComponent<SpriteRenderer>().sprite = FullHeart;
                Heart[2].GetComponent<SpriteRenderer>().sprite = FullHeart;
                break;
        }
    }
}
