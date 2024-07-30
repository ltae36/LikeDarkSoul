using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakeState : State
{
    public IdleState idleState;
    State returnState;
    public bool isAwake = false;

    private void Start()
    {
        returnState = this;    
    }
    public override State RunCurrentState()
    {
        if (isAwake)
        {
            BossAnimationManager.instance.SetTrigger("IsAwake");

            StartCoroutine(AwakeProcess());
        }

        return returnState;
        
    }

    IEnumerator AwakeProcess()
    {
        yield return new WaitForSeconds(1.34f);

        returnState = idleState;
        BossLocomotion.instance.SetIdleDirection();
        StopAllCoroutines();
    }
}
