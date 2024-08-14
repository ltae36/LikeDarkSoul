using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BossLocomotion : MonoBehaviour
{

    //추가하고 싶은 기능
    //애니메이션의 root 움직임을 적용하고 싶다.
    //단, fsm이 공격일때만, 걷기 깨어나기, 자기 일대는 root 움직임을 무시하기 위해서 back in pose를 다 세팅해놓을 것임.
    //공격중일 때 handle movement랑 handle rotation을 꺼놓자
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
    public Vector3 movePosition;
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

    //public static BossLocomotion instance;

    CharacterController cc;
    BossStatus status;
    bool drawRay = false;

    void Start()
    {
        myTransform = transform;
        target = GameObject.FindGameObjectWithTag("Player");
        if (target == null)
            Destroy(this);

        cc = GetComponent<CharacterController>();
        status = GetComponent<BossStatus>();
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

    public void SetMoveDirection(MoveType moveType)
    {
        SetTargetPosition();
        SetTargetDirection();

        switch (moveType) {
            case MoveType.Linear:
                moveDirection = targetDirection.normalized;
                moveDirection.y = 0;
                movePosition = targetPosition;
                vertical = moveDirection.x;
                horizontal = moveDirection.z;
                break;
            case MoveType.Dash:
                isDash = true;
                movePosition = targetPosition;
                moveDirection = targetDirection.normalized;
                vertical = moveDirection.x;
                horizontal = moveDirection.z;
                break;
            case MoveType.Jump:
                isJump = true;
                SetJumpDirection();
                break;
            case MoveType.Circle:
                moveDirection = targetDirection.normalized;
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
                cc.Move(moveDirection * status.moveSpeed * Time.deltaTime); 
                break;
            case MoveType.Jump:
                if (!isJump)
                {
                    return;
                }
                currentTime += Time.deltaTime;
                //각 순간마다 jump 속도는 다음의 식을 이용해서 구할 수 있다.
                jumpDirection = targetDirection.normalized * status.jumpSpeed + Vector3.up * status.jumpSpeed + Physics.gravity * currentTime;
                if (jumpDirection.y < 0)
                {
                    Ray ray = new Ray(transform.position, Vector3.down);
                    RaycastHit hit;
                    drawRay = true;
                    //Layer 7이 ground임 0.6f 로 쏘는 이유 내 현재 크기도 고려해야 된다!
                    if (Physics.Raycast(ray, out hit, raySize))
                    {
                        print(hit.collider.name);
                        if (hit.collider.CompareTag("Ground"))
                        {
                            movePosition.y = 0;
                            transform.position = movePosition;
                            //jump를 종료한다
                            isJump = false;
                            currentTime = 0;
                            drawRay = false;
                            return;
                        }
                    }
                }
                cc.Move(jumpDirection * Time.deltaTime);
                break;
            case MoveType.Dash:
                if (!isDash)
                {
                    return;
                }
                cc.Move(moveDirection * status.dashSpeed * Time.deltaTime);

                //만약 targetPosition과 거리 차이가 1f미만이라면
                if(Vector3.Distance(transform.position, movePosition) < 1f)
                {
                    
                    //targetPosition.y = 0;
                    //transform.position = targetPosition;
                    isDash = false;
                }
                break;
        }
    }

    private void SetJumpDirection()
    {
        movePosition = targetPosition;
        moveDirection = targetDirection;
        moveDirection.y = 0;
        //v0을 구한다.
        float jumpVelocity = Mathf.Sqrt(moveDirection.magnitude * Physics.gravity.magnitude / 2);
        status.SetJumpSpeed(jumpVelocity);
        print("jump Velocity : "+ jumpVelocity);
        print("jump speed: "+ status.jumpSpeed);
        currentTime = 0;
    }

    public bool IsJumping()
    {
        return isJump;
    }

    public bool IsDashing()
    {
        return isDash;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(movePosition, new Vector3(1, 1, 1));

        if (drawRay)
            Gizmos.DrawLine(transform.position, transform.position - new Vector3(0, raySize, 0));


        Gizmos.DrawIcon(transform.position, "boss.png");

    }


}
