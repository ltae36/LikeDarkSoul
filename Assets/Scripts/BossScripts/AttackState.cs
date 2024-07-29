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
        //공격을 할 것임
        //공격을 할 건데 공격 타입을 정할 것임
        //공격 타입은 지금 플레이어의 위치에 따라 결정 된다.
        //그 외에는 랜덤으로 지정할 것이다.
        //공격 패턴은 다음과 같다
        //근거리: 잡기, 철권, 철사장
        //일반거리: 찌르기, 횡베기, naereo zzigi
        //원거리: 점프, 점프후 내려찍기

        // 타겟과의 거리를 받아오자
        float distance = BossLocomotion.instance.targetDistance;

        //근거리 공격
        if (distance < nearAttackRange)
        {
            BossAnimationManager.instance.SetAttackType(0);
            BossAnimationManager.instance.SetTrigger("WalkToAttack");
            returnState = this;

        }
        //중거리공격
        else if (distance < normalAttackRange)
        {
            BossAnimationManager.instance.SetAttackType(1);
            BossAnimationManager.instance.SetTrigger("WalkToAttack");
            returnState = this;

        }
        //원거리 공격
        else if(distance < farAttackRange)
        {
            BossAnimationManager.instance.SetAttackType(2);
            BossAnimationManager.instance.SetTrigger("WalkToAttack");
            returnState = this;

        }
        //원거리 보다 멀면 걍 idle 하삼
        else
        {
            print("attack -> idle");
            return idleState;
        }

        //공격 애니메이션이 끝날 때까지 기다린다.
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
