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
            //만약 타이머 시간이 다 되면
            currentTime = 0;
            //애니메이션 trigger을 킨다.
            BossAnimationManager.bossAnimationManager.SetTrigger("WalkToAttackDelay");
            //BossAnimationManager.bossAnimationManager.ResetTrigger("");
            //현재 상태를 attack state로 바꾼다.

            return attackState;
        }
        boss.MoveToDirection();
        //움직이자.
        return this;
    }

}
