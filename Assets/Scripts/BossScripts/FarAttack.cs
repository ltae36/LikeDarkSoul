using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FarAttack : State
{
    public JumpAttack jumpAttackState;
    public DashAttack dashAttackState;

    public override State RunCurrentState()
    {
        float randNum = Random.Range(0.0f, 1.0f);
        if(randNum > 0.5f)
        {
            jumpAttackState.SetJumpVelocity();
            return jumpAttackState;
        }
        else
        {
            dashAttackState.SetDashPosition(BossLocomotion.instance.target.transform.position);
            return dashAttackState;
        }
    }
}
