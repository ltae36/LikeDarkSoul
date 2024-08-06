using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Animator attack;
    PlayerMove move;
    StatManager hp;

    float actionTime;
    int comboCount;

    public bool inAction;
    public Collider swordCol;

    void Start()
    {        
        inAction = false;
        comboCount = 0;
        attack = GetComponent<Animator>();
        move = GetComponent<PlayerMove>();
        hp = GetComponent<StatManager>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) || Input.GetMouseButton(1)) 
        {
            inAction = true;
        }

        attack.SetBool("Shield", Input.GetMouseButton(1));

        // ��Ŭ���� ����, ��Ŭ���� ���
        if (Input.GetMouseButtonDown(0))
        {
            // swordCol를 활성화한다.
            swordCol.enabled = true;
            // ���� �ִϸ��̼��� ����Ǵ� ������ WASD�� ������ �ȿ�����.
            //actionTime = attack.GetCurrentAnimatorClipInfo(0).Length;
            comboCount++;
            attack.SetTrigger("Sword");
            attack.SetInteger("Combo", comboCount);
            
        }


        if (inAction)
        {
            actionTime += Time.deltaTime;
            if (actionTime > 1.5)
            {
                inAction = false;
                actionTime = 0;
                comboCount = 0;
                swordCol.enabled=false;
            }
        }        
    }
}
