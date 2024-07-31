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
        if (randNum > 0.5f)
        {
            return stabAttackState;
        }
        else
        {
            return verticalAttackState;
        }
    }


}
