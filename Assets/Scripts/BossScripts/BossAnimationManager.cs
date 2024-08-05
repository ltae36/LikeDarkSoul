using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;


[RequireComponent(typeof(BossLocomotion))]
public class BossAnimationManager : MonoBehaviour
{
    //animation을 관리하는 클래스 singletone으로 구현하자
    public static BossAnimationManager instance;

    [SerializeField] Animator animator;


    //public AnimationCurve myCurve;

    float vertical;
    float horizontal;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Dictionary<string, float> animationTime = new Dictionary<string, float>() {
        {"Base Layer.Awake",0.8f },
        {"Base Layer.Attack.Vertical", 0.85f },
        {"Base Layer.Attack.Horizontal",0.88f }
    
    };

    void Update()
    {
        vertical = BossLocomotion.instance.vertical;
        horizontal = BossLocomotion.instance.horizontal;
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



    public void AttackAnimationStart(int distanceType, int attackType)
    {
        animator.SetTrigger("WalkToAttack");
        animator.SetInteger("DistanceType", distanceType);
        animator.SetInteger("AttackType", attackType);
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

    public bool IsAttackAnimationEnd(int distanceType, int attackType)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        string stateName = "Base Layer.Attack.Vertical";

        if(distanceType == 1 && attackType == 0)
        {
            stateName = "Base Layer.Attack.Vertical";
        }
        else if(distanceType == 1 && attackType == 1)
        {
            stateName = "Base Layer.Attack.Horizontal";
        }
        int attackHash = Animator.StringToHash(stateName);
        if (stateInfo.fullPathHash == attackHash)
        {
            if (stateInfo.normalizedTime > animationTime[stateName])
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
    public void SetAttackType(int attackType)
    {
        //attack type 0 = first attack
        //attack type 1 = second attack
        //attack type 2 = third attack
        animator.SetInteger("AttackType", attackType);
    }

    public void DeathAnimationStart()
    {
        animator.SetTrigger("Death");
    }
}
