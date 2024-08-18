using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PlayerEvasion : MonoBehaviour
{
    // WASD�� ���� �̵����� ���¿��� �����̽��ٸ� ������ �̵����� �������� ���� ȸ���Ѵ�.
    
    Animator anim;
    PlayerMove move;
    CharacterController cc;

    float rollSpeed;
    float sliding;

    Vector3 rollDir;
    Vector3 dir;

    public Camera cam;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        move = GetComponentInChildren<PlayerMove>();
        cc = GetComponentInChildren<CharacterController>();
        sliding = Mathf.Lerp(0, 100, rollSpeed);

    }

    void Update()
    {


        //// ī�޶��� Transform�� �����´�.
        //Transform cameraTransform = cam.transform;

        //// ī�޶��� ���� �����̼�y���� �����Ѵ�.
        //Quaternion cameraRotationY = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);

        //dir = cameraRotationY * rollDir;

        // �ȱ� �ִϸ��̼��� ������̶��
        //if (anim.GetCurrentAnimatorStateInfo(0).IsName("WalkSprintTree") == true) 
        //{
        //    float animTime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
        //    if (animTime > 0 && animTime < 1.0f) // �ִϸ��̼� ��� ��
        //    {
        //        if (Input.GetKeyDown(KeyCode.Space)) 
        //        {
        //            anim.SetTrigger("Roll 1");
        //            move.enabled = false;
        //            cc.Move(rollDir * rollSpeed * Time.deltaTime);
        //        }
        //    }
        //}


        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.applyRootMotion = true;

            anim.SetTrigger("Roll 1");
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("RollBackward"))
            {
                rollSpeed = anim.GetCurrentAnimatorStateInfo(0).length;
            }
            rollDir = new Vector3(sliding, 0, 0);
            move.enabled = false;
            cc.Move(rollDir);
        }
        else 
        {
            move.enabled = true;
        }

    }
}
