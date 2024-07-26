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
        //���� attack delay ������ ��
        if(BossAnimationManager.bossAnimationManager.IsAttackDelay() == true)
        {
            float distance = locomotion.targetDistance;
            print(distance);
            //recovery time�� ��� ���� �ð��� �����ش�.
            currentTime += Time.deltaTime;
            //���� recovery timer�� ���õ� ������ Ŀ����
            if (currentTime > recoveryTime)
            {
                //���¸� attack ���·� �ٲ۴�.
                BossAnimationManager.bossAnimationManager.SetTrigger("AttackDelayToAttack");
                //�Ÿ��� ���� � ������ ���� �����Ѵ�.

                //���� �Ÿ��� �ʹ� �ָ�
                if (distance > farAttackRange)
                {
                    //idle ���·� �ٲ۴�.
                    BossAnimationManager.bossAnimationManager.SetTrigger("AttackDelayToWalk");
                    return idleState;
                }
            }
        }
        //���� attack ������ ��
        else
        {
            //Ÿ�̸Ӵ� ���õǸ� �ȵȴ�.
            currentTime = 0;
            //������ ������ has exit time���� �ٷ� attack delay�� �Ѿ�� �ȴ�.
        }

        return this;
    }


}
