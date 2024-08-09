using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    float moveSpeed;
    public float stamina;

    public float runSpeed = 8f;
    public float turnSpeed = 8f;
    public float sprintSpeed = 10f;
    public float walkSpeed = 10f;
    public Vector3 dir;

    public bool isSprint = false;
    public bool isWalking = false;
    public bool isrun;

    float onSpace;
    Vector3 camDir;

    public Camera cam;
    GameObject playerModel;
    Animator animator;
    CharacterController cc;
    PlayerAttack attack;
    public StatManager stat;

    void Start()
    {
        attack = GetComponent<PlayerAttack>();
        animator = GetComponentInChildren<Animator>();
        cc = GetComponent<CharacterController>();        
        moveSpeed = runSpeed;
        stat = GetComponentInChildren<StatManager>();
    }

    void Update()
    {
        if (camDir != Vector3.zero)
        {
            print("이동 가능 상태");
        }
        else if (camDir == Vector3.zero)
        {
            print("공격, 방어 중");
        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // 카메라의 Transform을 가져온다.
        Transform cameraTransform = cam.transform;

        // 카메라의 로컬 로테이션y값을 추출한다.
        Quaternion cameraRotationY = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);

        dir = new Vector3(h, 0, v);
        camDir = cameraRotationY * dir; // 카메라의 로컬 로테이션y 값을 dir에 적용한다.

        //// 액션이 작동 중일 때는 WASD이동이 작동하지 않는다.
        if (stat.inAction)
        {
            camDir = Vector3.zero;
        }

        //dir = new Vector3(h, 0, v);
        //camDir = cameraRotationY * dir; // 카메라의 로컬 로테이션y 값을 dir에 적용한다.


        // CharacterController를 이용한 이동
        cc.Move(camDir * moveSpeed * Time.deltaTime);

        // 방향전환
        if (dir != Vector3.zero)
        {
            Quaternion rot = Quaternion.LookRotation(camDir, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, turnSpeed * Time.deltaTime);
        }

        
        #region 애니메이션 재생
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
        if (Input.GetKey(KeyCode.Space) && ( Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S) ))
        {
            onSpace += Input.GetAxis("Jump");
            if (onSpace > 60)
            {
                isSprint = true;
                isrun = false;
            }
        }
        else
        {
            isSprint = false;
            isrun = true;
            if (onSpace > 0)
            {
                onSpace -= Time.deltaTime * onSpace;
            }
        }

        if (isSprint)
        {
            animator.SetFloat("MoveSpeed", Mathf.Clamp(moveSpeed, walkSpeed, sprintSpeed));
            moveSpeed += Time.deltaTime * 1.5f;

        }
        else if (isWalking)
        {
            animator.SetFloat("MoveSpeed", Mathf.Clamp(moveSpeed, walkSpeed, sprintSpeed));
            moveSpeed -= Time.deltaTime * 1.5f;
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
            else if ( dir != Vector3.zero && (isWalking || isrun || !isSprint) && onSpace < 59)
            {
                animator.SetTrigger("Roll 1");
            }
        }
        #endregion
        
    }
}
