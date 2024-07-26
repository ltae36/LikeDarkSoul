using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLocomotion : MonoBehaviour
{
    // Start is called before the first frame update
    public float vertical;
    public float horizontal;
    public Vector3 moveDirection;

    [HideInInspector]
    public Transform myTransform;

    [Header("attack target")]
    [SerializeField] GameObject target;
    [Header("status")]
    [SerializeField] float moveSpeed;
    [SerializeField] float dashSpeed;
    [SerializeField] float rotationSpeed;



    void Start()
    {
        myTransform = transform;
        target = GameObject.Find("Player");
        if (target == null)
            Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        // 매 프레임마다 움직임 방향을 구하고 싶다.
        SetMoveDirection();

        // 움직임 방향에 맞추어서 나의 회전을 바꾸고 싶다.
        HandleRotation();

        // 움직임 방향으로 이동하고 싶다.
        //HandleMovement();
    }

    // target의 위치에 맞추어서 내 움직임 방향을 구하고 싶다.
    void SetMoveDirection()
    {
        moveDirection = target.transform.position - transform.position;
        moveDirection.y = 0;
        moveDirection.Normalize();

        vertical = moveDirection.x;
        horizontal = moveDirection.z;
    }

    //내움직임 방향에 맞추어서 나의 회전방향을 바꾸고 싶다.
    void HandleRotation()
    {
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        myTransform.rotation = Quaternion.Slerp(myTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void HandleMovement()
    {
        //이동방향으로 이동하고 싶다.
        //p=p0 + vt

        transform.position += moveSpeed * moveDirection * Time.deltaTime;

    }
}
