using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearAttack : State
{
    public CatchAttack catchAttackState;

    public override State RunCurrentState()
    {
        //잡기 공격을 한다
        //애니메이션에서 잡기 공격을 실행시킨다.
        BossAnimationManager.instance.SetDistanceType(0);
        BossAnimationManager.instance.SetAttackType(0);
        BossAnimationManager.instance.SetTrigger("WalkToAttack");
        return catchAttackState;
    }
}
