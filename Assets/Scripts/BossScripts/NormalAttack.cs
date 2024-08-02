using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NormalAttack : State
{
    public IdleState idleState;
    public StabAttack stabAttackState;
    public VerticalAttack verticalAttackState;

    public override State RunCurrentState()
    {
        float randNum = Random.Range(0.0f, 1.0f);
        BossAnimationManager.instance.SetDistanceType(1);
        if (randNum > 0.5f)
        {
            BossAnimationManager.instance.SetAttackType(1);
            BossAnimationManager.instance.SetTrigger("WalkToAttack");
            return stabAttackState;
        }
        else
        {
            BossAnimationManager.instance.SetAttackType(0);
            BossAnimationManager.instance.SetTrigger("WalkToAttack");
            return verticalAttackState;
        }
    }


}
