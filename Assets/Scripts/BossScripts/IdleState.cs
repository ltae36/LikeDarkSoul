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
            //현재 상태를 attack state로 바꾼다.
            print("idle -> attack");
            return attackState;
        }
        currentTime += Time.deltaTime;

        //print(currentTime);
        //만약 타이머 시간이 다 되면
        BossLocomotion.instance.MoveToDirection();
        //움직이자.
        return this;
    }

}
