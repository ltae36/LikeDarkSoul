using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class StatManager : MonoBehaviour
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
    public HitCheck check;

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
        // ���� ���¿� ���� ���¹̳� �Ҹ𷮰� ������ �޶�����.
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
            case PlayerState.move:
                inAction = false;
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
        if (check.isDamaged && HP != 0)
        {
            print(playTime);
            HP -= 90;
            BeingIdle(playTime);
        }
        else if (HP <= 0) 
        {
            mystate = PlayerState.dead;
        }

    }

    private void move()
    {
        // �����̽��ٸ� ���� ��� WASD�������� ����
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // ������ ���� ���¹̳� ����
            stam -= 19;
        }
        // WalkSprintTree ����� �����̽��ٸ� ���� ���¶�� playerState�� dash�� ��ȯ
        else if (Input.GetKey(KeyCode.Space))
        {
            print("�޸��� ��");
            mystate = PlayerState.dash;
        }
        else
        {
            mystate = PlayerState.move;
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("WalkSprintTree") == true)
        {
            // WalkSprintTree�� ����� �����ٸ� idle���°� ��
            float animTime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
            if (animTime == 0) //�ִϸ��̼� �÷��� ��
            {
                mystate = PlayerState.idleNmove;
            }
        }
        // ���� �ִϸ��̼��� ����ȴٸ� attack���°� ��.
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("OneHand_Up_Attack_1") == true || anim.GetCurrentAnimatorStateInfo(0).IsName("OneHand_Up_Attack_2") == true)
        {
            playTime = anim.GetCurrentAnimatorStateInfo(0).length;
            float animTime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
            if (animTime > 0 && animTime < 1.0f)
            {
                mystate = PlayerState.attack;
            }
        }
        // �ǰ� �ִϸ��̼��� ����ȴٸ� damaged���°� ��.
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Hit_F_1_InPlace") == true || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit_F_2_InPlace") == true)
        {
            playTime = anim.GetCurrentAnimatorStateInfo(0).length;
            float animTime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
            if (animTime > 0 && animTime < 1.0f)
            {
                mystate = PlayerState.damaged;
            }
        }
        // ��� �ִϸ��̼��� ����ȴٸ� defense���°� ��.
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("OneHand_Up_Shield_Block_Hit_1") == true || anim.GetCurrentAnimatorStateInfo(0).IsName("OneHand_Up_Shield_Block_Idle") == true)
        {
            playTime = anim.GetCurrentAnimatorStateInfo(0).length;
            float animTime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
            if (animTime > 0 && animTime < 1.0f)
            {
                mystate = PlayerState.defense;
            }
        }

        Recovery(10);
    }


    private void defense()
    {
        if (!Input.GetMouseButton(1)) 
        {
            mystate = PlayerState.idleNmove;
        }
        // ���и� �� ���¿��� ������ �޾Ҵٸ� ���¹̳� ����
        // ���и� �� ���¿����� ���¹̳ʰ� 0.5�� ������ ȸ��
        Recovery(5);
    }

    private void attack()
    {        
        // ������ �� ��� ���¹̳� ����
        StartCoroutine(BeingIdle(playTime));
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
        if (!inAction) 
        {
            inAction = true;
        }

        if (stam < fullStamina)
        {
            Recovery(10);
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("WalkSprintTree") == true)
        {
            // WalkSprintTree�� ������̶�� move���·� ��ȯ
            float animTime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
            if (animTime > 0 && animTime < 1.0f) //�ִϸ��̼� �÷��� ��
            {
                mystate = PlayerState.move;
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
        else if (check.isDamaged && anim.GetCurrentAnimatorStateInfo(0).IsName("Hit_F_1_InPlace") == true || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit_F_2_InPlace") == true)
        {
            playTime = anim.GetCurrentAnimatorStateInfo(0).length;
            float animTime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
            if (animTime > 0 && animTime < 1.0f)
            {
                mystate = PlayerState.damaged;
            }
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("OneHand_Up_Shield_Block_Hit_1") == true || anim.GetCurrentAnimatorStateInfo(0).IsName("OneHand_Up_Shield_Block_Idle") == true)
        {
            playTime = anim.GetCurrentAnimatorStateInfo(0).length;
            float animTime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
            if (animTime > 0 && animTime < 1.0f)
            {
                mystate = PlayerState.defense;
            }
        }


        //if (Input.GetMouseButton(1)) 
        //{
        //    mystate = PlayerState.defense;
        //}

    }

    void Recovery(float wastage) 
    {
        if (stam < fullStamina)
        {
            stam += Time.deltaTime * wastage;
        }
        stam = Mathf.Clamp(stam, 0, fullStamina);
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
        stam = Mathf.Clamp(stam, 0, fullStamina);
    }

    IEnumerator BeingIdle(float sec) 
    {
        isBeingIdle = true;

        float elapsedTime = 0f;

        while (elapsedTime < sec)
        {
            if (Input.GetMouseButtonDown(0))
            {
                stam -= 19;
            }

            elapsedTime += Time.deltaTime;
            yield return null;  // �� ������ ���
        }

        mystate = PlayerState.idleNmove;
        isBeingIdle = false;
    }
}
