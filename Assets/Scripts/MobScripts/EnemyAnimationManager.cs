using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationManager : MonoBehaviour
{

    public static EnemyAnimationManager instance;
    Animator animator;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void AwakeAnimationStart()
    {
        animator.SetTrigger("Awake");
    }

    public void IdleAnimationStart()
    {
        animator.SetTrigger("Idle");
    }

    public void RunAnimationStart() {
        animator.SetTrigger("Run");
    }

    public void AttackAnimationStart()
    {
        animator.SetTrigger("Attack");
    }

    public bool IsAttackAnimationEnd()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        string stateName = "Base Layer.Enemy_Attack_1";

        int attackHash = Animator.StringToHash(stateName);
        if (stateInfo.fullPathHash == attackHash)
        {
            if (stateInfo.normalizedTime > 0.8f)
                return true;
        }
        return false;
    }


}
