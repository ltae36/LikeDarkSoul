using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLocomotion : MonoBehaviour
{
    // Start is called before the first frame update
    public float vertical;
    public float horizontal;
    public Vector3 moveDirection;
    public float targetDistance;

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
        // �� �����Ӹ��� ������ ������ ���ϰ� �ʹ�.
        SetMoveDirection();

        // ������ ���⿡ ���߾ ���� ȸ���� �ٲٰ� �ʹ�.
        HandleRotation();

        // ������ �������� �̵��ϰ� �ʹ�.
        //HandleMovement();
    }

    // target�� ��ġ�� ���߾ �� ������ ������ ���ϰ� �ʹ�.
    void SetMoveDirection()
    {
        moveDirection = target.transform.position - transform.position;
        moveDirection.y = 0;
        moveDirection.Normalize();

        vertical = moveDirection.x;
        horizontal = moveDirection.z;

        targetDistance = moveDirection.magnitude;
    }

    //�������� ���⿡ ���߾ ���� ȸ�������� �ٲٰ� �ʹ�.
    void HandleRotation()
    {
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        myTransform.rotation = Quaternion.Slerp(myTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void MoveToDirection()
    {
        //�̵��������� �̵��ϰ� �ʹ�.
        //p=p0 + vt

        transform.position += moveSpeed * moveDirection * Time.deltaTime;

    }
}
