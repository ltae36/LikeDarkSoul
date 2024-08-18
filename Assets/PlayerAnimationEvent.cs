using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    public BoxCollider swordCollider;

    public void AttackStart()
    {
        swordCollider.enabled = true;
    }
    public void AttackEnd()
    {
        swordCollider.enabled = false;
    }
}
