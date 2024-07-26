using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public AttackState attackState;
    public BossLocomotion boss;

    [SerializeField] float idleTime = 2.0f;
    float currentTime = 0;
    
    public override State RunCurrentState()
    {
        currentTime += Time.deltaTime;
        //print(currentTime);
        if (currentTime > idleTime)
        {
            //���� Ÿ�̸� �ð��� �� �Ǹ�
            currentTime = 0;
            //�ִϸ��̼� trigger�� Ų��.
            BossAnimationManager.bossAnimationManager.SetTrigger("WalkToAttackDelay");
            //BossAnimationManager.bossAnimationManager.ResetTrigger("");
            //���� ���¸� attack state�� �ٲ۴�.

            return attackState;
        }
        boss.MoveToDirection();
        //��������.
        return this;
    }

}
