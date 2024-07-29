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
        // ��Ŭ���� ����, ��Ŭ���� ���
        if (Input.GetMouseButtonDown(0))
        {
            // ���� �ִϸ��̼��� ����Ǵ� ������ WASD�� ������ �ȿ�����.
            //actionTime = attack.GetCurrentAnimatorClipInfo(0).Length;
            inAction = true;
            attack.SetTrigger("Sword");            
            
        }
        // ��Ŭ���� ��� ������ ���� ��� ����ؼ� ���
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
