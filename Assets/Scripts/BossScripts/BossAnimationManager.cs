using UnityEngine;

[RequireComponent(typeof(BossLocomotion))]
public class BossAnimationManager : MonoBehaviour
{
    //animation�� �����ϴ� Ŭ����

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
        //animator�� ������ ������Ʈ �ϰ� �ʹ�.
        UpdateValuesInAnimator();
    }

    void UpdateValuesInAnimator()
    {
        animator.SetFloat("Vertical",vertical,0.1f,Time.deltaTime);
        animator.SetFloat("Horizontal",horizontal, 0.1f, Time.deltaTime);
    }
}
