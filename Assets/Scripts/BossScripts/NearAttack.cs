using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearAttack : State
{
    public CatchAttack catchAttackState;

    public override State RunCurrentState()
    {
        return catchAttackState;
    }
}
