using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLocomotion : MonoBehaviour
{
    // Start is called before the first frame update
    public float vertical;
    public float horizontal;
    public Vector3 moveDirection;
    public Vector3 targetDirection;
    public float targetDistance;

    [HideInInspector]
    public Transform myTransform;

    [Header("attack target")]
    [SerializeField] GameObject target;
    [Header("status")]
    [SerializeField] float moveSpeed;
    [SerializeField] float dashSpeed;
    [SerializeField] float rotationSpeed;

    public static BossLocomotion instance;

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
    }

    // Update is called once per frame
    void Update()
    {
        // �� �����Ӹ��� ������ ������ ���ϰ� �ʹ�.
        SetTargetDirection();

        // ������ ���⿡ ���߾ ���� ȸ���� �ٲٰ� �ʹ�.
        HandleRotation();

        // ������ �������� �̵��ϰ� �ʹ�.
        //HandleMovement();
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
    public void SetMoveDirection()
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

    //�������� ���⿡ ���߾ ���� ȸ�������� �ٲٰ� �ʹ�.
    void HandleRotation()
    {
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        myTransform.rotation = Quaternion.Slerp(myTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void MoveToDirection()
    {
        //�̵��������� �̵��ϰ� �ʹ�.
        //p=p0 + vt
        transform.position += moveSpeed * moveDirection * Time.deltaTime;
    }

}
