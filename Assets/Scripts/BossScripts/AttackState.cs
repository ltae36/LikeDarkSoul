using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public IdleState idleState;
    public BossLocomotion locomotion;
    public float recoveryTime;
    public bool isRecoveryTime = false;

    float currentTime;

    [Header("Attack range")]
    [SerializeField]
    float nearAttackRange;
    [SerializeField]
    float normalAttackRange;
    [SerializeField]
    float farAttackRange;



    public override State RunCurrentState()
    {
        //지금 attack delay 상태일 때
        if(BossAnimationManager.bossAnimationManager.IsAttackDelay() == true)
        {
            float distance = locomotion.targetDistance;
            print(distance);
            //recovery time에 계속 현재 시간을 더해준다.
            currentTime += Time.deltaTime;
            //만약 recovery timer가 세팅된 값보다 커지면
            if (currentTime > recoveryTime)
            {
                //상태를 attack 상태로 바꾼다.
                BossAnimationManager.bossAnimationManager.SetTrigger("AttackDelayToAttack");
                //거리에 따라 어떤 공격을 할지 결정한다.

                //만약 거리가 너무 멀면
                if (distance > farAttackRange)
                {
                    //idle 상태로 바꾼다.
                    BossAnimationManager.bossAnimationManager.SetTrigger("AttackDelayToWalk");
                    return idleState;
                }
            }
        }
        //지금 attack 상태일 때
        else
        {
            //타이머는 세팅되면 안된다.
            currentTime = 0;
            //공격이 끝나면 has exit time으로 바로 attack delay로 넘어가게 된다.
        }

        return this;
    }


}
