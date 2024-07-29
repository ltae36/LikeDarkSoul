using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Animator attack;
    PlayerMove move;

    float actionTime;

    public bool inAction;  

    void Start()
    {        
        inAction = false;
        attack = GetComponent<Animator>();
        move = GetComponent<PlayerMove>();
    }

    void Update()
    {
        // 왼클릭시 공격, 우클릭시 방어
        if (Input.GetMouseButtonDown(0))
        {
            // 공격 애니메이션이 재생되는 동안은 WASD를 눌러도 안움직임.
            //actionTime = attack.GetCurrentAnimatorClipInfo(0).Length;
            inAction = true;
            attack.SetTrigger("Sword");            
            
        }
        // 우클릭을 계속 누르고 있을 경우 계속해서 방어
        attack.SetBool("Shield", Input.GetMouseButton(1));
        if (inAction)
        {
            actionTime += Time.deltaTime;
            print(actionTime);
            if (actionTime > 2.19)
            {
                inAction = false;
                actionTime = 0;
            }
        }
        
    }
}
