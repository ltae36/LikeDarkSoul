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
        stam = Mathf.Clamp(stam, 0, fullStamina);
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
        // ���� ���¿� ���� ���¹̳� �Ҹ𷮰� ������ �޶�����.
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
    }

    private void defense()
    {
        stam -= 19f;
        // ���и� �� ���¿����� ���¹̳ʰ� 0.5�� ������ ȸ��
        Recovery(5);
    }

    private void attack()
    {
    }

    private void dash()
    {
        // dash���¿����� ���������� ���¹̳� �Ҹ�
        Consumption(10);
        if (!Input.GetKey(KeyCode.Space))
        {
            mystate = PlayerState.idleNmove;
        }
    }

    private void idleNmove()
    {
        Recovery(10);
        // sprint�ִϸ��̼��� ������̶�� playerState�� dash�� ��ȯ
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("WalkSprintTree") == true)
        {
            float animTime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
            if (animTime > 0 && animTime < 1.0f) //�ִϸ��̼� �÷��� ��
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    print("�޸��� ��");
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