using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AttackState : State
{
    public IdleState idleState;
    public float recoveryTime;
    public bool isRecoveryTime = false;


    State returnState;

    private void Start()
    {
        
    }
    public override State RunCurrentState()
    {
        BossLocomotion.instance.SetIdleDirection();
        return idleState;
    }


}
