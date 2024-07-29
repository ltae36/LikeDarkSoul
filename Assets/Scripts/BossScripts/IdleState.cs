using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public AttackState attackState;

    [SerializeField] float idleTime = 2.0f;
    float currentTime = 0;
    
    public override State RunCurrentState()
    {
        if (currentTime > idleTime)
        {
            currentTime = 0;
            //���� ���¸� attack state�� �ٲ۴�.
            print("idle -> attack");
            return attackState;
        }
        currentTime += Time.deltaTime;

        //print(currentTime);
        //���� Ÿ�̸� �ð��� �� �Ǹ�
        BossLocomotion.instance.MoveToDirection();
        //��������.
        return this;
    }

}
