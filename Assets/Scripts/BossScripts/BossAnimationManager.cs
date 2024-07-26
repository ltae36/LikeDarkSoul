using UnityEngine;

[RequireComponent(typeof(BossLocomotion))]
public class BossAnimationManager : MonoBehaviour
{
    //animation을 관리하는 클래스 singletone으로 구현하자
    public static BossAnimationManager bossAnimationManager;

    [SerializeField] Animator animator;
    BossLocomotion bossLocomotion;

    float vertical;
    float horizontal;

    private void Awake()
    {
        if (bossAnimationManager == null)
        {
            bossAnimationManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        bossLocomotion = GetComponent<BossLocomotion>();
    }

    
    void Update()
    {
        vertical = bossLocomotion.vertical;
        horizontal = bossLocomotion.horizontal;
        //animator의 변수를 업데이트 하고 싶다.
        UpdateValuesInAnimator();
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

    public bool IsAttackDelay()
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsTag("AttackDelay");
    }
}
