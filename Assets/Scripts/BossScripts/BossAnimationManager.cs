using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BossLocomotion))]
public class BossAnimationManager : MonoBehaviour
{
    BossLocomotion locomotion;
    [SerializeField] Animator animator;


    //public AnimationCurve myCurve;

    float vertical;
    float horizontal;


    private void Start()
    {
        locomotion = GetComponent<BossLocomotion>();    
    }

    void Update()
    {
        vertical = locomotion.vertical;
        horizontal = locomotion.horizontal;
        //animator의 변수를 업데이트 하고 싶다.
        UpdateValuesInAnimator();

        //myCurve.Evaluate()
    }

    void UpdateValuesInAnimator()
    {
        animator.SetFloat("Vertical",vertical,0.1f,Time.deltaTime);
        animator.SetFloat("Horizontal",horizontal, 0.1f, Time.deltaTime);
    }

    public void SetTrigger(string trigger)
    {
        animator.SetTrigger(trigger);
    }

    public void AwakeAnimationStart()
    {
        animator.SetTrigger("IsAwake");
    }



    public void AttackAnimationStart(int distanceType, float attackType)
    {
        animator.SetTrigger("WalkToAttack");
        animator.SetInteger("DistanceType", distanceType);
        animator.SetFloat("AttackType", attackType);
    }

    public void TurnOffRootMotion()
    {
        animator.applyRootMotion = false;
    }

    public void TurnOnRootMotion()
    {
        animator.applyRootMotion=true;
    }
    public bool IsAwakeAnimationEnd()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        int awakeHash = Animator.StringToHash("Base Layer.Awake");
        if(stateInfo.fullPathHash == awakeHash)
        {
            if (stateInfo.normalizedTime > 0.8f)
                return true;
        }
        return false;

    }

    public bool IsAttackAnimationEnd()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        string stateName = "Base Layer.Attack.AttackCombo";

        int attackHash = Animator.StringToHash(stateName);
        if (stateInfo.fullPathHash == attackHash)
        {
            if (stateInfo.normalizedTime > 0.89f)
                return true;
        }
        return false;

    }
    /// <summary>
    /// attack type에 따라 근거리, 중거리, 원거리 공격 애니메이션 트리거가 바뀝니다.
    /// </summary>
    /// <param name="distanceType">0 = near attack 1 = normal attack 2 = far attack</param>
    public void SetDistanceType(int distanceType)
    {
        //attack type 0 = near attack
        //attack type 1 = normal attack
        //attack type 2 = far attack
        animator.SetInteger("DistanceType", distanceType);
    }

    /// <summary>
    /// 공격 유형을 정합니다.
    /// </summary>
    /// <param name="attackType">0 = first attack 1 = second attack 2 = third attack</param>
    public void SetAttackType(float attackType)
    {
        //attack type 0 = first attack
        //attack type 1 = second attack
        //attack type 2 = third attack
        animator.SetFloat("AttackType", attackType);
    }

    public void DeathAnimationStart()
    {
        animator.SetTrigger("Death");
    }
}
