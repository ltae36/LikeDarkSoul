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
    /// ����ȭ�� vector
    /// </summary>
    public Vector3 moveDirection;
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

    //������ �����̶�, ���� �� ���� ���� ���� ���� ������ ������ ���� ���� �־ �߰���.
    public void SetIdleDirection()
    {
        //0�̸� ��, 1�̸� ����, 2�̸� ���������� ������
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
                isDash = true;
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

                if (jumpDirection.y < 0)
                {
                    Vector3 currentJumpPosition = transform.position + jumpDirection * Time.deltaTime;

                    //���� �浹�ϴ��� �˻��ؾ� �Ѵ�.
                    //���� 0.1f �Ʒ��� ray�� ���� �� ���� �ε�ģ�ٸ�...
                    print(currentJumpPosition);
                    Ray ray = new Ray(currentJumpPosition, Vector3.down);
                    RaycastHit hit;
                    //Layer 7�� ground�� 0.6f �� ��� ���� �� ���� ũ�⵵ ����ؾ� �ȴ�!
                    if (Physics.Raycast(ray, out hit, raySize))
                    {
                        if (hit.collider.name.Equals("Ground"))
                        {
                            //���� ��ġ�� ���� �޶���� targetPosition���� �ٲ۴�. 
                            print(targetPosition);
                            targetPosition.y = 0;
                            transform.position = targetPosition;
                            
                            //jump�� �����Ѵ�
                            isJump = false;
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

                //���� targetPosition�� �Ÿ� ���̰� 0.1f�̸��̶��
                if(Vector3.Distance(transform.position, targetPosition) < 1f)
                {
                    //���� ��ġ�� target Position���� ��´�.
                    targetPosition.y = 0;
                    transform.position = targetPosition;
                    isDash = false;
                }
                break;
        }
    }

    private void SetJumpDirection()
    {
        //v0�� ���Ѵ�.
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
