using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventScript : MonoBehaviour
{
    public UIManager manager;

    BossPhaseTransition phaseTransition;

    private void Start()
    {
        phaseTransition = GetComponentInParent<BossPhaseTransition>();
    }
    public void HideBossHpBar()
    {
        manager.HideBossHpBar();
    }

    public void BossPhase2()
    {
        if (phaseTransition != null)
        {
            phaseTransition.PhaseTransition();
        }
    }
    
}
