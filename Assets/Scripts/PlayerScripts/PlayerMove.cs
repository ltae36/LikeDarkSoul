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
    
    Animator animator;
    CharacterController cc;
    PlayerAttack attack;

    void Start()
    {
        attack = GetComponent<PlayerAttack>();
        animator = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
        moveSpeed = runSpeed;

    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (!attack.inAction)
        {
            dir = new Vector3(h, 0, v);
            dir.Normalize();
        }
        else
        {
            dir = Vector3.zero;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isSprint)
        {
            animator.SetTrigger("Jump_Back");
        }

        // WASD를 누르면 해당 방향으로 달리는 애니메이션이 재생된다.
        animator.SetBool("Run", dir != Vector3.zero);        

        // WASD를 누른 채로 LeftAlt를 누르면 속도가 느려지고, walk애니메이션이 재생된다.
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }        

        // WASD를 누른 채로 스페이스바를 누르면 속도가 빨라지고, sprint애니메이션이 재생된다.
        if (Input.GetKey(KeyCode.Space))
        {
            isSprint = true;
            //stamina -= Time.deltaTime;
        }
        else
        {
            isSprint = false;
            //stamina += Time.deltaTime;
        }

        if (isSprint)
        {
            animator.SetFloat("MoveSpeed", Mathf.Clamp(moveSpeed, walkSpeed, sprintSpeed));
            moveSpeed += Time.deltaTime;
            //while (moveSpeed > sprintSpeed)
            //{
            //    moveSpeed += Time.deltaTime;
            //}
            //if (stamina < 0)
            //{
            //    animator.SetFloat("movespeed", runSpeed);
            //    moveSpeed = runSpeed;
            //}

        }
        else if (isWalking)
        {
            animator.SetFloat("MoveSpeed", Mathf.Clamp(moveSpeed, walkSpeed, sprintSpeed));
            moveSpeed -= Time.deltaTime;
        }
        else
        {
            animator.SetFloat("MoveSpeed", runSpeed);
            moveSpeed = runSpeed;
        }

        cc.Move(dir * moveSpeed * Time.deltaTime); 
     
        if (dir != Vector3.zero)
        {
            Quaternion rot = Quaternion.LookRotation(dir, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, turnSpeed * Time.deltaTime);
        }
    }
}
