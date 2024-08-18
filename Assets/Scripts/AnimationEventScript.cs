using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventScript : MonoBehaviour
{

    BossPhaseTransition phaseTransition;
    BossAnimationManager animationManager;
    public BoxCollider swordCollider;
    public BoxCollider feetCollider;
    public ShakeObject shakeCam;

    private void Start()
    {
        phaseTransition = GetComponentInParent<BossPhaseTransition>();
        animationManager = GetComponent<BossAnimationManager>();
    }


    public void BossPhase2()
    {
        if (phaseTransition != null)
        {
            phaseTransition.PhaseTransition();
        }
    }

    public void AttackEnd()
    {
        print("AttackEnd");
        animationManager.AttackAnimationEnd();
        swordCollider.enabled = false;
    }

    public void AttackStart()
    {
        swordCollider.enabled = true;
    }
    
    public void FeetAttackStart()
    {
        feetCollider.enabled = true;
    }

    public void FeetAttackEnd()
    {
        feetCollider.enabled = false;
    }

    public void CamShake()
    {
        shakeCam.ShakePos();
    }
}
