using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public IdleState idleState;
    public override State RunCurrentState()
    {
        //������ �� ����
        //�ٽ� idle state�� ���ƿ´�.
        return idleState;
    }
}
