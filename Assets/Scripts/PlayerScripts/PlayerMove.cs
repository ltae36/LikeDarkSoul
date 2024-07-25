using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // WASD키 입력에 따라 플레이어가 이동한다.
    public float moveSpeed = 8f;
    Vector3 dir;
    Animator animator;



    void Start()
    {
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        dir = new Vector3(h, 0, v);
        dir.Normalize();
        if (Input.GetKeyDown(KeyCode.W))
        {
            animator.SetTrigger("Walk_F");
        }
        transform.position += dir * moveSpeed * Time.deltaTime;

    }
}
