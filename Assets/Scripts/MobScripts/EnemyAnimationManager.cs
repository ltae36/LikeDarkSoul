using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationManager : MonoBehaviour
{

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void AwakeAnimationStart()
    {
        animator.SetTrigger("IsAwake");
    }

    public void IdleAnimationStart()
    {
        animator.SetTrigger("Idle");
    }

    public void RunAnimationStart() {
        animator.SetTrigger("Chase");
    }

    public void AttackAnimationStart()
    {
        animator.SetTrigger("Attack");
    }

    public void AttackDelayAnimationStart()
    {
        animator.SetTrigger("AttackDelay");
    }
    public bool IsAttackAnimationEnd()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        string stateName = "Base Layer.AttackCombo";

        int attackHash = Animator.StringToHash(stateName);
        if (stateInfo.fullPathHash == attackHash)
        {
            if (stateInfo.normalizedTime > 0.83f)
                return true;
        }
        return false;
    }

    public bool IsAwakeAnimationEnd()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        string stateName = "Base Layer.Zombie Stand Up";

        int awakeHash = Animator.StringToHash(stateName);
        if(stateInfo.fullPathHash == awakeHash)
        {
            if(stateInfo.normalizedTime > 0.92f)
            {
                return true;
            }
        }
        return false;
    }
}
