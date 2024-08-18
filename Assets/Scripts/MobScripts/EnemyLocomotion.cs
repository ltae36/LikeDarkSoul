using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static BossLocomotion;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UIElements;

public class EnemyLocomotion : MonoBehaviour
{
    public float targetDistance;
    /// <summary>
    /// ����ȭ�� vector
    /// </summary>
    public Vector3 moveDirection;
    public Vector3 movePosition;

    /// <summary>
    /// ����ȭ ���� ���� vector
    /// </summary>
    public Vector3 targetDirection;
    public Vector3 targetPosition;

    [Header("attack target")]
    public GameObject target;

    CharacterController cc;
    EnemyStatus status;
    private void Start()
    {
        cc = GetComponent<CharacterController>();
        target = GameObject.FindGameObjectWithTag("Player");
        if (target == null)
            Destroy(this);
        status = GetComponent<EnemyStatus>();
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
    }
    public void SetMoveDirection(MoveType moveType)
    {
        SetTargetPosition();
        SetTargetDirection();

        switch (moveType)
        {
            case MoveType.Linear:
            case MoveType.Dash:
                moveDirection = targetDirection.normalized;
                moveDirection.y = 0;
                movePosition = targetPosition;
                break;
            case MoveType.Circle:
                moveDirection = targetDirection.normalized;
                break;
        }
    }

    public void HandleRotation()
    {
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, status.rotationSpeed * Time.deltaTime);
    }

    public void MoveEnemy(MoveType moveType)
    {
        switch (moveType)
        {
            case MoveType.Linear:
                cc.Move(moveDirection * status.moveSpeed * Time.deltaTime);
                break;
            case MoveType.Dash:
                cc.Move(moveDirection * status.dashSpeed * Time.deltaTime);
                break;
            case MoveType.Circle:
                break;
        }
    }
}
