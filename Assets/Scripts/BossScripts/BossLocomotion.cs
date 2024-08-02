using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLocomotion : MonoBehaviour
{
    enum MoveType
    {
        Linear = 0,
        Circle = 1
    }
    public float vertical;
    public float horizontal;
    public float targetDistance;
    public Vector3 moveDirection;
    public Vector3 targetDirection;
    public Vector3 targetPosition;
    MoveType moveType;

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


    // target의 위치를 정하고 싶다
    void SetTargetPosition()
    {
        targetPosition = target.transform.position; 
    }
    //target direction
    void SetTargetDirection()
    {
        targetDirection = targetPosition - transform.position;
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
    public void HandleRotation()
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

    //현재 target 

    //직선 이동을 한다.
    public void LinearMovement(float speed)
    {
        //target의 위치, 방향, 거리를 계산한다.
        SetTargetPosition();
        //나는 target으로 향하는 방향으로 이동하고 싶다.
        moveDirection = targetDirection;
        cc.Move(moveDirection * speed * Time.deltaTime);
    }
    //곡선 이동을 한다.
    /// <summary>
    /// 곡선이동을 하기 위한 함수, 호출하기 전에 SetTargetDirection을 호출해야 하고, 호출 중에는 target direction을 바꿔서는 안 됨.
    /// </summary>
    /// <param name="speed"></param>
    public void CircleMovement(float speed)
    {
        //target의 위치는 호출되기 전에 한 번만 계산한다.
        SetTargetDirection();
        moveDirection = new Vector3(targetDirection.z, 0, targetDirection.x);
        cc.Move(moveDirection * speed * Time.deltaTime);
    }
}
