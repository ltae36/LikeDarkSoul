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

    public void ResetTrigger(string trigger)
    {
        animator.ResetTrigger(trigger);
    }


    public bool IsAttacking()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            float animeTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if(animeTime > 2)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        return false;
    }
    
    public bool IsAwaking()
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsTag("Awake");
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
}
