using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AttackState : State
{
    public IdleState idleState;
    public float recoveryTime;
    public bool isRecoveryTime = false;

    float currentTime;

    [Header("Attack range")]
    [SerializeField]
    float nearAttackRange;
    [SerializeField]
    float normalAttackRange;
    [SerializeField]
    float farAttackRange;

    State returnState;

    private void Start()
    {
        points = new List<Vector3>();
    }
    public override State RunCurrentState()
    {
        //������ �� ����
        //������ �� �ǵ� ���� Ÿ���� ���� ����
        //���� Ÿ���� ���� �÷��̾��� ��ġ�� ���� ���� �ȴ�.
        //�� �ܿ��� �������� ������ ���̴�.
        //���� ������ ������ ����
        //�ٰŸ�: ���, ö��, ö����
        //�ϹݰŸ�: ���, Ⱦ����, naereo zzigi
        //���Ÿ�: ����, ������ �������

        // Ÿ�ٰ��� �Ÿ��� �޾ƿ���
        float distance = BossLocomotion.instance.targetDistance;

        //�ٰŸ� ����
        if (distance < nearAttackRange)
        {
            BossAnimationManager.instance.SetAttackType(0);
            BossAnimationManager.instance.SetTrigger("WalkToAttack");
            returnState = this;

        }
        //�߰Ÿ�����
        else if (distance < normalAttackRange)
        {
            BossAnimationManager.instance.SetAttackType(1);
            BossAnimationManager.instance.SetTrigger("WalkToAttack");
            returnState = this;

        }
        //���Ÿ� ����
        else if(distance < farAttackRange)
        {
            BossAnimationManager.instance.SetAttackType(2);
            BossAnimationManager.instance.SetTrigger("WalkToAttack");
            returnState = this;

        }
        //���Ÿ� ���� �ָ� �� idle �ϻ�
        else
        {
            print("attack -> idle");
            return idleState;
        }

        //���� �ִϸ��̼��� ���� ������ ��ٸ���.
        if (BossAnimationManager.instance.IsAttacking())
        {
            returnState = this;
        }
        else
        {
            BossLocomotion.instance.SetMoveDirection();
            returnState = idleState;
            print("attack -> idle");
        }

        return returnState;
    }

    IEnumerator AttackToWalkProcess()
    {
        yield return new WaitForSeconds(1);

        BossLocomotion.instance.SetMoveDirection();
        returnState = idleState;
        print("attack -> idle");
        StopAllCoroutines();
    }

    List<Vector3> points;
    private void OnDrawGizmos()
    {
        
        for(int i =0; i<360; i++)
        {
            points.Add(new Vector3(Mathf.Cos(Mathf.Deg2Rad * i), 0, Mathf.Sin(Mathf.Deg2Rad * i)));
        }
        for(int i =0; i<points.Count -1; i++)
        {
            Gizmos.DrawLine(BossLocomotion.instance.myTransform.position +points[i] * nearAttackRange, BossLocomotion.instance.myTransform.position+points[i+1] * nearAttackRange);
            Gizmos.DrawLine(BossLocomotion.instance.myTransform.position+points[i] * normalAttackRange, BossLocomotion.instance.myTransform.position + points[i + 1] * normalAttackRange);
            Gizmos.DrawLine(BossLocomotion.instance.myTransform.position+points[i] * farAttackRange, BossLocomotion.instance.myTransform.position+ points[i + 1] * farAttackRange);
        }
    }
}
