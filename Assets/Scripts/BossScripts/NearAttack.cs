using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearAttack : State
{
    public CatchAttack catchAttackState;

    public override State RunCurrentState()
    {
        //��� ������ �Ѵ�
        //�ִϸ��̼ǿ��� ��� ������ �����Ų��.
        BossAnimationManager.instance.SetDistanceType(0);
        BossAnimationManager.instance.SetAttackType(0);
        BossAnimationManager.instance.SetTrigger("WalkToAttack");
        return catchAttackState;
    }
}
