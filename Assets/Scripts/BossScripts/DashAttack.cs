using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAttack : State
{
    public IdleState idleState;

    Vector3 targetPosition;
    public override State RunCurrentState()
    {
        if (Vector3.Distance(targetPosition, BossLocomotion.instance.myTransform.position) < BossAttack.instance.normalAttackRange) 
        {
            return idleState;
        }
        BossLocomotion.instance.DashBoss();
        return this;
    }

    public void SetDashPosition(Vector3 target)
    {
        BossLocomotion.instance.SetMoveDirection();
        targetPosition = target;
    }
}
