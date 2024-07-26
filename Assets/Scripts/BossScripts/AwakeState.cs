using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakeState : State
{
    public IdleState idleState;
    public bool isAwake = false;
    public override State RunCurrentState()
    {
        if (isAwake)
        {
            //������ �߸�
            //���ϸ��̼� ����ϰ�
            BossAnimationManager.bossAnimationManager.SetTrigger("IsAwake");
            //hp�� Ȱ��ȭ��Ų��.
            //â �ݶ��̴� Ȱ��ȭ��Ų��.
            return idleState;
        }
        else
        {
            return this;
        }

        
    }

}
