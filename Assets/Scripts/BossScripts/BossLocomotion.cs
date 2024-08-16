using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BossLocomotion : MonoBehaviour
{

    //�߰��ϰ� ���� ���
    //�ִϸ��̼��� root �������� �����ϰ� �ʹ�.
    //��, fsm�� �����϶���, �ȱ� �����, �ڱ� �ϴ�� root �������� �����ϱ� ���ؼ� back in pose�� �� �����س��� ����.
    //�������� �� handle movement�� handle rotation�� ������
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
    /// ����ȭ�� vector
    /// </summary>
    public Vector3 moveDirection;
    public Vector3 movePosition;
    public Vector3 jumpDirection;
    /// <summary>
    /// ����ȭ ���� ���� vector
    /// </summary>
    public Vector3 targetDirection;
    public Vector3 targetPosition;
    public bool isJump;
    public bool isDash;


    [HideInInspector]
    public Transform myTransform;

    [Header("attack target")]
    public GameObject target;

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


    // target�� ��ġ�� ���ϰ� �ʹ�
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

    //�������� ���⿡ ���߾ ���� ȸ�������� �ٲٰ� �ʹ�.
    public void HandleRotation()
    {
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        myTransform.rotation = Quaternion.Slerp(myTransform.rotation, targetRotation, status.rotationSpeed * Time.deltaTime);
    }

    //�����̴� �Լ� �����̱� ���� set target direction �� �� ���� ���� �����δ�.
    public void MoveBoss(MoveType moveType)
    {
        switch (moveType)
        {
            //���� ������
            case MoveType.Linear:
                cc.Move(moveDirection * status.moveSpeed * Time.deltaTime); 
                break;
            case MoveType.Jump:
                if (!isJump)
                {
                    return;
                }
                currentTime += Time.deltaTime;
                //�� �������� jump �ӵ��� ������ ���� �̿��ؼ� ���� �� �ִ�.
                jumpDirection = targetDirection.normalized * status.jump_x_velocity + Vector3.up * status.jump_y_velocity + Physics.gravity * currentTime;
                print(jumpDirection);
                if (jumpDirection.y < 0)
                {
                    Ray ray = new Ray(transform.position, Vector3.down);
                    RaycastHit hit;
                    drawRay = true;
                    //Layer 7�� ground�� 0.6f �� ��� ���� �� ���� ũ�⵵ ����ؾ� �ȴ�!
                    if (Physics.Raycast(ray, out hit, raySize))
                    {
                        print(hit.collider.name);
                        if (hit.collider.CompareTag("Ground"))
                        {
                            movePosition.y = 0;
                            transform.position = movePosition;
                            //jump�� �����Ѵ�
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

                //���� targetPosition�� �Ÿ� ���̰� 1f�̸��̶��
                if(Vector3.Distance(transform.position, movePosition) < status.attackDistance)
                {
                    
                    //targetPosition.y = 0;
                    //transform.position = targetPosition;
                    isDash = false;
                }
                break;
        }
    }

    //private void SetJumpDirection()
    //{
    //    movePosition = targetPosition;
    //    moveDirection = targetDirection;
    //    moveDirection.y = 0;
    //    //v0�� ���Ѵ�.
    //    float jumpVelocity = Mathf.Sqrt(moveDirection.magnitude * Physics.gravity.magnitude / 2);
    //    status.SetJumpSpeed(jumpVelocity);
    //    print("jump Velocity : "+ jumpVelocity);
    //    print("jump speed: "+ status.jumpSpeed);
    //    currentTime = 0;
    //}

    private void SetJumpDirection()
    {
        //x�� jump �ӵ��� ���ϰ� �ʹ�.
        movePosition = targetPosition;
        moveDirection = targetDirection;

        float distance = moveDirection.magnitude;
        //s = vt, v = s/t
        status.jump_x_velocity = distance / status.jumpTime;

        //y�� jump �ʱ� �ӵ��� ���ϰ� �ʹ�.
        //v = gt/2
        status.jump_y_velocity = Physics.gravity.magnitude * status.jumpTime;

        //Ÿ�̸Ӹ� �����Ѵ�.
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
