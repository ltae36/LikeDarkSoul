using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class StatManager : DamageCount
{
    float fullHP = 454;
    float fullFP = 93;
    float fullStamina = 95;
    float playTime;

    public float HP;
    public float FP;
    public float stam;
    public bool inAction;

    public Slider hpSlider;
    public Slider fpSlider;
    public Slider stamSlider;

    public GameObject deadScene;

    public Animator anim;

    PlayerAttack isAttack;

    private bool isBeingIdle = false;

    public enum PlayerState 
    {
        idleNmove,
        dash,
        attack,
        defense,
        damaged,
        roll,
        move,
        dead
    }

    public PlayerState mystate = PlayerState.idleNmove;

    //private void Awake()
    //{
    //    anim = GetComponent<Animator>();
    //}

    void Start()
    {           
        deadScene.SetActive(false);

        hpSlider.maxValue = fullHP;
        hpSlider.minValue = 0;
        fpSlider.maxValue = fullFP;
        stamSlider.maxValue = fullStamina;

        HP = fullHP;
        FP = fullFP;
        stam = fullStamina;

        inAction = false;
    }

    void Update()
    {
        // 나의 상태에 따라서 스태미너 소모량과 패턴이 달라진다.
        switch (mystate) 
        {
            case PlayerState.idleNmove:
                inAction = false;
                idleNmove();
                break;
            case PlayerState.dash:
                dash();
                break;
            case PlayerState.attack:
                inAction = true;
                attack();
                break;
            case PlayerState.defense:
                inAction = true;
                defense();
                break;
            case PlayerState.damaged:
                inAction = true;
                damaged();
                break;
            case PlayerState.roll:
                roll();
                break;
            case PlayerState.move:
                move();
                break;
            case PlayerState.dead:
                dead();
                break;
        
        }

        hpSlider.value = HP;
        fpSlider.value = FP;
        stamSlider.value = stam;
        //if (Input.GetKey(KeyCode.Space))
        //{
        //    stam -= Time.deltaTime * usage;
        //}
        //else if(stam < fullStamina)
        //{
        //    stam += Time.deltaTime * usage;
        //}
    }

    private void dead()
    {
        deadScene.SetActive(true);
    }

    private void damaged()
    {
        if (isDamaged && HP != 0)
        {
            HP -= 90;
        }
        else if (HP <= 0) 
        {
            mystate = PlayerState.dead;
        }
        beingIdle(playTime);
    }

    private void move()
    {
        // 스페이스바를 누를 경우 WASD방향으로 구름
        // 구를때 마다 스태미너 감소
    }


    private void defense()
    {
        if (!Input.GetMouseButton(1)) 
        {
            mystate = PlayerState.idleNmove;
        }
        // 방패를 든 상태에서 공격을 받았다면 스태미너 감소
        // 방패를 든 상태에서는 스태미너가 0.5배 느리게 회복
        Recovery(5);
    }

    private void attack()
    {        
        // 공격을 할 경우 스태미너 감소
        StartCoroutine(beingIdle(playTime));
    }

    private void dash()
    {
        // dash상태에서는 지속적으로 스태미너 소모
        Consumption(10);
        if (!Input.GetKey(KeyCode.Space))
        {
            mystate = PlayerState.idleNmove;
        }
    }

    private void idleNmove()
    {
        if (stam < fullStamina)
        {
            Recovery(10);
        }
        // damaged가 체크되었다면 playerState를 damaged로 전환
        if (isDamaged) 
        {
            mystate = PlayerState.damaged;
        }

        
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("WalkSprintTree") == true)
        {
            float animTime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
            if (animTime > 0 && animTime < 1.0f) //애니메이션 플레이 중
            {
                mystate = PlayerState.move;

                if (Input.GetKey(KeyCode.Space))
                {
                    // WalkSprintTree 재생중 스페이스바를 누른 상태라면 playerState를 dash로 전환
                    print("달리는 중");
                    mystate = PlayerState.dash;
                }
            }
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("OneHand_Up_Attack_1") == true || anim.GetCurrentAnimatorStateInfo(0).IsName("OneHand_Up_Attack_2") == true) 
        {
            playTime = anim.GetCurrentAnimatorStateInfo(0).length;
            float animTime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
            if (animTime > 0 && animTime < 1.0f) 
            {
                mystate = PlayerState.attack;
            }
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Hit_F_1_InPlace") == true || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit_F_2_InPlace") == true) 
        {
            playTime = anim.GetCurrentAnimatorStateInfo(0).length;
            float animTime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
            if (animTime > 0 && animTime < 1.0f) 
            {
                mystate = PlayerState.damaged;
            }
        }

        if (Input.GetMouseButton(1)) 
        {
            mystate = PlayerState.defense;
        }

    }

    void Recovery(float wastage) 
    {
        if (stam != fullStamina)
        {
            stam += Time.deltaTime * wastage;
        }
    }

    void Consumption(float wastage) 
    {
        if (stam != 0)
        {
            stam -= Time.deltaTime * wastage;
        }
        else 
        {
            mystate = PlayerState.idleNmove;
        }
    }

    IEnumerator beingIdle(float sec) 
    {
        isBeingIdle = true;
        if (Input.GetMouseButtonDown(0))
        {
            stam -= 19;
        }
        yield return new WaitForSeconds(sec);
        mystate = PlayerState.idleNmove;
        isBeingIdle = false;
    }
}
