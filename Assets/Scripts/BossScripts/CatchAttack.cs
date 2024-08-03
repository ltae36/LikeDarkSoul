using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchAttack : State
{
    public IdleState idleState;
    bool isIdle = false;
    public State returnState;

    public override State RunCurrentState()
    {
        if(isIdle) { return this; }
        BossLocomotion.instance.SetIdleDirection();
        isIdle = true;
        StartCoroutine(EndAnimation());
        return returnState;
    }

    IEnumerator EndAnimation()
    {
        yield return new WaitForSeconds(3.0f);
        isIdle = false;
        returnState = idleState;
    }
}


