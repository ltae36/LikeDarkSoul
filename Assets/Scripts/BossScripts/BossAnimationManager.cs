using UnityEngine;

[RequireComponent(typeof(BossLocomotion))]
public class BossAnimationManager : MonoBehaviour
{
    //animation을 관리하는 클래스

    [SerializeField] Animator animator;
    BossLocomotion bossLocomotion;

    float vertical;
    float horizontal;

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
}
