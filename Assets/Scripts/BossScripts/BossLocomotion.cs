using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLocomotion : MonoBehaviour
{
    public enum MoveType
    {
        Linear = 0,
        Circle = 1,
        Jump = 2,
        Dash = 4
    }
    public float vertical;
    public float horizontal;
    public float targetDistance;
    /// <summary>
    /// 정규화된 vector
    /// </summary>
    public Vector3 moveDirection;
    public Vector3 jumpDirection;
    /// <summary>
    /// 정규화 되지 않은 vector
    /// </summary>
    public Vector3 targetDirection;
    public Vector3 targetPosition;
    public bool isJump;
    public bool isDash;


    [HideInInspector]
    public Transform myTransform;

    [Header("attack target")]
    public GameObject target;

    [Header("status")]
    [SerializeField] float moveSpeed;
    [SerializeField] float dashSpeed;
    [SerializeField] float rotationSpeed;
    float currentTime;

    [Header("RaySize")]
    [SerializeField] float raySize = 0.6f;

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
    public void SetTargetPosition()
    {
        targetPosition = target.transform.position; 
    }
    //target direction
    public void SetTargetDirection()
    {
        targetDirection = targetPosition - transform.position;
        targetDirection.y = 0;
        targetDistance = targetDirection.magnitude;
//        targetDirection.Normalize();
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

    public void SetMoveDirection(MoveType moveType)
    {
        SetTargetPosition();
        SetTargetDirection();

        switch (moveType) {
            case MoveType.Linear:
                moveDirection = targetDirection.normalized;
                vertical = moveDirection.x;
                horizontal = moveDirection.z;
                break;
            case MoveType.Dash:
                moveDirection = targetDirection.normalized;
                vertical = moveDirection.x;
                horizontal = moveDirection.z;
                break;
            case MoveType.Jump:
                isJump = true;
                SetJumpDirection();
                break;
        }
    }

    //내움직임 방향에 맞추어서 나의 회전방향을 바꾸고 싶다.
    public void HandleRotation()
    {
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        myTransform.rotation = Quaternion.Slerp(myTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    //움직이는 함수 움직이기 전에 set target direction 한 번 해준 다음 움직인다.
    public void MoveBoss(MoveType moveType)
    {
        switch (moveType)
        {
            //직선 움직임
            case MoveType.Linear:
                cc.Move(moveDirection * BossStatus.moveSpeed * Time.deltaTime); 
                break;
            case MoveType.Jump:
                if (!isJump)
                {
                    return;
                }
                currentTime += Time.deltaTime;
                //각 순간마다 jump 속도는 다음의 식을 이용해서 구할 수 있다.
                jumpDirection = targetDirection.normalized * BossStatus.jumpSpeed + Vector3.up * BossStatus.jumpSpeed + Physics.gravity * Mathf.Pow(currentTime, 2.0f) / 2;
                
                Vector3 currentJumpPosition = transform.position + jumpDirection * Time.deltaTime;

                //땅에 충돌하는지 검사해야 한다.
                //만약 0.1f 아래로 ray를 쐈을 때 땅에 부딪친다면...
                print(currentJumpPosition);
                Ray ray = new Ray(currentJumpPosition, Vector3.down);
                RaycastHit hit;
                //Layer 7이 ground임 0.6f 로 쏘는 이유 내 현재 크기도 고려해야 된다!
                if (Physics.Raycast(ray, out hit, raySize))
                {
                    if (hit.collider.name.Equals("Ground"))
                    {
                        //현재 위치를 땅에 달라붙은 targetPosition으로 바꾼다. 
                        transform.position = targetPosition;
                        //jump를 종료한다
                        isJump = false;
                        return;
                    }
                }
                cc.Move(jumpDirection * Time.deltaTime);
                break;
            case MoveType.Dash:
                if (!isDash)
                {
                    return;
                }
                cc.Move(moveDirection * BossStatus.dashSpeed * Time.deltaTime);

                //만약 targetPosition과 거리 차이가 0.1f미만이라면
                if(Vector3.Distance(transform.position, targetPosition) < 0.1f)
                {
                    //현재 위치를 target Position으로 잡는다.
                    transform.position = targetPosition;
                    isDash = false;
                }
                break;
        }
    }

    private void SetJumpDirection()
    {
        //v0을 구한다.
        float jumpVelocity = Mathf.Sqrt(targetDirection.magnitude * Physics.gravity.magnitude / 2);
        BossStatus.SetJumpSpeed(jumpVelocity);
        currentTime = 0;
    }

        public void DashBoss()
    {
        cc.Move(moveDirection * dashSpeed * Time.deltaTime);
    }

    public bool IsJumping()
    {
        return isJump;
    }

    public bool IsDashing()
    {
        return isDash;
    }
}
