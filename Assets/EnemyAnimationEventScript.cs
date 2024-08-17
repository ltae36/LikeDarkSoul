using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEventScript : MonoBehaviour
{

    public BoxCollider swordCollider;

    public void AttackAnimationEnd()
    {
        swordCollider.enabled = false;
    }
}
