using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Apple.ReplayKit;

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
        myTransform.rotation = Quaternion.Slerp(myTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    //�����̴� �Լ� �����̱� ���� set target direction �� �� ���� ���� �����δ�.
    public void MoveBoss(MoveType moveType)
    {
        switch (moveType)
        {
            //���� ������
            case MoveType.Linear:
                cc.Move(moveDirection * BossStatus.moveSpeed * Time.deltaTime); 
                break;
            case MoveType.Jump:
                if (!isJump)
                {
                    return;
                }
                currentTime += Time.deltaTime;
                //�� �������� jump �ӵ��� ������ ���� �̿��ؼ� ���� �� �ִ�.
                jumpDirection = targetDirection.normalized * BossStatus.jumpSpeed + Vector3.up * BossStatus.jumpSpeed + Physics.gravity * Mathf.Pow(currentTime, 2.0f) / 2;
                print("��ġ:" +transform.position);
                print("���� ����: "+jumpDirection);
                if (jumpDirection.y < 0)
                {
                    Ray ray = new Ray(transform.position, Vector3.down);
                    RaycastHit hit;
                    //Layer 7�� ground�� 0.6f �� ��� ���� �� ���� ũ�⵵ ����ؾ� �ȴ�!
                    if (Physics.Raycast(ray, out hit, raySize))
                    {
                        print(hit.collider.name);
                        if (hit.collider.name.Equals("Ground"))
                        {
                            movePosition.y = 0;
                            transform.position = movePosition;

                            //jump�� �����Ѵ�
                            isJump = false;
                            GetComponent<Animator>().applyRootMotion = true;
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
                cc.Move(moveDirection * BossStatus.dashSpeed * Time.deltaTime);

                //���� targetPosition�� �Ÿ� ���̰� 1f�̸��̶��
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
        //v0�� ���Ѵ�.
        float jumpVelocity = Mathf.Sqrt(targetDirection.magnitude * Physics.gravity.magnitude / 2);
        BossStatus.SetJumpSpeed(jumpVelocity);
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
}
