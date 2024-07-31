using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchAttack : State
{
    public IdleState idleState;
    public override State RunCurrentState()
    {
        BossLocomotion.instance.SetIdleDirection();
        return idleState;
    }
}


