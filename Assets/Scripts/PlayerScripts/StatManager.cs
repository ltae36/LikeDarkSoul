using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class StatManager : MonoBehaviour
{
    float fullHP = 454;
    float fullFP = 93;
    float fullStamina = 95;

    public float HP;
    public float FP;
    public float stam;

    public Slider hpSlider;
    public Slider fpSlider;
    public Slider stamSlider;

    public GameObject deadScene;

    public Animator anim;

    public enum PlayerState 
    {
        idleNmove,
        dash,
        attack,
        defense,
        roll,
        jump
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

    }

    void Update()
    {
        // 나의 상태에 따라서 스태미너 소모량과 패턴이 달라진다.
        switch (mystate) 
        {
            case PlayerState.idleNmove:
                idleNmove();
                break;
            case PlayerState.dash:
                dash();
                break;
            case PlayerState.attack:
                attack();
                break;
            case PlayerState.defense:
                defense();
                break;
            case PlayerState.roll:
                roll();
                break;
            case PlayerState.jump:
                jump();
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

    private void jump()
    {
    }

    private void roll()
    {
        // 구르기를 할 때 스태미너 감소
    }

    private void defense()
    {
        // 방패를 든 상태에서 공격을 받았다면 스태미너 감소
        // 방패를 든 상태에서는 스태미너가 0.5배 느리게 회복
        Recovery(5);
    }

    private void attack()
    {
        // 공격을 할 경우 스태미너 감소
        // 강공격일 경우에는 1.5배 더 감소
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
        // sprint애니메이션이 재생중이라면 playerState를 dash로 전환
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("WalkSprintTree") == true)
        {
            float animTime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
            if (animTime > 0 && animTime < 1.0f) //애니메이션 플레이 중
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    print("달리는 중");
                    mystate = PlayerState.dash;
                }
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
}
