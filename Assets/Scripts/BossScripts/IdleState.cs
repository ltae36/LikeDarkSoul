using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public bool canSeeThePlayer;
    public AttackState attackState;
    
    
    public override State RunCurrentState()
    {
        //���� �ð� ���� ������ ����
        //�÷��̾ �����Ѵ�.
        return this;
    }

}
