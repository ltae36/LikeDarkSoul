using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // WASD키 입력에 따라 플레이어가 이동한다.
    float moveSpeed;

    public float runSpeed = 8f;
    public float turnSpeed = 8f;
    public float sprintSpeed = 10f;
    public float walkSpeed = 10f;

    public Vector3 dir;
    Animator animator;
    CharacterController cc;

    void Start()
    {
        animator = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
        moveSpeed = runSpeed;
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        dir = new Vector3(h, 0, v);
        dir.Normalize();

        // WASD를 누르면 해당 방향으로 뛴다.
        animator.SetBool("Run", dir != Vector3.zero);
        // WASD를 누른 상태에서 스페이스바를 누르고 있으면 달린다.

        // WASD를 누른 상태에서 LeftAlt를 누르고 있으면 천천히 걷는다.
              
        
        

        // 이동
        cc.Move(dir * moveSpeed * Time.deltaTime);

        // 회전
        if (dir != Vector3.zero)
        {
            Quaternion rot = Quaternion.LookRotation(dir, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, turnSpeed * Time.deltaTime);
        }
    }
}
