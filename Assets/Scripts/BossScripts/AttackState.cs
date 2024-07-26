using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public IdleState idleState;
    public override State RunCurrentState()
    {
        //공격을 한 다음
        //다시 idle state로 돌아온다.
        return idleState;
    }
}
