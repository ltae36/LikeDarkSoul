using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PlayerEvasion : MonoBehaviour
{
    // WASD를 눌러 이동중인 상태에서 스페이스바를 누르면 이동중인 방향으로 굴러 회피한다.
    
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


        //// 카메라의 Transform을 가져온다.
        //Transform cameraTransform = cam.transform;

        //// 카메라의 로컬 로테이션y값을 추출한다.
        //Quaternion cameraRotationY = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);

        //dir = cameraRotationY * rollDir;

        // 걷기 애니메이션이 재생중이라면
        //if (anim.GetCurrentAnimatorStateInfo(0).IsName("WalkSprintTree") == true) 
        //{
        //    float animTime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
        //    if (animTime > 0 && animTime < 1.0f) // 애니메이션 재생 중
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
