using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public bool canSeeThePlayer;
    public AttackState attackState;
    
    
    public override State RunCurrentState()
    {
        //일정 시간 동안 움직인 다음
        //플레이어를 공격한다.
        return this;
    }

}
