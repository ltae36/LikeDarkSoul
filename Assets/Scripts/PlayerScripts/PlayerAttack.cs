using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator attack;
    PlayerMove move;
    StatManager hp;

    float actionTime;
    int comboCount;


    public Collider swordCol;

    void Start()
    { 
        
        comboCount = 0;
        attack = GetComponentInChildren<Animator>();
        move = GetComponent<PlayerMove>();
        hp = GetComponent<StatManager>();
    }

    void Update()
    {

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


        //if (inAction)
        //{
        //    actionTime += Time.deltaTime;
        //    if (actionTime > 1.5)
        //    {
        //        inAction = false;
        //        actionTime = 0;
        //        comboCount = 0;
        //        swordCol.enabled=false;
        //    }
        //}        
    }
}
