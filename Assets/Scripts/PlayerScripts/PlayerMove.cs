using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    int secondHitAnimation;
    public float moveSpeed;

    public float runSpeed = 6f;
    public float turnSpeed = 8f;
    public float sprintSpeed = 8f;
    public float walkSpeed = 4f;

    Vector3 dir;

    public bool isSprint = false;
    public bool isWalking = false;
    public bool isrun;

    //float onSpace;


    public Vector3 camDir;
    public Camera cam;
    public StatManager stat;

    GameObject playerModel;
    Animator animator;
    CharacterController cc;
    PlayerAttack attack;
    HitCheck hit;

    void Start()
    {
        attack = GetComponent<PlayerAttack>();
        animator = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
        hit = GetComponent<HitCheck>();
        moveSpeed = runSpeed;
    }

    void Update()
    {
        if (stat.inAction) 
        {
            print(stat.inAction);

        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");


        // 카메라의 Transform을 가져온다.
        Transform cameraTransform = cam.transform;

        // 카메라의 로컬 로테이션y값을 추출한다.
        Quaternion cameraRotationY = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);

        // 액션이 작동 중일 때는 WASD이동이 작동하지 않는다.
        if (!stat.inAction)
        {
            moveSpeed = runSpeed;
        }
        else
        {
            moveSpeed = 0;
        }
        dir = new Vector3(h, 0, v);


        camDir = cameraRotationY * dir; // 카메라의 로컬 로테이션y 값을 dir에 적용한다.

        // CharacterController를 이용한 이동
        cc.Move(camDir * moveSpeed * Time.deltaTime);

        // 방향전환
        if (dir != Vector3.zero)
        {
            Quaternion rot = Quaternion.LookRotation(camDir, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, turnSpeed * Time.deltaTime);
        }


        #region 애니메이션 재생

        if (hit.isDamaged)
        {
            animator.SetTrigger("Hit");
            secondHitAnimation = 1;
            if (secondHitAnimation == 1)
            {
                animator.SetFloat("DamageCount", secondHitAnimation);
                secondHitAnimation = 0;
            }
        }


        // WASD를 누르면 해당 방향으로 달리는 애니메이션이 재생된다.
        animator.SetBool("Run", dir != Vector3.zero);

        // WASD를 누른 채로 LeftAlt를 누르면 속도가 느려지고, walk애니메이션이 재생된다.
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            isWalking = true;
            isrun = false;
        }
        else
        {
            isWalking = false;
            isrun = true;
        }

        // WASD를 누른 채로 스페이스바를 누르면 속도가 빨라지고, sprint애니메이션이 재생된다.
        //if (Input.GetKey(KeyCode.Space) && ( Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S) ))
        //{
        //    onSpace += Input.GetAxis("Jump");
        //    if (onSpace > 60)
        //    {
        //        isSprint = true;
        //        isrun = false;
        //    }
        //}

        if (Input.GetKey(KeyCode.Space))
        {
            isSprint = true;
            isrun = false;
        }
        else
        {
            isSprint = false;
            isrun = true;
            //if (onSpace > 0)
            //{
            //    onSpace -= Time.deltaTime * onSpace;
            //}
        }

        if (isSprint)
        {
            animator.SetFloat("MoveSpeed", Mathf.Clamp(moveSpeed, walkSpeed, sprintSpeed));
            moveSpeed += Time.deltaTime * 5f;

        }
        else if (isWalking)
        {
            animator.SetFloat("MoveSpeed", Mathf.Clamp(moveSpeed, walkSpeed, sprintSpeed));
            moveSpeed -= Time.deltaTime * 5f;
        }
        else
        {
            animator.SetFloat("MoveSpeed", runSpeed);
            moveSpeed = runSpeed;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (dir == Vector3.zero)
            {
                animator.SetTrigger("Jump_Back");
            }
        }

        if (stat.mystate == StatManager.PlayerState.move && Input.GetKeyUp(KeyCode.Space))
        {
            //HandleMovementInput();
            animator.SetTrigger("Roll 1");
        }



        #region 구르기 애니메이션
        //if (stat.mystate == StatManager.PlayerState.move)
        //{

        //    if (Input.GetKey(KeyCode.W))
        //    {
        //        animator.SetFloat("Roll", 0);
        //    }
        //    else if (Input.GetKey(KeyCode.A))
        //    {
        //        animator.SetFloat("Roll", 1);
        //    }
        //    else if (Input.GetKey(KeyCode.S))
        //    {
        //        animator.SetFloat("Roll", 3);
        //    }
        //    else if (Input.GetKey(KeyCode.D))
        //    {
        //        animator.SetFloat("Roll", 4);
        //    }

        //    if (Input.GetKeyUp(KeyCode.Space))
        //    {
        //        animator.SetTrigger("Roll 1");
        //    }
        //}


        //if (Input.GetKeyUp(KeyCode.Space))
        //{
        //    // 움직이지 않는 상태에서 스페이스바를 눌렀다 떼면 뒤로 점프한다.
        //    if (dir == Vector3.zero)
        //    {
        //        animator.SetTrigger("Jump_Back");
        //    }

        //    // 움직이는 상태에서 스페이스바를 누르면 해당 방향으로 구른다.
        //    else if (stat.mystate == StatManager.PlayerState.move)
        //    {
        //        animator.SetFloat("Roll", 3);
        //        animator.SetFloat("Roll", 0);
        //        animator.SetFloat("Roll", 1);
        //        animator.SetFloat("Roll", 4);

        //    }
        //}
        #endregion

        #endregion

    }

    //void HandleMovementInput() 
    //{
    //    // 기본적으로 Roll 값을 -1로 설정하여 입력이 없을 때의 상태를 정의합니다.
    //    int rollDirection = -1;

    //    if (Input.GetKey(KeyCode.W))
    //    {
    //        rollDirection = 0; // 앞방향
    //    }
    //    else if (Input.GetKey(KeyCode.A))
    //    {
    //        rollDirection = 1; // 왼쪽방향
    //    }
    //    else if (Input.GetKey(KeyCode.S))
    //    {
    //        rollDirection = 3; // 뒤쪽방향
    //    }
    //    else if (Input.GetKey(KeyCode.D))
    //    {
    //        rollDirection = 4; // 오른쪽방향
    //    }

    //    // Roll 애니메이션 값 설정
    //    animator.SetInteger("Roll", rollDirection);

    //    // 스페이스바를 눌렀다 떼는 경우 롤 애니메이션을 트리거
    //    if (rollDirection >= 0 && Input.GetKeyUp(KeyCode.Space))
    //    {
    //        animator.SetTrigger("Roll 1");
    //    }
    //}

}
