using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLocomotion : MonoBehaviour
{
    public float vertical;
    public float horizontal;
    public float targetDistance;
    public Vector3 moveDirection;
    public Vector3 targetDirection;

    [HideInInspector]
    public Transform myTransform;

    [Header("attack target")]
    public GameObject target;

    [Header("status")]
    [SerializeField] float moveSpeed;
    [SerializeField] float dashSpeed;
    [SerializeField] float rotationSpeed;

    public static BossLocomotion instance;

    CharacterController cc;

    private void Awake()
    {
        if (instance == null) {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        myTransform = transform;
        target = GameObject.Find("Player");
        if (target == null)
            Destroy(this);

        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        // 매 프레임마다 움직임 방향을 구하고 싶다.
        SetTargetDirection();

        // 움직임 방향에 맞추어서 나의 회전을 바꾸고 싶다.
        HandleRotation();

    }

    // target의 위치를 정하고 싶다
    void SetTargetDirection()
    {
        targetDirection = target.transform.position - transform.position;
        targetDirection.y = 0;
        targetDistance = targetDirection.magnitude;
        targetDirection.Normalize();
        
    }

    //움직임 방향이란, 걸을 때 앞을 보고 걸을 수도 있지만 옆으로 걸을 수도 있어서 추가함.
    public void SetIdleDirection()
    {
        //0이면 앞, 1이면 왼쪽, 2이면 오른쪽으로 가볼까
        int randomDirection = Random.Range((int)0, (int)3);
        Vector3 direction = Vector3.forward;
        switch (randomDirection)
        {
            case 0:
                direction = Vector3.forward;
                break;
            case 1:
                direction = Vector3.left;
                break;
            case 2:
                direction = Vector3.right;
                break;
        }

        moveDirection = transform.TransformDirection(direction).normalized;
        vertical = moveDirection.x;
        horizontal = moveDirection.z;
    }
    
    public void SetMoveDirection()
    {
        moveDirection = targetDirection;
        vertical = moveDirection.x;
        horizontal = moveDirection.z;
    }

    //내움직임 방향에 맞추어서 나의 회전방향을 바꾸고 싶다.
    void HandleRotation()
    {
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        myTransform.rotation = Quaternion.Slerp(myTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void MoveBoss()
    {
        
        cc.Move(moveDirection * moveSpeed *  Time.deltaTime);
    }

    public void DashBoss()
    {
        cc.Move(moveDirection * dashSpeed * Time.deltaTime);
    }
}
