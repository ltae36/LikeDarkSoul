using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEventScript : MonoBehaviour
{

    public BoxCollider swordCollider;

    public void AttackAnimationStart()
    {
        swordCollider.enabled = true;
    }
    public void AttackAnimationEnd()
    {
        swordCollider.enabled = false;
    }
}
