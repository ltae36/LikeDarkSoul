using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventScript : MonoBehaviour
{

    BossPhaseTransition phaseTransition;
    BossAnimationManager animationManager;

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
    }
    
}
