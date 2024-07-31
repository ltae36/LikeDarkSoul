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
        // �� �����Ӹ��� ������ ������ ���ϰ� �ʹ�.
        SetTargetDirection();

        // ������ ���⿡ ���߾ ���� ȸ���� �ٲٰ� �ʹ�.
        HandleRotation();

    }

    // target�� ��ġ�� ���ϰ� �ʹ�
    void SetTargetDirection()
    {
        targetDirection = target.transform.position - transform.position;
        targetDirection.y = 0;
        targetDistance = targetDirection.magnitude;
        targetDirection.Normalize();
        
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
    
    public void SetMoveDirection()
    {
        moveDirection = targetDirection;
        vertical = moveDirection.x;
        horizontal = moveDirection.z;
    }

    //�������� ���⿡ ���߾ ���� ȸ�������� �ٲٰ� �ʹ�.
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
