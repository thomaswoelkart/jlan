using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static SaveStats;

public class PlayerMovement : MonoBehaviour
{
    int levelanzahl = 10;

    public AnimatorOverrideController terukazu;
    public AnimatorOverrideController akira;
    public AnimatorOverrideController supaman;
    public AnimatorOverrideController teshi;
    public AnimatorOverrideController jlan;

    public AnimatorOverrideController backHandSword;
    public AnimatorOverrideController dagger;
    public AnimatorOverrideController TwoHandSword;

    CharacterController2D controller;
    Animator animator;
    Animator swordAnimator;
    GameObject Player;
    public float CameraMove = 0.25f;
    public float runSpeed = 40f;
    LifeScript life;
    GameObject CoinText;
    GameObject Coins;
    GameObject SpawnPunkt;
    int aktuellesLevel;
    GameObject TimeObject;
    PauseResume PauseResumeObject;
    public GameObject FinishMenu;
    GameObject[] Stars;
    public Sprite FullStar;
    public Sprite NoStar;

    GameObject HealGadget;
    GameObject JumpGadget;
    GameObject SpeedGadget;
    TextMeshProUGUI HealText;
    TextMeshProUGUI JumpText;
    TextMeshProUGUI SpeedText;
    TextMeshProUGUI HealCounter;
    TextMeshProUGUI JumpCounter;
    TextMeshProUGUI SpeedCounter;
    bool waitHeal = false;
    bool waitJump = false;
    bool waitSpeed = false;

    public float SecondsFor3Stars = 30f;
    public float SecondsFor2Stars = 60f;
    public float SecondsFor1Stars = 90f;

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;


    //niki sachen
    public AudioClip jumpSoundClip;
    AudioSource jumpSound;
    private float volume;
    //niki sachen ende

    bool isJumping = false;

    bool finish = false;

    float time;

    string curEnemy = "";

    void Start()
    {
        //---------------------------------------GameObject.FindWithTag---------------------------------------

        Player = GameObject.Find("Player");
        controller = Player.GetComponent<CharacterController2D>();
        animator = Player.GetComponent<Animator>();
        swordAnimator = GameObject.Find("Sword").GetComponent<Animator>();
        life = GameObject.Find("EventSystem").GetComponent<LifeScript>();
        CoinText = GameObject.Find("CoinText");
        Coins = GameObject.Find("Coins");
        SpawnPunkt = GameObject.Find("SpawnPunkt");
        TimeObject = GameObject.Find("Time");
        PauseResumeObject = GameObject.Find("EventSystem").GetComponent<PauseResume>();
        HealGadget = GameObject.Find("HealGadget");
        JumpGadget = GameObject.Find("JumpGadget");
        SpeedGadget = GameObject.Find("SpeedGadget");
        HealText = GameObject.Find("HealText").GetComponent<TextMeshProUGUI>();
        JumpText = GameObject.Find("JumpText").GetComponent<TextMeshProUGUI>();
        SpeedText = GameObject.Find("SpeedText").GetComponent<TextMeshProUGUI>();
        HealCounter = GameObject.Find("HealCounter").GetComponent<TextMeshProUGUI>();
        JumpCounter = GameObject.Find("JumpCounter").GetComponent<TextMeshProUGUI>();
        SpeedCounter = GameObject.Find("SpeedCounter").GetComponent<TextMeshProUGUI>();
        jumpSound = GameObject.Find("EventSystem").GetComponent<AudioSource>();
        //FinishMenu = GameObject.Find("FinishMenu");
        volume = (float)SaveScript.Sound;


        //SaveScript.SelectedPlayer = 2;

        switch (SaveScript.SelectedPlayer)
        {
            case 0:
                animator.runtimeAnimatorController = jlan as RuntimeAnimatorController;
                break;
            case 1:
                animator.runtimeAnimatorController = teshi as RuntimeAnimatorController;
                break;
            case 2:
                animator.runtimeAnimatorController = terukazu as RuntimeAnimatorController;
                break;
            case 3:
                animator.runtimeAnimatorController = akira as RuntimeAnimatorController;
                break;
            case 4:
                animator.runtimeAnimatorController = supaman as RuntimeAnimatorController;
                break;
        }
        switch (SaveScript.SelectedSwords)
        {
            case 0:
                swordAnimator.runtimeAnimatorController = backHandSword as RuntimeAnimatorController;
                break;
            case 1:
                swordAnimator.runtimeAnimatorController = TwoHandSword  as RuntimeAnimatorController;
                break;
            case 2:
                swordAnimator.runtimeAnimatorController = dagger as RuntimeAnimatorController;
                break;
        }
        Stars = new GameObject[3];

        //----------------------------------------------------------------------------------------------------
        //Player zum SpawnPunkt
        Player.transform.position = SpawnPunkt.transform.position;

        //Player kann nur einmal eine Münze einsammeln (den CircleCollider2D ignoren)
        foreach (GameObject g in Resources.FindObjectsOfTypeAll(typeof(GameObject)))
        {
            if (g.GetComponent<Collider2D>() != null && (g.layer == 7 || g.layer == 8))
                Physics2D.IgnoreCollision(Player.GetComponent<BoxCollider2D>(), g.GetComponent<Collider2D>());
        }
        SetCoins();

        HealText.text = SaveScript.Heal + "";
        JumpText.text = SaveScript.Jump + "";
        SpeedText.text = SaveScript.Speed + "";
        HealCounter.text = "";
        JumpCounter.text = "";
        SpeedCounter.text = "";

        aktuellesLevel = int.Parse(SceneManager.GetActiveScene().name.Substring(5));
    }
    // Update is called once per frame
    void Update()
    {
        if (!life.AmIDead() && !finish)
        {
            Timer();

            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
            swordAnimator.SetFloat("Speed", Mathf.Abs(horizontalMove));

            if (Input.GetButtonDown("Jump"))
            {
                
                jump = true;
                //animator.ResetTrigger("isAttacking");
                animator.SetBool("IsJumping", true);
                swordAnimator.SetBool("IsJumping", true);
                if (!isJumping)
                {
                    jumpSound.PlayOneShot(jumpSoundClip, volume);
                    isJumping = true;
                }


                //print("true");
            }

            if (Input.GetButtonDown("Crouch"))
                crouch = true;
            else if (Input.GetButtonUp("Crouch"))
                crouch = false;

            if (Player.transform.position.y < -10)
            {
                life.SetLeben(0, false);
            }

            if (Input.GetButtonDown("HealGadget") && !waitHeal)
            {
                UseHeal();
            }
            if (Input.GetButtonDown("JumpGadget") && !waitJump)
            {
                UseJump();
            }
            if (Input.GetButtonDown("SpeedGadget") && !waitSpeed)
            {
                UseSpeed();
            }
        }
    }

    public void UseHeal()
    {
        if (SaveScript.HealShop <= 0)
            return;
        SaveScript.Heal = SaveScript.HealShop - 1;
        HealText.text = SaveScript.Heal + "";
        HealGadget.GetComponent<Button>().enabled = false;
        life.SetLeben(6, true);
        StartCoroutine(waitUseHeal());
    }
    IEnumerator waitUseHeal()
    {
        waitHeal = true;
        for (int i = 10; i >= 0; i--)
        {
            HealCounter.text = i + "";
            yield return new WaitForSeconds(1);
        }
        HealCounter.text = "";
        waitHeal = false;
        //HealGadget.GetComponent<Button>().enabled = true;
    }
    public void UseJump()
    {
        if (SaveScript.JumpShop <= 0)
            return;
        SaveScript.Jump = SaveScript.JumpShop - 1;
        JumpText.text = SaveScript.Jump + "";
        JumpGadget.GetComponent<Button>().enabled = false;
        StartCoroutine(waitUseJump());
    }
    IEnumerator waitUseJump()
    {
        waitJump = true;
        controller.m_JumpForce += 300;
        for (int i = 10; i >= 0; i--)
        {
            JumpCounter.text = i + "";
            yield return new WaitForSeconds(1);
        }
        JumpCounter.text = "";
        controller.m_JumpForce -= 300;
        waitJump = false;
        //HealGadget.GetComponent<Button>().enabled = true;
    }
    public void UseSpeed()
    {
        if (SaveScript.SpeedShop <= 0)
            return;
        SaveScript.Speed = SaveScript.SpeedShop -1;
        SpeedText.text = SaveScript.Speed + "";
        SpeedGadget.GetComponent<Button>().enabled = false;
        StartCoroutine(waitUseSpeed());
    }
    IEnumerator waitUseSpeed()
    {
        waitSpeed = true;
        runSpeed *= 1.5f;
        for (int i = 10; i >= 0; i--)
        {
            SpeedCounter.text = i + "";
            yield return new WaitForSeconds(1);
        }
        SpeedCounter.text = "";
        runSpeed /= 1.5f;
        waitSpeed = false;
        //HealGadget.GetComponent<Button>().enabled = true;
    }
    void Timer()
    {
        time += Time.deltaTime;

        int minute = (int)time / 60;
        int second = (int)time % 60;
        int millisecond = (int)((time - (minute * 60 + second)) * 100);

        TimeObject.GetComponent<TextMeshProUGUI>().text = (minute < 10 ? "0" + minute : minute + "") + ":" + (second < 10 ? "0" + second : second + "") + ":" + (millisecond < 10 ? "0" + millisecond : millisecond + "");

        //TimeObject.GetComponent<TextMeshProUGUI>().text = time.ToString();
        //TimeObject.GetComponent<TextMeshProUGUI>().text = (time.Minute < 10 ? "0" + time.Minute : time.Minute + "") + ":" + (time.Second < 10 ? "0" + time.Second : time.Second + "") + ":" + (time.Millisecond < 10 ? "0" + time.Millisecond : time.Millisecond + "");
    }
    public void OnLanding()
    {
        isJumping = false;
        animator.SetBool("IsJumping", false);
        swordAnimator.SetBool("IsJumping", false);
    }
    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == 7)
        {
            collider.gameObject.SetActive(false);
            SaveScript.Coins = SaveScript.CoinsShop + 1;
            SetCoins();
        }
        if (collider.gameObject.layer == 8)
        {
            //Ziel
            //Debug.Log("Ziel");
            int load = SaveScript.Level;
            if (load == aktuellesLevel)
                SaveScript.Level = load + 1;
            PauseResumeObject.Pause();
            FinsihMenuActive(true);
            finish = true;

            switch (SetStars())
            {
                case 1:
                    if (GetStarIndex(SaveScript.Sterne[aktuellesLevel-1]) < GetStarIndex(LevelSterne.One))
                        SaveScript.SetStar(aktuellesLevel-1, LevelSterne.One);
                    break;
                case 2:
                    if (GetStarIndex(SaveScript.Sterne[aktuellesLevel-1]) < GetStarIndex(LevelSterne.Two))
                        SaveScript.SetStar(aktuellesLevel-1, LevelSterne.Two);
                    break;
                case 3:
                    if (GetStarIndex(SaveScript.Sterne[aktuellesLevel-1]) < GetStarIndex(LevelSterne.Three))
                        SaveScript.SetStar(aktuellesLevel-1, LevelSterne.Three);
                    break;
                default:
                    if (GetStarIndex(SaveScript.Sterne[aktuellesLevel-1]) < GetStarIndex(LevelSterne.NotStarted))
                        SaveScript.SetStar(aktuellesLevel-1, LevelSterne.NotStarted);
                    break;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9 && !collision.transform.parent.gameObject.GetComponent<SnakeScript>().wait && !collision.transform.parent.gameObject.GetComponent<Animator>().GetBool("Dead"))
        {
            curEnemy = "snake";
            collision.transform.parent.gameObject.GetComponent<SnakeScript>().isAttack = true;
            collision.transform.parent.gameObject.GetComponent<SnakeScript>().Attack();
            //collision.gameObject.GetComponentInParent<SnakeScript>().Attack();
        }
        else if (collision.gameObject.layer == 10 && !collision.transform.parent.gameObject.GetComponent<BatScript>().wait && !collision.transform.parent.gameObject.GetComponent<Animator>().GetBool("IsDead"))
        {
            curEnemy = "bat";
            collision.transform.parent.gameObject.GetComponent<BatScript>().isAttack = true;
            collision.transform.parent.gameObject.GetComponent<BatScript>().Attack();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9 || collision.gameObject.layer == 10)
        {
            if (curEnemy == "snake")
            {
                SnakeScript snakeScript = collision?.transform?.parent?.gameObject?.GetComponent<SnakeScript>();
                if(snakeScript != null)
                    snakeScript.isAttack = false;
                //print("exit " + curEnemy);
            }
            else
            {
                BatScript batScript = collision?.transform?.parent?.gameObject?.GetComponent<BatScript>();
                if(batScript != null)
                    batScript.isAttack = false;
                //print("exit " + curEnemy);
            }
        }
    }
    int GetStarIndex(LevelSterne star)
    {
        switch (star)
        {
            case LevelSterne.One:
                return 1;
            case LevelSterne.Two:
                return 2;
            case LevelSterne.Three:
                return 3;
            default:
                return 0;
        }
    }
    int SetStars()
    {
        int stars = 0;
        Stars[0] = GameObject.Find("Star1");
        Stars[1] = GameObject.Find("Star2");
        Stars[2] = GameObject.Find("Star3");

        Stars[0].GetComponent<SpriteRenderer>().sprite = NoStar;
        Stars[1].GetComponent<SpriteRenderer>().sprite = NoStar;
        Stars[2].GetComponent<SpriteRenderer>().sprite = NoStar;

        if (time <= SecondsFor1Stars)
        {
            Stars[0].GetComponent<SpriteRenderer>().sprite = FullStar;
            stars++;
        }
        if (time <= SecondsFor2Stars)
        {
            Stars[1].GetComponent<SpriteRenderer>().sprite = FullStar;
            stars++;
        }
        if (time <= SecondsFor3Stars)
        {
            Stars[2].GetComponent<SpriteRenderer>().sprite = FullStar;
            stars++;
        }
        return stars;
    }
    void SetCoins()
    {
        CoinText.GetComponent<TextMeshProUGUI>().text = SaveScript.Coins + "";
    }
    public void FinsihMenuActive(bool active)
    {
        FinishMenu.SetActive(active);
        if(int.Parse(SceneManager.GetActiveScene().name.Substring(5)) == levelanzahl)
            FinishMenu.gameObject.transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
    }
}
