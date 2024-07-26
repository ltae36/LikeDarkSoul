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
            //눈에서 뜨면
            //에니메이션 재생하고
            //hp바 활성화시킨다.
            //창 콜라이더 활성화시킨다.
            return idleState;
        }
        else
        {
            return this;
        }

        
    }

}
