using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // WASDŰ �Է¿� ���� �÷��̾ �̵��Ѵ�.
    float moveSpeed;

    public float runSpeed = 8f;
    public float turnSpeed = 8f;
    public float sprintSpeed = 10f;
    public float walkSpeed = 10f;

    bool isSprint = false;
    bool isWalking = false;

    public Vector3 dir;
    Animator animator;
    CharacterController cc;
    StatManager statManager;


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

        //float stam = statManager.stamina;

        // WASD�� ������ �ش� �������� �ڴ�.
        animator.SetBool("Run", dir != Vector3.zero);
        // WASD�� ���� ���¿��� �����̽��ٸ� ������ ������ �޸���.
        if (Input.GetKey(KeyCode.Space)) 
        {
            isSprint = true;            
        }
        else 
        {
            isSprint = false; 
        }
        if (isSprint)
        {
            animator.SetFloat("MoveSpeed", sprintSpeed);
            moveSpeed = sprintSpeed;
        }
        else
        {
            animator.SetFloat("MoveSpeed", runSpeed);
            moveSpeed = runSpeed;
        }

        if (Input.GetKey(KeyCode.LeftAlt))
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
        if (isWalking)
        {
            animator.SetFloat("MoveSpeed", walkSpeed);
            moveSpeed = walkSpeed;
        }
        else
        {
            animator.SetFloat("MoveSpeed", runSpeed);
            moveSpeed = runSpeed;
        }

        // WASD�� ���� ���¿��� LeftAlt�� ������ ������ õõ�� �ȴ´�.
        // �̵�
        cc.Move(dir * moveSpeed * Time.deltaTime);

        // ȸ��
        if (dir != Vector3.zero)
        {
            Quaternion rot = Quaternion.LookRotation(dir, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, turnSpeed * Time.deltaTime);
        }
    }
}
