using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator attack;

    PlayerMove move;
    StatManager stat;

    float actionTime;
    int comboCount;

    public Collider swordCol;
    public BoxCollider shieldCol;
    void Start()
    { 
        
        comboCount = 0;
        attack = GetComponentInChildren<Animator>();
        move = GetComponent<PlayerMove>();
        stat = GetComponentInChildren<StatManager>();

    }

    void Update()
    {
        bool isShield = Input.GetMouseButton(1);
        attack.SetBool("Shield", isShield);
        //shield를 들어올렸으면, 쉴드 콜라이더를 켜주고, 쉴드를 내렸으면, 쉴드 콜라이더를 꺼주자
        shieldCol.enabled = isShield;

        // 마우스 왼클릭을 하면 공격을 한다.
        if (Input.GetMouseButtonDown(0))
        {
            // swordCol를 활성화한다.
            swordCol.enabled = true;
            // 연속해서 한번 더 클릭하면 콤보 모션이 재생된다.
            comboCount++;
            attack.SetTrigger("Sword");
            attack.SetInteger("Combo", comboCount);

            // 스태미너가 감소한다.
            stat.stam -= stat.useStam;
        }      
    }
}
